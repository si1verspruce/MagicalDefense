using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseForEnemies : MonoBehaviour, IResetOnRestart
{
    [SerializeField] private float _pauseDuration;
    [SerializeField] private int _decimalPlacesCount;
    [SerializeField] private TextMeshProUGUI _text;

    public void Pause()
    {
        OnTimeActivityChanged(false);
        _text.text = _pauseDuration.ToString();
        StartCoroutine(UnpauseEndFrame());
    }

    public void Reset()
    {
        OnTimeActivityChanged(true);
        StopAllCoroutines();
    }

    private IEnumerator UnpauseEndFrame()
    {
        float time = _pauseDuration;

        while (time > 0)
        {
            yield return null;

            time -= Time.deltaTime;
            _text.text = Math.Round((decimal)time, _decimalPlacesCount).ToString();
        }

        OnTimeActivityChanged(true);
    }

    private void OnTimeActivityChanged(bool isActive)
    {
        EnemyTime.SetTimeActivity(isActive);
        _text.gameObject.SetActive(!isActive);
    }
}
