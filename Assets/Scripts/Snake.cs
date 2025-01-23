using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;

    private List<Transform> segments = new List<Transform>();

    [SerializeField] private Transform segmentPreFab;

    [SerializeField] private int initialPlayerSize;

    [SerializeField] private ScoreController scoreController;
    [SerializeField] private LivesController livesController;

    private float powerupTimeElapsed=0f;

    [SerializeField] private float powerupEffectTime = 3f;

    private int scoreIncrement=100;

    private bool shield = false;
    private bool powerupEnabled = false;

    private string powerup;

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

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPreFab);

        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
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
            KillPlayer();
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

        direction = Vector2.right;

        this.transform.position = Vector3.zero;
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

    private void KillPlayer()
    {
        if (livesController.getlives() > 0)
        {
            livesController.DecreaseLives(1);
        }
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
