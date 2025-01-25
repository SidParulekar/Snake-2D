using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private LivesController livesController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private Consumable foodController;
    [SerializeField] private Consumable poisonController;
    [SerializeField] private Powerup powerupController;
    [SerializeField] private GameObject gameOverUI;

    void Update()
    {
        if(livesController.getlives()==0)
        {
            foodController.enabled = false;
            poisonController.enabled = false;
            powerupController.enabled = false;
            HighScoreController.Instance.SetHighScore(scoreController.GetScore());
            gameOverUI.SetActive(true);
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
