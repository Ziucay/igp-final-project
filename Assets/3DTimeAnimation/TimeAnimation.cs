using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeAnimation : MonoBehaviour
{

    public GameObject objectToScale;
    public GameObject background;

    public void StartAnimation(float animationTime)
    {
        objectToScale.SetActive(true);
        background.SetActive(true);

        Vector3 localScale = objectToScale.transform.localScale;

        objectToScale.transform.localScale = new Vector3(localScale.x, localScale.y, 0);

        objectToScale.transform.DOScaleZ(1, animationTime);
        
    }

    public void Reset()
    {
        objectToScale.SetActive(false);
        background.SetActive(false);
    }
}
