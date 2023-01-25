using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpgradePriceText : MonoBehaviour
{
    [SerializeField] private SpellFullView _view;

    private TextMeshProUGUI _text;

    public void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _view.UpgradePriceChanged += OnPriceChange;
        OnPriceChange(_view.UpgradePrice);
    }

    private void OnDisable()
    {
        _view.UpgradePriceChanged -= OnPriceChange;
    }

    private void OnPriceChange(int price)
    {
        _text.text = price.ToString();
    }
}
