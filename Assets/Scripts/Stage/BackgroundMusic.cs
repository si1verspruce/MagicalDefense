using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private MenuScreen _menu;
    [SerializeField] private AudioSource _audio;

    private void OnEnable()
    {
        _menu.Activated += OnMenuActivated;
    }

    private void OnDisable()
    {
        _menu.Activated -= OnMenuActivated;
    }

    private void Start()
    {
        if (_menu.gameObject.activeSelf == false)
            _audio.Play();
    }

    private void OnMenuActivated(bool isActive)
    {
        if (isActive)
            _audio.Stop();
        else
            _audio.Play();
    }
}
