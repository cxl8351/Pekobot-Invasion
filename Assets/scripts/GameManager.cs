using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    protected const float BASE_SPAWN_RATE = 1000;
    private float timer;
    [SerializeField] private GameObject powerupPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        //Attempt to spawn a powerup
        if (timer >= 0.01)
        {
            // The odds of successfully spawning a powerup is 1/1000 per second.
            // Changing the SpawnRateMultiplier will change the denominator.
            float maxRanNum = BASE_SPAWN_RATE * (1 / powerupPrefab.GetComponent<Powerup>().SpawnRateMultiplier);
            float spawnNumber = Random.Range(0, maxRanNum);
            spawnNumber = Mathf.Floor(spawnNumber);
            SpawnPowerup(spawnNumber);

            timer = 0;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnNumber"></param>
    private void SpawnPowerup(float spawnNumber)
    {
        if (spawnNumber == 0)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            spawnPos.x = Random.Range(minScreenBounds.x, maxScreenBounds.x);
            spawnPos.y = Random.Range(minScreenBounds.y, maxScreenBounds.y);

            Instantiate( powerupPrefab, spawnPos, powerupPrefab.transform.rotation );

            Debug.Log("Spawned Powerup");
        }
    }
}
