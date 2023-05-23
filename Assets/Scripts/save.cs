using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class save : MonoBehaviour
{
    public EventManager _eventManager;
    public GameObject player;
    /*

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
                    + "/MySaveData.dat"); 
        SaveData data = new SaveData();
        Dictionary<string, bool> _flags = _eventManager.GetFlags();

        bool [] vals = new bool [_flags.Count];
        int i = 0;
        foreach (var pair in _flags)
        {   
            vals[i] = pair.Value;
            i++;
        }

        data.flags = vals;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }

    
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath 
                    + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                    File.Open(Application.persistentDataPath 
                    + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _eventManager.SetFlags(data.flags);
            player.transform.position = new Vector3(-3,-3.5f,0);
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    */
}


[Serializable]
class SaveData
{
    // public Dictionary<string, bool> flags;
    public bool [] flags;
}
    