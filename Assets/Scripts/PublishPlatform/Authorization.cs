using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Authorization : MonoBehaviour
{
    /*[SerializeField] private YesNoPopupWindow _window;
    [SerializeField] private Session _session;
    [SerializeField] private int _sessionCountToReview;
    [SerializeField] private SaveLoadSystem _saveLoadSystem;

    private int _authorizationSessionNumber;
    private int _currentSessionNumber;

    public event UnityAction PlayerAuthorized;

    [DllImport("__Internal")]
    private static extern int RequestAuthorizationStatus();

    [DllImport("__Internal")]
    private static extern void AuthorizeToTheGame();

    private void OnEnable()
    {
        _window.ConfirmClick += OnWindowConfirmClick;
        _saveLoadSystem.DataLoaded += OnDataLoaded;
    }

    private void OnDisable()
    {
        _window.ConfirmClick -= OnWindowConfirmClick;
        _saveLoadSystem.DataLoaded -= OnDataLoaded;
        _session.SessionActivityChanged -= OnSessionActivityChanged;
    }

    private void OnWindowConfirmClick()
    {
        AuthorizeToTheGame();
    }

    private void OnDataLoaded()
    {
        bool isPlayerAuthorized = Convert.ToBoolean(RequestAuthorizationStatus());

        if (isPlayerAuthorized)
        {
            PlayerAuthorized?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            _session.SessionActivityChanged += OnSessionActivityChanged;
            _authorizationSessionNumber = _session.Number + _sessionCountToReview;
            Debug.Log(_authorizationSessionNumber);
        }
    }

    private void OnSessionActivityChanged(bool isActive)
    {
        if (_session.Number > _currentSessionNumber)
        {
            _currentSessionNumber = _session.Number;
            Debug.Log(_currentSessionNumber);

            if (_currentSessionNumber == _authorizationSessionNumber)
            {
                bool isPlayerAuthorized = Convert.ToBoolean(RequestAuthorizationStatus());

                if (isPlayerAuthorized)
                {
                    PlayerAuthorized?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    _window.gameObject.SetActive(true);
                }
            }
        }
    }*/
}
