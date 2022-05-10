using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMemento : ISerializable
{
    public float X;
    public float Y;
    public float Z;

    public Vector3 ToVector()
    {
        return new Vector3(X, Y, Z);
    }

    public PlayerMemento(Vector3 playerPosition)
    {
        X = playerPosition.x;
        Y = playerPosition.y;
        Z = playerPosition.z;
    }
}