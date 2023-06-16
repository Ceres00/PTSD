using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerScript : MonoBehaviour
{
    public GameObject[] passengers;
    public GameObject[] destinations;
    public GameObject[] packages;
    public bool isTransporting = false;
    public bool isPackageActive = false;

    public int completedTransportationsPerk = 0;
    public int completedTransportationsNerf = 0;
    public bool Change = false;

    private Manager manager;
    private Coroutine hurryCoroutine;

    private void Start()
    {
        passengers = GameObject.FindGameObjectsWithTag("Passenger");
        destinations = GameObject.FindGameObjectsWithTag("Destination");
        packages = GameObject.FindGameObjectsWithTag("Package");
        RandomizePassengersAndDestinations();

        manager = FindObjectOfType<Manager>();
    }

    private void RandomizePassengersAndDestinations()
    {
        Shuffle(passengers);
        Shuffle(destinations);
        Shuffle(packages);

        foreach (GameObject passenger in passengers)
        {
            passenger.SetActive(false);
        }

        foreach (GameObject destination in destinations)
        {
            destination.SetActive(false);
        }

        foreach (GameObject package in packages)
        {
            package.SetActive(false);
        }

        passengers[0].SetActive(true);

        if (!isTransporting)
        {
            hurryCoroutine = StartCoroutine(Hurry());
        }
    }

    IEnumerator Hurry()
    {
        yield return new WaitForSeconds(10f);

        if (!isTransporting)
        {
            passengers[0].SetActive(false);
            packages[0].SetActive(false);
            RandomizePassengersAndDestinations();
        }
    }

    private void PackageDelivery()
    {
        Shuffle(passengers);
        Shuffle(destinations);
        Shuffle(packages);

        foreach (GameObject passenger in passengers)
        {
            passenger.SetActive(false);
        }

        foreach (GameObject destination in destinations)
        {
            destination.SetActive(false);
        }

        foreach (GameObject package in packages)
        {
            package.SetActive(false);
        }

        packages[0].SetActive(true);

        if (!isTransporting)
        {
            hurryCoroutine = StartCoroutine(Hurry());
        }
    }

    public void GiveMoney()
    {
        float moneyEarned = manager.timeRemaining * 2f;
        manager.money += moneyEarned;
        Debug.Log("Earned money: " + moneyEarned);

        if (!Change)
        {
            completedTransportationsPerk++;
        }
        else
        {
            completedTransportationsNerf++;
        }

        if (hurryCoroutine != null)
        {
            StopCoroutine(hurryCoroutine);
        }

        RandomizePassengersAndDestinations();
    }

    public void GivePackage()
    {
        float moneyEarned = manager.timeRemaining * 2f;
        manager.money += moneyEarned;
        Debug.Log("Earned money: " + moneyEarned);

        isPackageActive = true;

        if (hurryCoroutine != null)
        {
            StopCoroutine(hurryCoroutine);
        }

        PackageDelivery();
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
