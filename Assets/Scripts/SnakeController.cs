using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    protected Vector2 direction;

    protected List<Transform> segments = new List<Transform>();

    [SerializeField] protected Transform segmentPreFab;

    [SerializeField] protected int initialPlayerSize;

    [SerializeField] protected ScoreController scoreController;
    [SerializeField] protected LivesController livesController;

    [SerializeField] protected PowerupStatus powerupStatus;

    [SerializeField] protected float powerupEffectTime;

    [SerializeField] protected Vector3 startPosition;

    protected Vector2 startDirection;

    protected float powerupTimeElapsed = 0f;

    protected int scoreIncrement = 100;

    protected bool shield = false;
    protected bool powerupEnabled = false;

    protected string powerup;

    protected void Grow()
    {
        Transform segment = Instantiate(this.segmentPreFab);

        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    protected void Shrink()
    {
        if (segments.Count > 1)
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

    protected void ResetPlayer()
    {
        this.transform.position = startPosition;

        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < this.initialPlayerSize; i++)
        {
            segments.Add(Instantiate(this.segmentPreFab));
        }

        direction = startDirection;    
    }

    protected void KillPlayer()
    {
        if (livesController.GetLives() > 0)
        {
            livesController.DecreaseLives(1);
        }
    }

    protected void SnakeMovement()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x + direction.x),
                                              Mathf.Round(this.transform.position.y + direction.y),
                                              0.0f);
    }

    protected void CollisionProcessing(string colliderTag, GameObject colliderGameObject)
    {
        if (colliderTag == "Food")
        {
            Grow();
            if(scoreController)
            {
                scoreController.IncreaseScore(scoreIncrement);
            }    
        }

        if (colliderTag == "Poison" && !shield)
        {
            Shrink();
        }

        if (colliderTag == "ScoreBoost")
        {
            colliderGameObject.SetActive(false);
            powerup = colliderTag;
            scoreIncrement = 200;
            powerupStatus.gameObject.SetActive(true);
            powerupStatus.RefreshUI("Score Boost", "Active");
            powerupEnabled = true;
        }

        if (colliderTag == "Shield")
        {
            colliderGameObject.SetActive(false);
            powerup = colliderTag;
            shield = true;
            powerupStatus.gameObject.SetActive(true);
            powerupStatus.RefreshUI("Shield", "Active");
            powerupEnabled = true;
        }

        if (colliderTag == "Wall" && !shield || colliderTag == "SnakeBody" && !shield)
        {           
            KillPlayer();
            ResetPlayer();   
        }

        if (colliderTag == "Wall" && shield)
        {
            direction = -1 * direction;
        }
    }

    protected void PowerupProcessing()
    {
        if (powerupEnabled)
        {
            powerupTimeElapsed += Time.deltaTime;
            if (powerupTimeElapsed >= powerupEffectTime)
            {
                PowerupDisable(powerup);
                powerupTimeElapsed = 0;
            }
        }
    }

    private void PowerupDisable(string powerup)
    {
        switch (powerup)
        {
            case "ScoreBoost":
                scoreIncrement = 100;
                powerupStatus.gameObject.SetActive(false);
                break;

            case "Shield":
                shield = false;
                powerupStatus.gameObject.SetActive(false);
                break;
        }

        powerupEnabled = false;
    }

}
