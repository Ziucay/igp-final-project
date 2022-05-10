using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ClothesStandMemento : ISerializable
{
    public float PositionX;
    public float PositionY;
    public float PositionZ;
    
    public float RotationX;
    public float RotationY;
    public float RotationZ;
    public float RotationW;

    public bool IsBought;
    
    public Vector3 PositionToVector()
    {
        return new Vector3(PositionX, PositionY, PositionZ);
    }
    
    public Quaternion RotationToQuaternion()
    {
        return new Quaternion(RotationX, RotationY, RotationZ, RotationW);
    }

    public ClothesStandMemento(Vector3 position, Quaternion rotation, bool isBought)
    {
        PositionX = position.x;
        PositionY = position.y;
        PositionZ = position.z;

        RotationX = rotation.x;
        RotationY = rotation.y;
        RotationZ = rotation.z;
        RotationW = rotation.w;

        IsBought = isBought;
    }
}
