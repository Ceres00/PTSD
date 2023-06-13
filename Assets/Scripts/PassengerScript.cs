using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerScript : MonoBehaviour
{
    public GameObject[] passengers;
    public GameObject[] destinations;
    private bool isTransporting = false;

    public int completedTransportationsPerk = 0;
    public int completedTransportationsNerf = 0;
    public bool Change = false;

    private Manager manager;

    private void Start()
    {
        passengers = GameObject.FindGameObjectsWithTag("Passenger");
        destinations = GameObject.FindGameObjectsWithTag("Destination");
        RandomizePassengersAndDestinations();
    }

    private void RandomizePassengersAndDestinations()
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

    private void OnTriggerEnter(Collider other)
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

    private void GiveMoney()
    {
        float moneyEarned = manager.timeRemaining * 2f;
        manager.money += moneyEarned;
        Debug.Log("Earned money: " + moneyEarned);

        if (Change)
        {
            completedTransportationsPerk++;
        }
        else
        {
            completedTransportationsNerf++;
        }

        RandomizePassengersAndDestinations();
    }

    private void Shuffle(GameObject[] array)
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