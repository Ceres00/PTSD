using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
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
            t.passengers[0].SetActive(false);
            t.destinations[0].SetActive(true);
            t.isTransporting = true;
        }
    }
}
