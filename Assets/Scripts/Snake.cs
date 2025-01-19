using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private List<Transform> segments = new List<Transform>();

    [SerializeField]private Transform segmentPreFab;

    [SerializeField]private int initialPlayerSize;

    [SerializeField] private ScoreController scoreController;

    private void Start()
    {
        ResetPlayer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate() 
    {

        for(int i = segments.Count-1; i>0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x + direction.x),
                                              Mathf.Round(this.transform.position.y + direction.y),
                                              0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPreFab);

        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);

        scoreController.IncreaseScore(100);
    }

    private void Destroy()
    {
        if(segments.Count>1)
        {
            Destroy(segments[segments.Count - 1].gameObject);
            segments.Remove(segments[segments.Count - 1]);
        }

        else
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        for(int i=1; i<segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        for(int i=1; i<this.initialPlayerSize; i++)
        {
            segments.Add(Instantiate(this.segmentPreFab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag=="Food")
        {
            Grow();
        }

        if(collider.tag=="Poison")
        {
            Destroy();
        }

        if(collider.tag=="Obstacle")
        {
            ResetPlayer();
        }
    }
}
