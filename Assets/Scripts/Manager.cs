using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public float MoneyMultiplier = 1;
    public float TimerScale = 0;


    public float money = 0;
    public Text timerText;
    public Text moneyText;
    public float timeRemaining = 20;


    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int timeText = (int)timeRemaining;
            timerText.text = timeRemaining.ToString();
        }
        else
        {
            timeRemaining = 0;
            // Zaman doldu game over UI þeyleri
        }
        moneyText.text = "Money: " + money + "$";
    }
}
