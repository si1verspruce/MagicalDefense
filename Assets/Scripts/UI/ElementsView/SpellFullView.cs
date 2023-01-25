using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellFullView : SpellShortView
{
    [SerializeField] private Image _spellIcon;
    [SerializeField] private TextMeshProUGUI _upgradeDescription;
    [SerializeField] private GameObject _buyGroup;
    [SerializeField] private Button _buy;
    [SerializeField] private TextMeshProUGUI _buyPrice;
    [SerializeField] private GameObject _upgradeGroup;
    [SerializeField] private Button _upgrade;

    public int UpgradePrice => Spell.UpgradePrice;

    public event UnityAction<Spell, SpellFullView> BuyButtonClicked;
    public event UnityAction<Spell, SpellFullView> UpgradeButtonClicked;
    public event UnityAction<int> UpgradePriceChanged;

    public override void Init(Spell spell)
    {
        base.Init(spell);
        _upgradeDescription.text = Spell.UpgradeDescription;
        _spellIcon.sprite = Spell.Icon;
        _buyPrice.text = Spell.BuyPrice.ToString();
        UpgradePriceChanged?.Invoke(Spell.UpgradePrice);

        if (spell.IsBought)
            ActivateUpgradeGroup();
    }

    private void OnEnable()
    {
        if (Spell.IsBought == false)
            _buy.onClick.AddListener(OnBuyButtonClick);
        else
            _upgrade.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnDisable()
    {
        if (Spell.IsBought == false)
            _buy.onClick.RemoveListener(OnBuyButtonClick);
        else
            _upgrade.onClick.RemoveListener(OnUpgradeButtonClick);
    }

    public void ActivateUpgradeGroup()
    {
        _buyGroup.SetActive(false);
        _upgradeGroup.SetActive(true);
    }

    public void UpdateUpgradeGroup()
    {
        InvokeLevelChanged(Spell.Level);
        UpgradePriceChanged?.Invoke(Spell.UpgradePrice);
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke(Spell, this);
    }

    private void OnUpgradeButtonClick()
    {
        UpgradeButtonClicked?.Invoke(Spell, this);
    }
}
