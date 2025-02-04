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
            if(direction!=Vector2.down)
            {
                direction = Vector2.up;
            }    
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            if(direction!=Vector2.up)
            {
                direction = Vector2.down;
            }        
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            if(direction!=Vector2.right)
            {
                direction = Vector2.left;
            }      
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            if(direction!=Vector2.left)
            {
                direction = Vector2.right;
            }     
        }

        PowerupProcessing();

        if(livesController.GetLives()==0)
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
