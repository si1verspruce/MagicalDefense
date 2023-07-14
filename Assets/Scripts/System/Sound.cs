using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour, ISaveable
{
    [SerializeField] private Toggle[] _soundToggles;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] private bool _isSoundOnByDefault;
    [SerializeField] private AudioSource _backgroundSound;

    private bool _isSoundOn;
    private bool _isSoundChangeable = true;

    private void OnEnable()
    {
        foreach (var toggle in _soundToggles)
            toggle.onValueChanged.AddListener(ToggleSound);
    }

    private void OnDisable()
    {
        foreach (var toggle in _soundToggles)
            toggle.onValueChanged.RemoveListener(ToggleSound);
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
        ToggleSound(_isSoundOnByDefault);
    }

    public object SaveState()
    {
        var data = new SaveData() { isSoundOn = _isSoundOn };

        return data;
    }

    [ContextMenu("On")]
    public void UnPause()
    {
        _isSoundChangeable = true;
        ToggleSound(_isSoundOn);
    }

    [ContextMenu("Off")]
    public void Pause()
    {
        _isSoundChangeable = false;
        ToggleSound(false);
    }

    private void TryToSetSoundState(bool isSoundOn)
    {
        if (_isSoundChangeable)
            _isSoundOn = isSoundOn;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            _isSoundChangeable = true;
            ToggleSound(_isSoundOn);
        }
        else
        {
            _isSoundChangeable = false;
            ToggleSound(false);
        }
    }

    private void ToggleSound(bool isOn)
    {
        AudioListener.volume = Convert.ToSingle(isOn);
        TryToSetSoundState(isOn);

        if (isOn)
            _backgroundSound.UnPause();
        else
            _backgroundSound.Pause();

        foreach (var toggle in _soundToggles)
            toggle.isOn = isOn;
    }

    [Serializable]
    private struct SaveData
    {
        public bool isSoundOn;
    }
}
