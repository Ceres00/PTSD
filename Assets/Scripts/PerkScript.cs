using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkScript : PassengerScript
{
    public GameObject perkMenu;
    private bool isGamePaused = false;
    private bool isPerkMenuActive = false;

    public Button Perk1;
    public Button Perk2;
    public Button Perk3;


    void Awake()
    {
        perkMenu.SetActive(false);
        Perk1.onClick.AddListener(FirstPerk);
        Perk2.onClick.AddListener(SecondPerk);
        Perk3.onClick.AddListener(ThirdPerk);
    }

    void Update()
    {
        if (!isPerkMenuActive && completedTransportations >= 4)
        {
            PauseGame();
            ShowPerkMenu();
        }
    }
    void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }
    void ShowPerkMenu()
    {
        isPerkMenuActive = true;
        perkMenu.SetActive(true);
        completedTransportations = 0;
    }
    void ChoosePerk()
    {
        perkMenu.SetActive(false);
        isPerkMenuActive = false;
        ResumeGame();
    }
    void FirstPerk()
    {
        JumpMult = 1;
        SpeedMultiplier = 1;
        TimerScale = 0;
        MoneyMultiplier = 2;
        ChoosePerk();
    }
    void SecondPerk()
    {
        JumpMult = 1;
        SpeedMultiplier = 1;
        TimerScale = 20;
        timeRemaining = timeRemaining + 20;
        MoneyMultiplier = 1;
        ChoosePerk();
    }
    void ThirdPerk()
    {
        JumpMult = 1.5f;
        SpeedMultiplier = 1.5f;
        TimerScale = 0;
        MoneyMultiplier = 1;
        ChoosePerk();
    }
}
