using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeScript : MonoBehaviour
{
    public float length, speed;

    float horizontal, vertical, sideEdge = 8.6f, topEdge = 4.7f;

    public GameObject foodManager, head;

    public GameManager gm;

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = new Vector2(0, 0.5f);
        transform.position = Vector2.zero;
        audio = GetComponent<AudioSource>();
        length = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.gameRunning)
            return;
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && horizontal != speed)
        {
            head.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);
            horizontal = -speed;
            vertical = 0;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && horizontal != -speed)
        {
            head.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y);
            horizontal = speed;
            vertical = 0;
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && vertical != -speed)
        {
            head.transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
            vertical = speed;
            horizontal = 0;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && vertical != speed)
        {
            head.transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
            vertical = -speed;
            horizontal = 0;
        }

        transform.Translate(new Vector2(horizontal * Time.deltaTime,
                vertical * Time.deltaTime));

        if (transform.position.y > topEdge || transform.position.y < -topEdge
                || transform.position.x < -sideEdge ||
                transform.position.x > sideEdge)
        {
            gm.EndGame();
        }
    }

    void SpeedChange(float speedChange)
    {
        speed += speedChange;
        if (speed < 5 || speed > 11)
        {
            speed -= speedChange;
            return;
        }

        if (horizontal == speed - speedChange)
            horizontal = speed;
        else if (-horizontal == speed - speedChange)
            horizontal = -speed;
        else if (vertical == speed - speedChange)
            vertical = speed;
        else
            vertical = -speed;
    }

    //Updates all the necessary properties after eating a food item
    void ateFood(Collider2D other, FoodScript script, float changeInLength,
            float changeInSpeed, int changeInPoints)
    {
        Destroy(other.gameObject);
        script.AddFood();
        length += changeInLength;
        audio.Play();
        SpeedChange(changeInSpeed);
        gm.UpdateScore(changeInPoints);
    }

    //Checks to see if the player has touched food
    void OnTriggerEnter2D(Collider2D other)
    {
        FoodScript foodScript = foodManager.GetComponent<FoodScript>();

        if (other.gameObject.tag == "Food")
            ateFood(other, foodScript, 0.5f, 0f, 1);

        if (other.gameObject.tag == "DoubleFood")
            ateFood(other, foodScript, 1f, 0f, 2);

        if (other.gameObject.tag == "TripleFood")
            ateFood(other, foodScript, 1.5f, 0f, 3);

        if (other.gameObject.tag == "ShortenFood")
            ateFood(other, foodScript, -length / 2, 0f, 1);

        if (other.gameObject.tag == "SpeedFood")
            ateFood(other, foodScript, 0f, 2f, 1);

        if (other.gameObject.tag == "SlowFood")
            ateFood(other, foodScript, 0f, -2f, 1);
    }
}
