using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupStatus : MonoBehaviour
{
    private TextMeshProUGUI powerupStatus;

    private void Awake()
    {
        powerupStatus = GetComponent<TextMeshProUGUI>();
    }

    public void RefreshUI(string powerup, string status)
    {
        powerupStatus.text = powerupStatus.text + powerup + " " + status;

        switch (powerup)
        {
            case "Shield":
                powerupStatus.color = new Color32(8, 150, 255, 255);
                break;

            case "Score Boost":
                powerupStatus.color = new Color32(230, 255, 8, 255);
                break;
        }
    }
}
