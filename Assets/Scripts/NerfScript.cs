using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NerfScript : MonoBehaviour
{
    public GameObject nerfMenu;
    private bool isNerfMenuActive = false;

    public Button Nerf1;
    public Button Nerf2;
    public Button Nerf3;

    private PassengerScript passengerScript;
    private Manager manager;
    private PlayerMovement playerMovement;

    void Awake()
    {
        nerfMenu.SetActive(false);
        Nerf1.onClick.AddListener(FirstNerf);
        Nerf2.onClick.AddListener(SecondNerf);
        Nerf3.onClick.AddListener(ThirdNerf);
        Nerf2.interactable = false;
        Nerf3.interactable = false;
    }
    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        passengerScript = FindObjectOfType<PassengerScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!isNerfMenuActive && passengerScript.completedTransportationsNerf >= 4)
        {
            PauseGame();
            ShowNerfMenu();
        }
        if (manager.money > 100)
        {
            Nerf2.interactable = true;
        }
        else
        {
            Nerf2.interactable = false;
        }
        
        if (manager.money > 200)
        {
            Nerf3.interactable = true;
        }
        else
        {
            Nerf3.interactable = false;
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        passengerScript.Change = !passengerScript.Change;
    }
    void ShowNerfMenu()
    {
        isNerfMenuActive = true;
        nerfMenu.SetActive(true);
        passengerScript.completedTransportationsNerf = 0;
    }
    void ChooseNerf()
    {
        nerfMenu.SetActive(false);
        isNerfMenuActive = false;
        ResumeGame();
    }
    public void FirstNerf()
    {
        playerMovement.SpeedMultiplier = 1;
        manager.TimerScale = 0;
        manager.MoneyMultiplier = 0.5f;
        ChooseNerf();
    }
    public void SecondNerf()
    {
        playerMovement.SpeedMultiplier = 1;
        manager.TimerScale = -20;
        manager.timeRemaining = manager.timeRemaining + manager.TimerScale;
        manager.MoneyMultiplier = 1;
        manager.money = manager.money - 100;
        ChooseNerf();
    }
    public void ThirdNerf()
    {
        playerMovement.SpeedMultiplier = 0.8f;
        manager.TimerScale = 0;
        manager.MoneyMultiplier = 1;
        manager.money = manager.money - 200;
        ChooseNerf();
    }
}