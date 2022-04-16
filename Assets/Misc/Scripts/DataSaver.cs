using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    private static HashSet< (String,ISerializable) > _mementoObjects;

    public GameObject Player;

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        if (!File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            Debug.Log("It goes in save");
            SaveState();
            SaveGame();
        }
        else
        {
            LoadGame();
            LoadState();
        }
    }

    private void SaveState()
    {
        var objects = FindObjectsOfType<MonoBehaviour>().OfType<IMemorable>();

        _mementoObjects = new HashSet< (String,ISerializable) >();
        foreach (var obj in objects)
        {
            _mementoObjects.Add( ((obj as MonoBehaviour).gameObject.tag, obj.SaveToMemento()) );
        }
    }

    private void LoadState()
    {
        if (_mementoObjects == null)
        {
            Debug.Log("Nothing to be loaded.");
            return;
        }

        foreach (var memento in _mementoObjects)
        {
            GameObject current = GameObject.FindGameObjectWithTag(memento.Item1);
            current.GetComponent<IMemorable>().RestoreFromMemento(memento.Item2);
        }
    }

    private void SaveGame()
    {
        Debug.Log("Saving game...");
        Save save = new Save(_mementoObjects);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }
    
    private void LoadGame()
    {
        Debug.Log("Loading game...");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = 
            File.Open(Application.persistentDataPath + "/gamesave.save",FileMode.Open);

        Save save = (Save)bf.Deserialize(file);
        _mementoObjects = save.obj;
        file.Close();
    }

    private void OnApplicationQuit()
    {
        SaveState();
        SaveGame();
    }

    [Serializable]
    public class Save
    {
        public HashSet<(String, ISerializable)> obj;

        public Save(HashSet<(String, ISerializable)> obj)
        {
            this.obj = obj;
        }
    }
}
