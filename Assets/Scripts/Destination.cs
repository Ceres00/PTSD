using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private PassengerScript t;
    private void Start()
    {
        t = FindObjectOfType<PassengerScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (t.isTransporting)
            {
                t.passengers[0].SetActive(true);
                t.destinations[0].SetActive(false);
                t.isTransporting = false;
                t.GiveMoney();
            }
        }
    }
}
