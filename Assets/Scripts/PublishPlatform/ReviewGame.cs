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

    private void Awake()
    {
        GetReviewAvailability();
        Debug.Log(_isReviewAvailable);

        if (_isReviewAvailable)
            _reviewSessionNumber = _session.Number + _sessionCountToReview;
    }

    private void OnEnable()
    {
        if (_isReviewAvailable)
            _session.SessionActivityChanged += OnSessionActivityChanged;
    }

    private void OnDisable()
    {
        if (_isReviewAvailable)
            _session.SessionActivityChanged -= OnSessionActivityChanged;
    }

    public void YandexSetReviewAvailability(bool isReviewAvailable)
    {
        _isReviewAvailable = isReviewAvailable;
    }

    private void OnSessionActivityChanged(bool isActive)
    {
        if (_session.Number > _currentSessionNumber)
        {
            _currentSessionNumber = _session.Number;

            if (_currentSessionNumber == _reviewSessionNumber)
            {
                GetReviewAvailability();

                if (_isReviewAvailable)
                    _window.gameObject.SetActive(true);
            }
        }
    }
}
