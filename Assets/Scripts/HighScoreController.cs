using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    private int highScore;

    public static HighScoreController instance;

    public static HighScoreController Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void SetHighScore(int currentScore)
    {
       if(highScore < currentScore)
        {
            highScore = currentScore;
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }


}
