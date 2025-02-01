using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private LivesController playerOneLivesController;
    [SerializeField] private LivesController playerTwoLivesController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private Consumable foodController;
    [SerializeField] private Consumable poisonController;
    [SerializeField] private Powerup powerupController;
    [SerializeField] private Snake SnakeOneController;
    [SerializeField] private SnakeTwo SnakeTwoController;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseGameScreen;
    [SerializeField] private GameObject winResult;
    [SerializeField] private GameObject highScore;


    void Update()
    {
        
        if(playerOneLivesController.getlives()==0)
        {
            EndGame();
        }

        if(playerTwoLivesController)
        {
            if(playerTwoLivesController.getlives()==0)
            {
                EndGame();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
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

    private void EndGame()
    {
        foodController.enabled = false;
        poisonController.enabled = false;
        powerupController.enabled = false;

        if (!playerTwoLivesController)
        {
            HighScoreController.Instance.SetHighScore(scoreController.GetScore());
        }
            
        gameOverUI.SetActive(true);

        if(playerTwoLivesController)
        {
            SnakeOneController.enabled = false;
            SnakeTwoController.enabled = false;
            winResult.SetActive(true);
            highScore.SetActive(false);
        }

        

        
    }

    private void PauseGame()
    {
        pauseGameScreen.SetActive(true);
        foodController.enabled = false;
        poisonController.enabled = false;
        powerupController.enabled = false;

        SnakeOneController.enabled = false;     

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
        
        SnakeOneController.enabled = true;
        
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
