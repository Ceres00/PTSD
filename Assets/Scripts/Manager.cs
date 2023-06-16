using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{
    public float MoneyMultiplier = 1;
    public float TimerScale = 0;


    public float money = 0;
    public Text timerText;
    public Text moneyText;
    public float timeRemaining = 20;

    public GameObject GameOver;


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
            GameOver.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        moneyText.text = "Money: " + money + "$";
    }
}
