using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public float TimeToGetClothes;

    public TimeAnimation animation;
    //TODO: Make changeable via event

    private float TimeRemainingToGetClothes;

    private bool _isPlayerInTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerInTrigger = true;
            ClothesCarry carry = other.GetComponent<ClothesCarry>();
            if (carry.CurrentClothesCount < carry.MaxClotthesCount)
                StartCoroutine(GetClothes(carry));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPlayerInTrigger = false;
        }
    }


    IEnumerator GetClothes(ClothesCarry carry)
    {
        animation.StartAnimation(TimeToGetClothes);
        TimeRemainingToGetClothes = TimeToGetClothes;
        while (TimeRemainingToGetClothes > 0 && _isPlayerInTrigger)
        {
            TimeRemainingToGetClothes -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        animation.Reset();
        if (_isPlayerInTrigger && TimeRemainingToGetClothes <= 0)
        {
            carry.AddClothes(1);
            if (carry.CurrentClothesCount < carry.MaxClotthesCount)
                StartCoroutine(GetClothes(carry));
        }
        
    }
}
