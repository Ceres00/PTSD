using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerScript : Manager
{
    private float money;
    public GameObject passengerObject;
    public GameObject destinationObject;
    private bool isTransporting = false;

    void Awake()
    {
        passengerObject = GameObject.FindGameObjectWithTag("Passenger");
        destinationObject = GameObject.FindGameObjectWithTag("Destination");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (passengerObject.activeSelf)
            {
                passengerObject.SetActive(false);
                destinationObject.SetActive(true);
                isTransporting = true;
            }
            else if (destinationObject.activeSelf)
            {
                passengerObject.SetActive(true);
                destinationObject.SetActive(false);
                isTransporting = false;
                GiveMoney();
            }
        }
    }

    void GiveMoney()
    {
        float moneyEarned = timeRemaining * 2f; 
        money += moneyEarned;
        Debug.Log("Earned money: " + moneyEarned);
    }
}
