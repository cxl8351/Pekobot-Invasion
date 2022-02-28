using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PekoPlayer : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private GameObject playerImage;
    [SerializeField] private GameObject carrotShotPrefab;
    [SerializeField] public GameObject powerupPrefab;

    public GameObject PowerupPrefab
    {
        get
        {
            return powerupPrefab;
        }

        set
        {
            powerupPrefab = value;
        }

    }

    public GameObject CarrotShotPrefab
    {
        get
        {
            return carrotShotPrefab;
        }
        set
        {
            carrotShotPrefab = value;
        }
    }

    public GameObject PlayerImage
    {
        get
        {
            return playerImage;
        }
        set
        {
            playerImage = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoPlaces();
        FigureItOut();

        if(powerupPrefab == null)
        {
            Gun();
        }
        else
        {
            powerupPrefab.GetComponent<Powerup>().PowerupEffect(gameObject);
        }


    }

    private void GoPlaces()
    {
        // Strafing
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector2(0, 1) * (speed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(-1, 0) * (speed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector2(0, -1) * (speed * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(1, 0) * (speed * Time.deltaTime));
            }
        }
        // Normal Movement
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector2(0, 1) * (speed * Time.deltaTime));
                playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(-1, 0) * (speed * Time.deltaTime));
                playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector2(0, -1) * (speed * Time.deltaTime));
                playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(1, 0) * (speed * Time.deltaTime));
                playerImage.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void FigureItOut()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint( Vector3.zero );
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        var playerWidth = playerImage.GetComponent<SpriteRenderer>().bounds.size.x;
        var playerHeight = playerImage.GetComponent<SpriteRenderer>().bounds.size.y;

        if (transform.position.x - (playerWidth / 2) < minScreenBounds.x)
        {
            // Note: You cannot access a dimensional component, i.e. transform.position.x == ________;
            transform.position = new Vector2(minScreenBounds.x + playerWidth / 2, transform.position.y);
        }

        if (transform.position.x + (playerWidth / 2) > maxScreenBounds.x)
        {
            transform.position = new Vector2(maxScreenBounds.x - playerWidth / 2, transform.position.y);
        }

        if(transform.position.y - (playerHeight / 2) < minScreenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, minScreenBounds.y + (playerHeight / 2));
        }

        if (transform.position.y + (playerHeight / 2) > maxScreenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, maxScreenBounds.y - (playerHeight / 2));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Gun()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(carrotShotPrefab, transform.position, playerImage.transform.rotation);
        }
        
    }

}
