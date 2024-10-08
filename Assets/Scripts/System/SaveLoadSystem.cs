using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private string _localSaveDirectory;
    [SerializeField] private string _fileName;

    private string _saveDirectory;

    public event UnityAction DataLoaded;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    private void Awake()
    {
        transform.parent = null;
    }

    public void SaveAll()
    {
        var data = GetDataObject();
        SaveAllData(ref data);

#if UNITY_EDITOR
        SaveFile(data);
#endif

#if UNITY_WEBGL
        SaveExtern(JsonUtility.ToJson(data));
#endif
    }

    public void Load()
    {
        _saveDirectory = Application.persistentDataPath + _localSaveDirectory;

#if UNITY_EDITOR
        var data = LoadFile();
        LoadData(data);
#endif

#if UNITY_WEBGL
        LoadExtern();
#endif
    }

    private SaveData GetDataObject()
    {
        SaveData data = new SaveData();

        if (File.Exists(_saveDirectory + _fileName) == false)
            data.objectsData = new List<ObjectData>();
        else
            data = LoadFile();

        return data;
    }

    private void SaveFile(SaveData data)
    {
        if (Directory.Exists(_saveDirectory) == false)
            Directory.CreateDirectory(_saveDirectory);

        File.WriteAllText(_saveDirectory + _fileName, JsonUtility.ToJson(data));
    }

    private SaveData LoadFile()
    {
        if (File.Exists(_saveDirectory + _fileName) == false)
            return new SaveData() { objectsData = new List<ObjectData>() };

        return JsonUtility.FromJson<SaveData>(File.ReadAllText(_saveDirectory + _fileName));
    }

    private void SaveAllData(ref SaveData data)
    {
        List<ObjectData> newData = new List<ObjectData>();

        foreach (var saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
        {
            var objectData = new ObjectData()
            {
                typeName = saveable.GetType().ToString(),
                jsonData = JsonUtility.ToJson(saveable.SaveState())
            };

            newData.Add(objectData);
        }

        data.objectsData = newData.Concat(data.objectsData.Where(state => newData.All(newState => newState.typeName != state.typeName))).ToList();
    }

    private void SaveObjectData(ref SaveData data, ISaveable saveable)
    {
        var stateIndex = data.objectsData.FindIndex(statesData => statesData.typeName == saveable.GetType().ToString());
        var newData = new ObjectData
        {
            typeName = saveable.GetType().ToString(),
            jsonData = JsonUtility.ToJson(saveable.SaveState())
        };

        if (stateIndex == -1)
            data.objectsData.Add(newData);
        else
            data.objectsData[stateIndex] = newData;
    }

    private void LoadData(SaveData data)
    {
        foreach (var saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
        {
            if (data.objectsData.Count != 0)
            {
                var stateIndex = data.objectsData.FindIndex(state => state.typeName == saveable.GetType().ToString());

                if (stateIndex == -1)
                    saveable.LoadByDefault();
                else
                    saveable.LoadState(data.objectsData[stateIndex].jsonData);
            }
            else
            {
                saveable.LoadByDefault();
            }
        }

        DataLoaded?.Invoke();
    }

    private void LoadExternData(string jsonData)
    {
        LoadData(JsonUtility.FromJson<SaveData>(jsonData));
    }

    [Serializable]
    private struct SaveData
    {
        public List<ObjectData> objectsData;
    }

    [Serializable]
    private struct ObjectData
    {
        public string typeName;
        public string jsonData;
    }
}
