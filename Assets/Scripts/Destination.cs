using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private PassengerScript t;
    private Manager m;
    private void Start()
    {
        t = FindObjectOfType<PassengerScript>();
        m = FindObjectOfType<Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (t.isTransporting && !t.isPackageActive)
            {
                m.timeRemaining += 10;
                t.destinations[0].SetActive(false);
                t.isTransporting = false;
                float randomNumber = Random.Range(0f, 1f);
                if (randomNumber <= 0.2f)
                {
                    t.GivePackage();
                }
                else
                {
                    t.GiveMoney();
                    t.passengers[0].SetActive(true);
                }
            }

            else if (t.isTransporting && t.isPackageActive)
            {
                m.timeRemaining += 10;
                t.passengers[0].SetActive(true);
                t.destinations[0].SetActive(false);
                t.isTransporting = false;
                t.isPackageActive = false;
                t.GiveMoney();
            }
        }
    }
}
