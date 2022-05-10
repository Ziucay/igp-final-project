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
        List<KeyValuePair<string, GameObject>> list = new List<KeyValuePair<string, GameObject>>();
        
        var objects = FindObjectsOfType<MonoBehaviour>().OfType<IMemorable>();
        foreach (var obj in objects)
        {
            list.Add(new KeyValuePair<string, GameObject>((obj as MonoBehaviour).gameObject.tag, 
                                                                (obj as MonoBehaviour).gameObject));
        }

        foreach (var memento in _mementoObjects)
        {
            GameObject current = GetGameObjectFromList(memento.Item1, list);
            if (current != null)
                current.GetComponent<IMemorable>().RestoreFromMemento(memento.Item2);
        }
    }

    private GameObject GetGameObjectFromList(string tag, List<KeyValuePair<string, GameObject>> list)
    {
        GameObject result = null;
        
        foreach (var entry in list)
        {
            if (entry.Key == tag)
            {
                result = entry.Value;
                list.Remove(entry);
                break;
            }
        }

        if (result == null)
        {
            Debug.Log("Object with such tag not found");
        }

        return result;
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
