using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{

    [SerializeField] private float carrotSpeed;
    [SerializeField] private GameObject carrotImage;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoPlaces();
        Gtfo();

        timer += Time.deltaTime;

    }

    private void GoPlaces()
    {
        transform.Translate(new Vector2(1, 0) * carrotSpeed * Time.deltaTime);
    }

    private void Gtfo()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        var carrotWidth = carrotImage.GetComponent<SpriteRenderer>().bounds.size.x;
        var carrotHeight = carrotImage.GetComponent<SpriteRenderer>().bounds.size.y;


        if (transform.position.x + (carrotWidth / 2) < minScreenBounds.x)
        {
            Destroy(gameObject);
        }

        if (transform.position.x - (carrotWidth / 2) > maxScreenBounds.x)
        {
            Destroy(gameObject);
        }

        if (transform.position.y + (carrotHeight / 2) < minScreenBounds.y)
        {
            Destroy(gameObject);
        }

        if (transform.position.y - (carrotHeight / 2) > maxScreenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
