using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkScript : PassengerScript
{
    public GameObject perkMenu;
    private bool isGamePaused = false;
    private bool isPerkMenuActive = false;

    void Awake()
    {
        perkMenu.SetActive(false);
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
}
