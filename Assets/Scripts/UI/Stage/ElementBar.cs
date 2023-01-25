using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementBar : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private MagicElementCell[] _cells;
    [SerializeField] private Session _session;
    [SerializeField] private int _rewardSessionCountWithFirstCellUnlocked;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] private AdSettings _ad;
    [SerializeField] private RewardedPopupAd _popupAd;
    [SerializeField] private YesNoPopupWindow _confirmationWindow;
    [SerializeField] private string _confirmationWindowMessage;

    private int _sessionUntilFirstCellUnlocked;
    private MagicElementCell _firstCell;

    public event UnityAction<MagicElementCell> CellAdded;

    public object SaveState()
    {
        return new SaveData { sessionUntilFirstCellUnlocked = _sessionUntilFirstCellUnlocked };
    }

    public void LoadState(string jsonData)
    {
        Init();
        _sessionUntilFirstCellUnlocked = JsonUtility.FromJson<SaveData>(jsonData).sessionUntilFirstCellUnlocked;
    }

    public void LoadByDefault()
    {
        Init();
    }

    private void OnEnable()
    {
        _confirmationWindow.ConfirmClick += ShowAd;
        _confirmationWindow.RejectClick += OnRejectClick;
        _ad.RewardedAdLoaded += SetActiveFirstCellButton;
    }

    private void OnDisable()
    {
        _confirmationWindow.ConfirmClick -= ShowAd;
        _confirmationWindow.RejectClick -= OnRejectClick;
        _firstCell.Clicked -= RequestAd;
        _ad.RewardedAdLoaded -= SetActiveFirstCellButton;
    }

    public void Reset()
    {
        if (_session.Number >= _sessionUntilFirstCellUnlocked)
        {
            _firstCell.SetLock(true);
            _firstCell.SetButtonActive(_ad.RewardedIsLoaded);
        }
        else
        {
            _firstCell.SetLock(false);
        }
    }

    private void Init()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            CellAdded?.Invoke(_cells[i]);

            if (i == 0)
            {
                _cells[i].SetLock(true);
                _firstCell = _cells[i];
                _cells[i].Clicked += RequestAd;
            }
        }
    }

    private void SetActiveFirstCellButton()
    {
        _firstCell.SetButtonActive(true);
    }

    private void OnRejectClick()
    {
        Time.timeScale = 1;
    }

    private void RequestAd()
    {
        if (_ad.GetRewardedLoadState())
        {
            _confirmationWindow.gameObject.SetActive(true);
            _confirmationWindow.SetMessage(_confirmationWindowMessage,
                new string[] { _rewardSessionCountWithFirstCellUnlocked.ToString() }, PopupWindowParameters.ValueTag);
        }
    }

    private void ShowAd()
    {
        _popupAd.Rewarded += TryGetReward;
        _popupAd.Show();
        _popupAd.SetRewardValues(new string[] { _rewardSessionCountWithFirstCellUnlocked.ToString() });
    }

    private void TryGetReward(bool isRewarded)
    {
        if (isRewarded)
        {
            _sessionUntilFirstCellUnlocked = _session.Number + _rewardSessionCountWithFirstCellUnlocked;
            _firstCell.SetLock(false);
            _saveLoad.Save(this);
        }

        _popupAd.Rewarded -= TryGetReward;
    }

    [Serializable]
    private struct SaveData
    {
        public int sessionUntilFirstCellUnlocked;
    }
}