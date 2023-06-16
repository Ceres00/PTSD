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
    public Text GameOverText;
    public float timeRemaining = 15;

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
            GameOverText.text = "Score: " + money + "\n" + "Go to Menu";
        }
        moneyText.text = "Money: " + money + "$";
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
