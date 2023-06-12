using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public float MoneyMultiplier = 1;
    public float TimerScale = 0;
    public float SpeedMultiplier = 1;
    public float JumpMult = 1;

    public float money;
    public Text timerText;
    public Text moneyText;
    public float timeRemaining = 90f;



    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeRemaining = 0;
            // Zaman doldu game over UI �eyleri
        }
        moneyText.text = "Money: " + money + "$";
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = timeString;
    }
}
