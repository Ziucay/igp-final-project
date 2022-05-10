using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesStand : MonoBehaviour, IMemorable
{

    public int CurrentClothesAmount = 0;
    public int MaxClothesAmount = 4;

    public GameObject[] ClothSlots;

    public bool isBought = false;

    private bool _isPlayerInTrigger = false;

    public TimeAnimation TimeAnimation;
    public GameObject BuyFrame;
    private void OnTriggerEnter(Collider other)
    {

        if (isBought)
        {
            HandleInteractionThenBought(other);
        }
        else
        {
            HandleInteractionThenNotBought(other);
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerInTrigger = false;
        }
    }

    private void HandleInteractionThenBought(Collider other)
    {
        if (other.tag == "Player")
        {
            int playerClothes = other.GetComponent<ClothesCarry>().CurrentClothesCount;
            if (playerClothes > 0)
            {
                int clothesToPut = Math.Min(MaxClothesAmount - CurrentClothesAmount, playerClothes);
                for (int i = 0; i < clothesToPut; i++)
                {
                    AddClothOnStand();
                }
                other.GetComponent<ClothesCarry>().DeleteClothes(clothesToPut);
            }
        }

        if (other.tag == "Customer")
        {
            if (!other.GetComponent<Customer>().hasClothes)
            {
                if (CurrentClothesAmount > 0)
                {
                    Debug.Log("Take clothes");
                    other.GetComponent<Customer>().TakeClothes();
                    DeleteClothOnStand();
                }
                else
                {
                    other.GetComponent<Customer>().CallFindClothesCoroutine();
                }
            }
        }
    }

    private void AddClothOnStand()
    {
        Debug.Log("Add cloth");
        ClothSlots[CurrentClothesAmount].SetActive(true);
        CurrentClothesAmount++;
    }
    
    private void DeleteClothOnStand()
    {
        Debug.Log("Delete cloth");
        CurrentClothesAmount--;
        ClothSlots[CurrentClothesAmount].SetActive(false);
    }

    private void HandleInteractionThenNotBought(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerInTrigger = true;
            StartCoroutine(BuyStand());
        }
    }

    private IEnumerator BuyStand()
    {

        MoneyManager moneyManager = GameObject.FindGameObjectWithTag("MoneyManager").GetComponent<MoneyManager>();
        TimeAnimation.StartAnimation(3);
        float RemainingTime = 3;
        while (RemainingTime > 0 && _isPlayerInTrigger && moneyManager.GetMoney() >= 300)
        {
            RemainingTime -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
        TimeAnimation.Reset();
        if (_isPlayerInTrigger && RemainingTime <= 0 && moneyManager.GetMoney() >= 300)
        {
            SwitchToBought();
            moneyManager.DecreaseMoney(300);
        }
    }

    private void SwitchToBought()
    {
        isBought = true;
        BuyFrame.SetActive(false);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    
    private void SwitchToNotBought()
    {
        isBought = false;
        BuyFrame.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
    
    public ISerializable SaveToMemento()
    {
        var obj = new ClothesStandMemento(transform.position,transform.rotation, isBought);
        return obj;
    }

    public void RestoreFromMemento(ISerializable memento)
    {
        if (memento.GetType() != typeof(ClothesStandMemento))
            throw new System.ArgumentException("Incorrect type of memento object");

        transform.position = ((ClothesStandMemento) memento).PositionToVector();
        transform.rotation = ((ClothesStandMemento) memento).RotationToQuaternion();
        isBought = ((ClothesStandMemento) memento).IsBought;
        if (isBought)
        {
            SwitchToBought();   
        }
        else
        {
            SwitchToNotBought();
        }
    }
}
