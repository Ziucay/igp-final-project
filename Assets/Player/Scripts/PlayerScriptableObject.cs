using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerScriptableObject : ISerializable
{
    public float X;
    public float Y;
    public float Z;

    public Vector3 ToVector()
    {
        return new Vector3(X, Y, Z);
    }

    public PlayerScriptableObject(Vector3 playerPosition)
    {
        X = playerPosition.x;
        Y = playerPosition.y;
        Z = playerPosition.z;
    }
}



