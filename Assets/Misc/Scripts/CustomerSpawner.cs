using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{

    public float CustomerSpawnDuration = 10;

    public Vector3 CustomerSpawnPoint;

    public GameObject Customer;

    private void Start()
    {
        StartCoroutine(TrySpawnCustomer());
    }

    private IEnumerator TrySpawnCustomer()
    {
        var currentCustomerAmount = GameObject.FindGameObjectsWithTag("Customer").Length;

        if (currentCustomerAmount < 3)
        {
            Instantiate(Customer, CustomerSpawnPoint, Quaternion.identity);
        }

        yield return new WaitForSeconds(10);
        StartCoroutine(TrySpawnCustomer());
    }
}
