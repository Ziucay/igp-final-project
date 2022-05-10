using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClothesCarry : MonoBehaviour
{
    public TextMeshProUGUI clothesUI;

    public int CurrentClothesCount = 0;

    public int MaxClotthesCount = 3;

    public void AddClothes(int clothes)
    {
        CurrentClothesCount = Math.Min(CurrentClothesCount + 1, MaxClotthesCount);
        clothesUI.text = "Clothes: " + CurrentClothesCount;
    }
    
    public void DeleteClothes(int clothes)
    {
        CurrentClothesCount = Math.Max(CurrentClothesCount - clothes, 0);
        clothesUI.text = "Clothes: " + CurrentClothesCount;
    }
}
