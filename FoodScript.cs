using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public GameObject[] foods;

    void Start()
    {
        AddFood();
    }

    public void AddFood()
    {
        int rand = Random.Range(0, foods.Length);

        //This gives the shorten food a less likely chance to spawn
        if (rand == 3)
            rand = Random.Range(0, 4);

        Instantiate(foods[rand], new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f)),
                Quaternion.identity);
    }
}
