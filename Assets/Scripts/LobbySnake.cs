using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySnake : SnakeController
{
    private void Start()
    {
        segmentPreFab.position = startPosition;
        startDirection = Vector2.right;
        ResetPlayer();
    }

    private void Update()
    {
        if(this.transform.position.x >= 20f && direction == Vector2.right)
        {
            direction = Vector2.down;
        }

        else if(this.transform.position.x <= -20f && direction == Vector2.left)
        {
            direction = Vector2.up;
        }

        else if(this.transform.position.y >= 9f && direction == Vector2.up)
        {
            direction = Vector2.right;
        }

        else if (this.transform.position.y <= -9f && direction == Vector2.down)
        {
            direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        SnakeMovement();
    }
}
