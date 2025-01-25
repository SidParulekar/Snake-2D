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

        if(powerupEnabled)
        {
            powerupTimeElapsed += Time.deltaTime;
            if(powerupTimeElapsed>=powerupEffectTime)
            {
                PowerupDisable(powerup);
                powerupTimeElapsed = 0;
            }
        }

        if(livesController.getlives()==0)
        {
            this.GetComponent<Snake>().enabled = false;
        }
    }

    private void FixedUpdate() 
    {
        
         for (int i = segments.Count - 1; i > 0; i--)
         {
            segments[i].position = segments[i - 1].position;
         }
              
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x + direction.x),
                                              Mathf.Round(this.transform.position.y + direction.y),
                                              0.0f);
    }

    private void PowerupDisable(string powerup)
    {
        switch(powerup)
        {
            case "ScoreBoost":
                scoreIncrement = 100;
                break;

            case "Shield":
                shield = false;
                break;
        }

        powerupEnabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag=="Food")
        {
            Grow();
            scoreController.IncreaseScore(scoreIncrement);
        }

        if(collider.tag=="Poison")
        {
            Destroy();
        }

        if(collider.tag=="ScoreBoost")
        {
            collider.gameObject.SetActive(false);
            powerup = collider.tag;
            scoreIncrement = 200;
            powerupEnabled = true;
        }

        if(collider.tag=="Shield")
        {
            collider.gameObject.SetActive(false);
            powerup = collider.tag;
            shield = true;
            powerupEnabled = true;
        }

        if(collider.tag=="Wall" && !shield || collider.tag=="SnakeBody" && !shield)
        {
            KillPlayer();
            ResetPlayer();       
        }

        if(collider.tag=="Wall" && shield)
        {
            direction = -1*direction;
        }
    }
}
