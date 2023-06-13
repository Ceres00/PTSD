using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkScript : MonoBehaviour
{
    public GameObject perkMenu;
    private bool isGamePaused = false;
    private bool isPerkMenuActive = false;

    public Button Perk1;
    public Button Perk2;
    public Button Perk3;

    private PassengerScript passengerScript;
    private Manager manager;
    private PlayerMovement playerMovement;

    void Awake()
    {
        perkMenu.SetActive(false);
        Perk1.onClick.AddListener(FirstPerk);
        Perk2.onClick.AddListener(SecondPerk);
        Perk3.onClick.AddListener(ThirdPerk);
        Perk2.interactable = false;
        Perk3.interactable = false;
    }
    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        passengerScript = FindObjectOfType<PassengerScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!isPerkMenuActive && passengerScript.completedTransportationsPerk >= 4)
        {
            PauseGame();
            ShowPerkMenu();
        }

        if (manager.money > 100)
        {
            Perk2.interactable = true;
        }
        else
        {
            Perk2.interactable = false;
        }

        if (manager.money > 200)
        {
            Perk3.interactable = true;
        }
        else
        {
            Perk3.interactable = false;
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
        passengerScript.completedTransportationsPerk = 0;
    }
    void ChoosePerk()
    {
        perkMenu.SetActive(false);
        isPerkMenuActive = false;
        passengerScript.Change = !passengerScript.Change;
        ResumeGame();
    }
    public void FirstPerk()
    {
        playerMovement.SpeedMultiplier = 1;
        manager.TimerScale = 0;
        manager.MoneyMultiplier = 2;
        ChoosePerk();
    }
    public void SecondPerk()
    {
        playerMovement.SpeedMultiplier = 1;
        manager.TimerScale = 20;
        manager.timeRemaining = manager.timeRemaining + manager.TimerScale;
        manager.MoneyMultiplier = 1;
        manager.money -= 100;
        ChoosePerk();
    }
    public void ThirdPerk()
    {
        playerMovement.SpeedMultiplier = 1.2f;
        manager.TimerScale = 0;
        manager.MoneyMultiplier = 1;
        manager.money -= 200;
        ChoosePerk();
    }
}
