using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestructor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer")
        {
            Destroy(other.gameObject);
        }
    }
}
