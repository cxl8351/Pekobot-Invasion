using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    
    [SerializeField] private float spawnRateMultiplier;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    [SerializeField] private GameObject powerupImage;
    private float durationTimer;
    private float gunTimer;

    public float SpawnRateMultiplier
    {
        get
        {
            return spawnRateMultiplier;
        }

        set
        {
            spawnRateMultiplier = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        durationTimer = 0;
        speed = 2;
        duration = 5;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        GoPlaces();
        Gtfo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnPickUp(collision.transform.root.gameObject);
    }

    private void GoPlaces()
    {
        transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
    }

    private void OnPickUp(GameObject player)
    {
        player.GetComponent<PekoPlayer>().PowerupPrefab = gameObject; // Assign the player this power up
        gameObject.SetActive(false); // deactivate the powerup (not destroy it)
    }

    private void Gtfo()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        var powerupWidth = powerupImage.GetComponent<SpriteRenderer>().bounds.size.x;
        var powerupHeight = powerupImage.GetComponent<SpriteRenderer>().bounds.size.y;

        if (transform.position.x - (powerupWidth / 2) < minScreenBounds.x)
        {
            // Note: You cannot access a dimensional component, i.e. transform.position.x == ________;
            transform.position = new Vector2(minScreenBounds.x + powerupWidth / 2, transform.position.y);
        }

        if (transform.position.x + (powerupWidth / 2) > maxScreenBounds.x)
        {
            transform.position = new Vector2(maxScreenBounds.x - powerupWidth / 2, transform.position.y);
        }

        if (transform.position.y - (powerupHeight / 2) < minScreenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, minScreenBounds.y + (powerupHeight / 2));
        }

        if (transform.position.y + (powerupHeight / 2) > maxScreenBounds.y)
        {
            transform.position = new Vector2(transform.position.x, maxScreenBounds.y - (powerupHeight / 2));
        }
    }

    public void PowerupEffect(GameObject player)
    {
        // play the powerup effect for 5 seconds
        if(durationTimer <= duration){
            var playerImageReference = player.GetComponent<PekoPlayer>().PlayerImage;
            float rateOfFire = player.GetComponent<PekoPlayer>().RateOfFire;

            gunTimer += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                if (gunTimer >= rateOfFire)
                {
                    float orientation = Mathf.Round(playerImageReference.transform.rotation.z);
                    // If the player's image is facing either up or down...
                    if (orientation == 1 || orientation == -1)
                    {
                        // ... spread the carrots out on the x-axis of the player.
                        Instantiate(player.GetComponent<PekoPlayer>().CarrotShotPrefab, playerImageReference.transform.position, playerImageReference.transform.rotation);
                        Instantiate(player.GetComponent<PekoPlayer>().CarrotShotPrefab, new Vector3(playerImageReference.transform.position.x + 1, playerImageReference.transform.position.y, playerImageReference.transform.position.z), playerImageReference.transform.rotation);
                        Instantiate(player.GetComponent<PekoPlayer>().CarrotShotPrefab, new Vector3(player.transform.position.x - 1, playerImageReference.transform.position.y, playerImageReference.transform.position.z), playerImageReference.transform.rotation);
                    }
                    else
                    {
                        // ... spread the carrots out on the y-axis of the player.
                        Instantiate(player.GetComponent<PekoPlayer>().CarrotShotPrefab, playerImageReference.transform.position, playerImageReference.transform.rotation);
                        Instantiate(player.GetComponent<PekoPlayer>().CarrotShotPrefab, new Vector3(playerImageReference.transform.position.x, playerImageReference.transform.position.y + 1, playerImageReference.transform.position.z), playerImageReference.transform.rotation);
                        Instantiate(player.GetComponent<PekoPlayer>().CarrotShotPrefab, new Vector3(playerImageReference.transform.position.x, playerImageReference.transform.position.y - 1, playerImageReference.transform.position.z), playerImageReference.transform.rotation);
                    }
                    gunTimer = 0;
                }
            }
            durationTimer += Time.deltaTime;
        }
        // after 5 seconds...
        else
        {
            Destroy(gameObject); // ...destroy the power up--removing its assignment from the player.
            durationTimer = 0; // ...reset the timer.
        }
    }


}
