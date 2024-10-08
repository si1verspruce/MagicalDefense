using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RewardedPopupAd : MonoBehaviour
{
    [SerializeField] private PopupWindowParameters _adNotLoaded;
    [SerializeField] private PopupWindowParameters _adFailed;
    [SerializeField] private PopupWindowParameters _adCompleted;
    [SerializeField] private PopupWindow _window;
    [SerializeField] private AdSettings _ads;

    private string[] _rewardValues;

    public event UnityAction<bool> Rewarded;

    public void SetRewardValues(string[] values)
    {
        _rewardValues = values;
    }

    public void Show()
    {
        _ads.RewardedAdStateChanged += OnRewardedAdCompleted;
        _ads.RequestShowRewarded();
    }

    private void OnRewardedAdCompleted(RewardedAdState result)
    {
        switch (result)
        {
            case RewardedAdState.FailedToLoad:
                Rewarded?.Invoke(false);
                StartCoroutine(ActivatePopupWindowEndFrame(_adNotLoaded));
                break;
            case RewardedAdState.ShowFailed:
                Rewarded?.Invoke(false);
                StartCoroutine(ActivatePopupWindowEndFrame(_adFailed));
                break;
            case RewardedAdState.Finished:
                Rewarded?.Invoke(true);
                StartCoroutine(ActivatePopupWindowEndFrame(_adCompleted));
                break;
        }

        _ads.RewardedAdStateChanged -= OnRewardedAdCompleted;
    }

    private IEnumerator ActivatePopupWindowEndFrame(PopupWindowParameters popup)
    {
        yield return new WaitForEndOfFrame();

        if (popup.isShowWindow)
        {
            _window.SetMessage(popup.message, _rewardValues, PopupWindowParameters.ValueTag);
            _window.gameObject.SetActive(true);
            _window.ConfirmClick += PopupWindowClosed;
        }
    }

    private void PopupWindowClosed()
    {
        _window.ConfirmClick -= PopupWindowClosed;
        _window.gameObject.SetActive(false);
    }
}
