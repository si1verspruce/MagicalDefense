using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Session : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private float _time;
    [SerializeField] private Player _player;
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private Ressurection _ressurection;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private int _stageCountBeforeBoss;
    [SerializeField] private GamePause _gamePauseToggle;
    [SerializeField] private SessionRestarter _restarter;
    [SerializeField] private AdSettings _ad;

    private int _number;
    private int _bossNumber = 4;
    private bool _isSessionActive = true;
    private bool _isRessurectionAvailable = true;
    private float _currentTime;

    public event UnityAction<int> TimeChanged;
    public event UnityAction<bool> SessionActivityChanged;

    public int Number => _number;
    public int BossNumber => _bossNumber;

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _saveLoadSystem.Load();
        _currentTime = _time;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime * Convert.ToInt32(EnemyTime.IsActive);
            TimeChanged?.Invoke((int)Mathf.Round(_currentTime));
        }
        else if (_isSessionActive == true)
        {
            _number++;
            _player.AddGem();
            OnSessionOver();
            _victoryScreen.gameObject.SetActive(true);
        }
    }

    public object SaveState()
    {
        SaveData data = new SaveData() { number = _number };

        return data;
    }

    public void LoadState(string saveData)
    {
        var savedData = JsonUtility.FromJson<SaveData>(saveData);
        _number = savedData.number;
        _bossNumber = (_number / _stageCountBeforeBoss + 1) * _stageCountBeforeBoss - 1;
        _menuScreen.OpenScreen();
        _tutorial.DestroyScreen();
    }

    public void LoadByDefault()
    {
        _tutorial.gameObject.SetActive(true);
    }

    public void Reset()
    {
        _saveLoadSystem.SaveAll();

        _currentTime = _time;
        _isSessionActive = true;
        SessionActivityChanged?.Invoke(_isSessionActive);
        _isRessurectionAvailable = true;
    }

    private void OnHealthChanged(int _health)
    {
        if (_health <= 0)
        {
            if (_isRessurectionAvailable)
            {
                _ressurection.RequestAd();
                _isRessurectionAvailable = false;
            }
            else
            {
                OnSessionOver();
                _defeatScreen.gameObject.SetActive(true);
            }
        }
    }

    private void OnSessionOver()
    {
        _isSessionActive = false;
        SessionActivityChanged?.Invoke(_isSessionActive);
        _gamePauseToggle.RequestPause(gameObject);
        _saveLoadSystem.SaveAll();

#if UNITY_WEBGL
        SetToLeaderboard(_number);
#endif
    }

    [Serializable]
    private struct SaveData
    {
        public int number;
    }
}