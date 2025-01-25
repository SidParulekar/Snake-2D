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

    protected void Destroy()
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
        if (livesController.getlives() > 0)
        {
            livesController.DecreaseLives(1);
        }
    }

   
}
