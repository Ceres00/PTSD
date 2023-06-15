using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
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
            if (!t.isTransporting && t.isPackageActive) 
            {
                t.packages[0].SetActive(false);
                t.destinations[0].SetActive(true);
                t.isTransporting = true;
                t.isPackageActive = true; 

                Debug.Log("Package activated!");
            }
        }
    }
}
