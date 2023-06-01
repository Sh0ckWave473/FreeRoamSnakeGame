using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailScript : MonoBehaviour
{
    public GameObject player, head;

    public TrailRenderer trailRenderer;

    public GameManager gm;

    public EdgeCollider2D eC;

    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();

        //Also from the Youtube video by ZeroKelvinTutorials
        GameObject separateCollider = new GameObject("SeparateCollider", typeof(EdgeCollider2D));
        eC = separateCollider.GetComponent<EdgeCollider2D>();
        eC.isTrigger = true;
        eC.edgeRadius = 0.1f;

        audio = GetComponent<AudioSource>();

        transform.position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.gameRunning)
        {
            trailRenderer.time = float.PositiveInfinity;
            return;
        }

        transform.position = player.transform.position;

        //Learned from ZeroKelvinTutorials on YouTube
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < trailRenderer.positionCount; i++)
            points.Add(trailRenderer.GetPosition(i));
        eC.SetPoints(points);

        if (player.GetComponent<SnakeScript>().length != 0.5 &&
                eC.OverlapPoint(head.transform.position))
        {
            gm.EndGame();
        }

        trailRenderer.time = player.GetComponent<SnakeScript>().length;
    }
}
