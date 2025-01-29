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
    [SerializeField] private Snake SnakeOneController;
    [SerializeField] private SnakeTwo SnakeTwoController;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseGameScreen;
    


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

        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseGameScreen.activeInHierarchy)
            {
                PauseGame();
            }

            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseGameScreen.SetActive(true);
        foodController.enabled = false;
        poisonController.enabled = false;
        powerupController.enabled = false;

        if(SnakeOneController)
        {
            SnakeOneController.enabled = false;
        }

        if(SnakeTwoController)
        {
            SnakeTwoController.enabled = false;
        }
    }

    public void ResumeGame()
    {
        pauseGameScreen.SetActive(false);
        foodController.enabled = true;
        poisonController.enabled = true;
        powerupController.enabled = true;

        if (SnakeOneController)
        {
            SnakeOneController.enabled = true;
        }

        if (SnakeTwoController)
        {
            SnakeTwoController.enabled = true;
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
