using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoneyMemento : ISerializable
{
    public int money;
    
    public MoneyMemento(int money)
    {
        this.money = money;
    }
}
