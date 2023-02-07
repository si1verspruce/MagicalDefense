using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAfterSessionShower : MonoBehaviour
{
    [SerializeField] private float _sessionsTimeToShow;
    [SerializeField] private float _gameStartedTime;
    [SerializeField] private Session _stage;
    [SerializeField] private AdSettings _ads;
    [SerializeField] private float _delay;

    private float _time;
    private bool _isSessionActive;

    private void Awake()
    {
        _time = _gameStartedTime;
    }

    private void OnEnable()
    {
        _stage.SessionActivityChanged += OnSessionActivityChanged;
        _ads.AdClosed += PauseGame;
    }

    private void OnDisable()
    {
        _stage.SessionActivityChanged -= OnSessionActivityChanged;
        _ads.AdClosed -= PauseGame;
    }

    private void Update()
    {
        if (_isSessionActive)
        {
            _time += Time.deltaTime;
        }
    }

    private void OnSessionActivityChanged(bool isSessionActive)
    {
        _isSessionActive = isSessionActive;

        if (isSessionActive == false && _time >= _sessionsTimeToShow)
        {
            _time = 0;
            StartCoroutine(ShowAfterDelay());
        }
    }

    private IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSecondsRealtime(_delay);

        _ads.TryToShowInterstitial();
        Time.timeScale = 0;
    }

    private void PauseGame()
    {
        StartCoroutine(PauseGameEndFrame());
    }

    private IEnumerator PauseGameEndFrame()
    {
        yield return new WaitForEndOfFrame();

        Time.timeScale = 0;
    }
}
