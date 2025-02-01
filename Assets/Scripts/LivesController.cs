using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    private TextMeshProUGUI livesText;
    [SerializeField] private int playerLives = 5;

    private void Awake()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    public void DecreaseLives(int decrement)
    {
        playerLives -= decrement;
        RefreshUI();
    }

    public int getlives()
    {
        return playerLives;
    }

    private void RefreshUI()
    {
        livesText.text = "Lives: " + playerLives;
    }
}
