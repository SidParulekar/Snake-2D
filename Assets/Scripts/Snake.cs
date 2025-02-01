using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : SnakeController
{
    private void Start()
    {
        segmentPreFab.position = startPosition;
        startDirection = Vector2.right;
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

        PowerupProcessing();

        if(livesController.getlives()==0)
        {
            this.GetComponent<Snake>().enabled = false;
        }
    }

    private void FixedUpdate() 
    {
        SnakeMovement();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CollisionProcessing(collider.tag, collider.gameObject);
    }
}
