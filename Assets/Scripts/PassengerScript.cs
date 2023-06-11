using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerScript : Manager
{
    public GameObject[] passengers;
    public GameObject[] destinations;
    private bool isTransporting = false;

    public int completedTransportations = 0;

    void Awake()
    {
        passengers = GameObject.FindGameObjectsWithTag("Passenger");
        destinations = GameObject.FindGameObjectsWithTag("Destination");
    }

    void Start()
    {
        RandomizePassengersAndDestinations();
    }

    void RandomizePassengersAndDestinations()
    {
        Shuffle(passengers);
        Shuffle(destinations);

        foreach (GameObject passenger in passengers)
        {
            passenger.SetActive(false);
        }

        foreach (GameObject destination in destinations)
        {
            destination.SetActive(false);
        }

        passengers[0].SetActive(true);
        destinations[0].SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isTransporting)
            {
                passengers[0].SetActive(true);
                destinations[0].SetActive(false);
                isTransporting = false;
                GiveMoney();
            }
            else
            {              
                passengers[0].SetActive(false);
                destinations[0].SetActive(true);
                isTransporting = true;
            }
        }
    }

    void GiveMoney()
    {
        float moneyEarned = timeRemaining * 2f; 
        money += moneyEarned;
        Debug.Log("Earned money: " + moneyEarned);

        completedTransportations++;
        RandomizePassengersAndDestinations();
    }

    void Shuffle(GameObject[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            n--;
            GameObject temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}