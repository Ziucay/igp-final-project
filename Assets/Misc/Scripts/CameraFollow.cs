using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FollowTarget;

    private Vector3 _offset;
    private float _fixedY;

    private void Awake()
    {
        Vector3 initialPosition = transform.position;
        _offset = initialPosition - FollowTarget.position;
        _fixedY = initialPosition.y;
    }

    private void Start()
    {
        if (FollowTarget == null)
        {
            FollowTarget = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
    }

    private void LateUpdate()
    {
        Vector3 newPosition = FollowTarget.position + _offset;
        newPosition.y = _fixedY;
        transform.position = newPosition;
    }
}