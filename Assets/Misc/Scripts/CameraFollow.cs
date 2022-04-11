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

    private void LateUpdate()
    {
        Vector3 newPosition = FollowTarget.position + _offset;
        newPosition.y = _fixedY;
        transform.position = newPosition;
    }
}