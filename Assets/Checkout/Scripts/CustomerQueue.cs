using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    private List<GameObject> _customers = new List<GameObject>();
    private int _currentQueueSize = 0;
    private List<Vector3> _queueSlots = new List<Vector3>();
    
    public delegate void CustomerServiceAction(int money);
    public static event CustomerServiceAction OnCustomerServed;

    public int ClothesCost = 100;

    private void Start()
    {
        _queueSlots.Add(new Vector3(-9f, 1.5f, 15));
        _queueSlots.Add(new Vector3(-8f, 1.5f, 15));
        _queueSlots.Add(new Vector3(-7, 1.5f, 15));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer")
        {
            PlaceCustomerInQueue(other.gameObject);
        }

        if (other.tag == "Player")
        {
            ServeCustomers();
        }
    }

    private void ServeCustomers()
    {
        foreach (var customer in _customers)
        {
            if (OnCustomerServed != null)
            {
                OnCustomerServed(ClothesCost);
            }
            //TODO: Money should be in checkout
            customer.GetComponent<Customer>().GoOut();
        }
        _customers = new List<GameObject>();
        _currentQueueSize = 0;
    }

    private void MoneyToCornerAnimation()
    {
        var position3d = Camera.main.ScreenToWorldPoint(transform.position);
    }

    private void PlaceCustomerInQueue(GameObject newCustomer)
    {
        if (_currentQueueSize == _queueSlots.Count)
        {
            Debug.Log("Can't place new customer, queue is full");
            return;
        }

        Customer customer = newCustomer.GetComponent<Customer>();
        if (customer.hasClothes)
        {
            _customers.Add(newCustomer);
            newCustomer.GetComponent<Customer>().WaitInQueue(_queueSlots[_currentQueueSize]);
            _currentQueueSize++;
        }
    }
}
