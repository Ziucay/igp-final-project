using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MoneyManager : MonoBehaviour, IMemorable
{

    public TextMeshProUGUI MoneyCounter;

    private int _currentMoney = 0;

    private void Start()
    {
        CustomerQueue.OnCustomerServed += AddMoney;
        MoneyCounter.text = "Money: " + _currentMoney;
    }

    public void AddMoney(int money)
    {
        _currentMoney += money;
        MoneyCounter.text = "Money: " + _currentMoney;
    }

    public void DecreaseMoney(int money)
    {
        _currentMoney -= money;
        MoneyCounter.text = "Money: " + _currentMoney;
    }

    public int GetMoney()
    {
        return _currentMoney;
    }
    
    public ISerializable SaveToMemento()
    {
        var obj = new MoneyMemento(_currentMoney);
        return obj;
    }

    public void RestoreFromMemento(ISerializable memento)
    {
        if (memento.GetType() != typeof(MoneyMemento))
            throw new System.ArgumentException("Incorrect type of memento object");

        _currentMoney = ((MoneyMemento) memento).money;
    }
}
