using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public enum RewardedAdState
{
    FailedToLoad,
    ShowFailed,
    Finished
}

public class AdSettings : MonoBehaviour
{
    [SerializeField] private float _delayBetweenBannerLoads;

    private string _bannerId = "ca-app-pub-3940256099942544/6300978111";
    private string _interstitialId = "ca-app-pub-3940256099942544/1033173712";
    private string _rewardedId = "ca-app-pub-3940256099942544/5224354917";
    private bool _rewardedIsLoaded;

    public event UnityAction<RewardedAdState> RewardedAdStateChanged;
    public event UnityAction AdClosed;
    public event UnityAction RewardedAdLoaded;

    public bool RewardedIsLoaded => _rewardedIsLoaded;

    private void Start()
    {
        LoadBanners();
        LoadInterstitial();
        LoadRewarded();
    }

    public void ShowBanners()
    {
        /*ShowBanner(_bottomBanner, AdPosition.Bottom);
        ShowBanner(_topBanner, AdPosition.Top);*/
    }

    public void DestroyBanners()
    {
        /*_bottomBanner.Destroy();
        _topBanner.Destroy();*/

        LoadBanners();
    }

    public void TryToShowInterstitial()
    {
        /*if (_interstitial.IsLoaded())
        {
            _interstitial.Show();
            _interstitial.OnAdClosed += OnInterstitialClosed;
        }*/
    }

    public bool GetRewardedLoadState()
    {
        return false;
        /*return _rewarded.IsLoaded();*/
    }

    public void ShowRewarded()
    {
        /*if (_rewarded.IsLoaded())
        {
            _rewardedIsLoaded = false;
            _rewarded.Show();
            _rewarded.OnAdFailedToLoad += OnFailedToLoad;
            _rewarded.OnAdFailedToShow += OnFailedToShow;
            _rewarded.OnUserEarnedReward += OnEarnedReward;
        }*/
    }

    /*private void ShowBanner(BannerView view, AdPosition position)
    {
        view.Show();
        view.SetPosition(position);
    }*/

    private void LoadBanners()
    {
        /*LoadBanner(ref _bottomBanner);
        LoadBanner(ref _topBanner);*/
    }

    /*private void LoadBanner(ref BannerView view)
    {
        view = new BannerView(_bannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        view.LoadAd(request);
        view.Hide();
    }*/

    private void LoadInterstitial()
    {
        /*_interstitial = new InterstitialAd(_interstitialId);
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);*/
    }

    private void LoadRewarded()
    {
        /*_rewarded = new RewardedAd(_rewardedId);
        AdRequest request = new AdRequest.Builder().Build();
        _rewarded.LoadAd(request);
        _rewarded.OnAdLoaded += OnLoaded;*/
    }

    private void OnInterstitialClosed(object sender, EventArgs args)
    {
        AdClosed?.Invoke();
        //_interstitial.OnAdClosed -= OnInterstitialClosed;

        LoadInterstitial();
    }

    public void OnLoaded(object sender, EventArgs args)
    {
        RewardedAdLoaded?.Invoke();
        _rewardedIsLoaded = true;
    }

    /*private void OnFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RewardedAdStateChanged?.Invoke(RewardedAdState.FailedToLoad);
        OnRewardedCompletedEndFrame();
    }

    private void OnFailedToShow(object sender, AdErrorEventArgs args)
    {
        RewardedAdStateChanged?.Invoke(RewardedAdState.ShowFailed);
        OnRewardedCompletedEndFrame();
    }

    private void OnEarnedReward(object sender, Reward args)
    {
        RewardedAdStateChanged?.Invoke(RewardedAdState.Finished);
        OnRewardedCompletedEndFrame();
    }*/

    private void OnRewardedCompletedEndFrame()
    {
        /*_rewarded.OnAdFailedToLoad -= OnFailedToLoad;
        _rewarded.OnAdFailedToShow -= OnFailedToShow;
        _rewarded.OnUserEarnedReward -= OnEarnedReward;
        _rewarded.OnAdLoaded -= OnLoaded;*/

        LoadRewarded();
    }
}
