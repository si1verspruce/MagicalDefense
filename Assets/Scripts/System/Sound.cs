using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour, ISaveable
{
    [SerializeField] private Toggle[] _soundToggles;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] private bool _isSoundOnByDefault;

    private bool _isSoundOn;

    private void OnEnable()
    {
        foreach (var toggle in _soundToggles)
            toggle.onValueChanged.AddListener(ToggleVolumeWithSave);
    }

    private void OnDisable()
    {
        foreach (var toggle in _soundToggles)
            toggle.onValueChanged.RemoveListener(ToggleVolumeWithSave);
    }

    public void LoadState(string saveData)
    {
        var data = JsonUtility.FromJson<SaveData>(saveData);
        _isSoundOn = data.isSoundOn;

        foreach (var toggle in _soundToggles)
            toggle.isOn = _isSoundOn;
    }

    public void LoadByDefault()
    {
        ToggleVolume(_isSoundOnByDefault);
    }

    public object SaveState()
    {
        var data = new SaveData() { isSoundOn = _isSoundOn };

        return data;
    }

    private void ToggleVolume(bool isOn)
    {
        AudioListener.pause = !isOn;
        _isSoundOn = isOn;

        foreach (var toggle in _soundToggles)
            toggle.isOn = isOn;
    }

    private void ToggleVolumeWithSave(bool isOn)
    {
        ToggleVolume(isOn);
        _saveLoad.Save(this);
    }

    [Serializable]
    private struct SaveData
    {
        public bool isSoundOn;
    }
}
