using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTwo : SnakeController
{
    private void Start()
    {
        segmentPreFab.position = startPosition;
        startDirection = Vector2.left;
        ResetPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != Vector2.down)
            {
                direction = Vector2.up;
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != Vector2.up)
            {
                direction = Vector2.down;
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction != Vector2.right)
            {
                direction = Vector2.left;
            }
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction != Vector2.left)
            {
                direction = Vector2.right;
            }
        }

        PowerupProcessing();

        if (livesController.GetLives() == 0)
        {
            this.GetComponent<SnakeTwo>().enabled = false;
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
