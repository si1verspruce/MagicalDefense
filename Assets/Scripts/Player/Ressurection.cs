using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressurection : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RewardedPopupAd _ad;
    [SerializeField] private PauseForEnemies _pause;
    [SerializeField] private GamePause _gamePause;
    [SerializeField] private YesNoPopupWindow _confirmationWindow;
    [SerializeField] private string _confirmationWindowMessage;
    [SerializeField] private DefeatScreen _defeatScreen;

    private void OnEnable()
    {
        _confirmationWindow.ConfirmClick += OnConfirmClick;
        _confirmationWindow.RejectClick += OnRejectClick;
        _ad.Rewarded += TryGetReward;
    }

    private void OnDisable()
    {
        _confirmationWindow.ConfirmClick -= OnConfirmClick;
        _confirmationWindow.RejectClick -= OnRejectClick;
        _ad.Rewarded -= TryGetReward;
    }

    public void RequestAd()
    {
        _confirmationWindow.gameObject.SetActive(true);
        _confirmationWindow.SetMessage(_confirmationWindowMessage);
    }

    private void OnConfirmClick()
    {
        _ad.Show();
    }

    private void OnRejectClick()
    {
        _gamePause.RequestPause(gameObject);
        _defeatScreen.gameObject.SetActive(true);
    }

    private void TryGetReward(bool isRewarded)
    {
        if (isRewarded)
        {
            StartCoroutine(GetRewardEndFrame());
        }
    }

    private IEnumerator GetRewardEndFrame()
    {
        yield return new WaitForEndOfFrame();

        _pause.Pause();
        _player.Ressurect();
    }
}
