using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    
    [SerializeField] private float spawnRateMultiplier;
    [SerializeField] private float speed;
    [SerializeField] private float duration;
    private float durationTimer;
    private float timer;

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
        timer += Time.deltaTime;
        GoPlaces();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
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

    public void PowerupEffect(GameObject player)
    {
        // play the powerup effect for 5 seconds
        if(durationTimer <= duration){
            var playerImageReference = player.GetComponent<PekoPlayer>().PlayerImage;

            if (Input.GetKeyDown(KeyCode.Space))
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
