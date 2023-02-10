using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ReviewGame : MonoBehaviour
{
    [SerializeField] private YesNoPopupWindow _window;
    [SerializeField] private Session _session;
    [SerializeField] private int _sessionCountToReview;

    private bool _isReviewAvailable;
    private int _reviewSessionNumber;
    private int _currentSessionNumber;

    [DllImport("__Internal")]
    private static extern void GetReviewAvailability();

    [DllImport("__Internal")]
    private static extern void OpenReviewWindow();

    private void OnEnable()
    {
        _window.ConfirmClick += OnWindowConfirmClick;
    }

    private void OnDisable()
    {
        _window.ConfirmClick -= OnWindowConfirmClick;
        _session.SessionActivityChanged -= OnSessionActivityChanged;
    }

    private void Start()
    {
        StartCoroutine(GetReviewAvailabilityEndOfFrame());
    }

    public void YandexSetReviewAvailability(int isReviewAvailable)
    {
        _isReviewAvailable = Convert.ToBoolean(isReviewAvailable);

        if (_isReviewAvailable)
        {
            _session.SessionActivityChanged += OnSessionActivityChanged;
            _reviewSessionNumber = _session.Number + _sessionCountToReview;
        }
    }

    private void OnWindowConfirmClick()
    {
        OpenReviewWindow();
    }

    private void OnSessionActivityChanged(bool isActive)
    {
        if (_session.Number > _currentSessionNumber)
        {
            _currentSessionNumber = _session.Number;

            if (_currentSessionNumber == _reviewSessionNumber)
            {
                _window.gameObject.SetActive(true);
                _session.SessionActivityChanged -= OnSessionActivityChanged;
            }
        }
    }

    private IEnumerator GetReviewAvailabilityEndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        GetReviewAvailability();
    }
}
