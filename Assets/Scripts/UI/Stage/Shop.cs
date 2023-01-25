using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Shop : MonoBehaviour, ISaveable
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Spell[] _spells;
    [SerializeField] private SpellFullView _spellView;
    [SerializeField] private Transform _buyContainer;
    [SerializeField] private Transform _upgradeContainer;
    [SerializeField] private Button _back;
    [SerializeField] private Player _player;
    [SerializeField] private Button _switch;
    [SerializeField] private Transform _spellContainer;
    [SerializeField] private AudioSource _moneySound;

    private List<Spell> _spellInstances = new List<Spell>();

    public void LoadState(string saveData)
    {
        var data = JsonUtility.FromJson<SaveData>(saveData);

        foreach (var spell in _spells)
        {
            var dataIndex = data.spellDataList.FindIndex(spellData => spellData.typeName == spell.GetType().ToString());

            if (dataIndex != -1)
                InitSpell(spell, data.spellDataList[dataIndex].isBought, data.spellDataList[dataIndex].level);
        }
    }

    public void LoadByDefault()
    {
        foreach (var spell in _spells)
        {
            InitSpell(spell, spell.IsBought, spell.Level);
        }
    }

    private void OnEnable()
    {
        _back.onClick.AddListener(DeactivateScreen);
        _switch.onClick.AddListener(SwitchScreen);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(DeactivateScreen);
        _switch.onClick.RemoveListener(SwitchScreen);
    }

    private void InitSpell(Spell spell, bool isBought, int level)
    {
        var spellInstance = Instantiate(spell, _spellContainer);
        spellInstance.Init(isBought, level);
        _spellInstances.Add(spellInstance);
        SpellFullView view;

        if (spellInstance.IsBought)
        {
            view = Instantiate(_spellView, _upgradeContainer);
            _player.AddSpell(spellInstance);
            view.UpgradeButtonClicked += OnUpgradeButton;
        }
        else
        {
            view = Instantiate(_spellView, _buyContainer);
            view.BuyButtonClicked += OnBuyButton;
        }

        view.Init(spellInstance);
    }

    public object SaveState()
    {
        SaveData saveData = new SaveData() { spellDataList = new List<SpellData>() };

        foreach (var spell in _spellInstances)
        {
            var spellData = new SpellData()
            {
                typeName = spell.GetType().ToString(),
                isBought = spell.IsBought,
                level = spell.Level
            };

            saveData.spellDataList.Add(spellData);
        }

        return saveData;
    }

    public void ActivateScreen()
    {
        _screen.SetActive(true);
    }

    private void DeactivateScreen()
    {
        _screen.SetActive(false);
    }

    private void OnBuyButton(Spell spell, SpellFullView view)
    {
        if (_player.Gems >= spell.BuyPrice)
        {
            _player.BuySpell(spell);
            _moneySound.Play();
            view.ActivateUpgradeGroup();
            view.BuyButtonClicked -= OnBuyButton;
            view.UpgradeButtonClicked += OnUpgradeButton;
            view.transform.SetParent(_upgradeContainer);

            _saveLoadSystem.Save(this);
        }
    }

    private void OnUpgradeButton(Spell spell, SpellFullView view)
    {
        if (_player.Money >= spell.UpgradePrice)
        {
            _player.UpgradeSpell(spell);
            _moneySound.Play();
            view.UpdateUpgradeGroup();

            _saveLoadSystem.Save(this);

            if (spell.Level == int.MaxValue)
                view.UpgradeButtonClicked -= OnUpgradeButton;
        }
    }

    private void SwitchScreen()
    {
        if (_buyContainer.gameObject.activeSelf)
        {
            _buyContainer.gameObject.SetActive(false);
            _upgradeContainer.gameObject.SetActive(true);
        }
        else
        {
            _buyContainer.gameObject.SetActive(true);
            _upgradeContainer.gameObject.SetActive(false);
        }
    }

    [Serializable]
    private struct SaveData
    {
        public List<SpellData> spellDataList;
    }

    [Serializable]
    private struct SpellData
    {
        public string typeName;
        public bool isBought;
        public int level;
    }
}
