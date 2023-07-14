using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;

public enum RewardedAdState
{
    FailedToLoad,
    ShowFailed,
    Finished
}

public class AdSettings : MonoBehaviour
{
    [SerializeField] private GamePause _pause;
    [SerializeField] private Sound _sound;

    public event UnityAction<RewardedAdState> RewardedAdStateChanged;
    public event UnityAction AdClosed;
    public event UnityAction RewardedAdLoaded;

    [DllImport("__Internal")]
    private static extern void ShowInterstitial();

    [DllImport("__Internal")]
    private static extern void ShowRewarded();

    private void Awake()
    {
        transform.parent = null;
    }

    public void RequestShowInterstitial()
    {
        _pause.RequestPause(gameObject);
        _sound.Pause();
        ShowInterstitial();
    }
    
    public void RequestShowRewarded()
    {
        _pause.RequestPause(gameObject);
        _sound.Pause();
        ShowRewarded();
    }
    
    private void OnRewardedFailedToShow()
    {
        _pause.RequestPlay(gameObject);
        _sound.UnPause();
        RewardedAdStateChanged?.Invoke(RewardedAdState.ShowFailed);
    }

    private void OnRewardedEarnedReward()
    {
        _pause.RequestPlay(gameObject);
        _sound.UnPause();
        RewardedAdStateChanged?.Invoke(RewardedAdState.Finished);
    }

    private void OnInterstitialClosed()
    {
        _pause.RequestPlay(gameObject);
        _sound.UnPause();
    }
}
