using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    private const int UpgradePricePerLevel = 50;

    [SerializeField] private string _label;
    [SerializeField] private string _upgradeDescription;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _buyPrice;
    [SerializeField] private bool _isBought = false;
    [SerializeField] private MagicElement[] _combination;
    [SerializeField] private float _scalePerLevel;
    [SerializeField] protected float SpeedToTarget;
    [SerializeField] protected Instance CreatedInstance;

    private int _upgradePrice;
    private int _level = 1;
    private float _scaleModifier;

    public string Label => _label;
    public string UpgradeDescription => _upgradeDescription;
    public Sprite Icon => _icon;
    public int BuyPrice => _buyPrice;
    public int UpgradePrice => _upgradePrice;
    public bool IsBought => _isBought;
    public int Level => _level;
    public Instance InstanceToCreate => CreatedInstance;
    public float ScaleModifier => _scaleModifier;

    public void Init(bool isBought, int level)
    {
        _isBought = isBought;
        _level = level;
        _scaleModifier = 1 + (_level - 1) * _scalePerLevel;

        SetUpgradePrice(_level);
    }

    public bool CompareCombinations(List<ElementType> combination)
    {
        if (combination == null)
            return false;

        if (_combination.Count() != combination.Count())
            return false;

        var spellCombination = _combination.OrderBy(element => element.Type).ToArray();
        var receivedCombination = combination.OrderBy(element => element).ToArray();

        for (int i = 0; i < spellCombination.Count(); i++)
            if (spellCombination[i].Type != receivedCombination[i])
                return false;

        return true;
    }

    public abstract void Cast(Instance createdInstance, Vector3 targetPosition);

    public List<MagicElement> GetCombination()
    {
        List<MagicElement> elements = new List<MagicElement>();

        foreach (var element in _combination)
            elements.Add(element);

        return elements;
    }

    public void Buy()
    {
        _isBought = true;
    }

    public void OnLevelIncrease()
    {
        _level++;
        _scaleModifier += _scalePerLevel;
        SetUpgradePrice(_level);
    }

    protected void ResetInstance(Instance instance, Vector3 startPosition, Quaternion startRotation)
    {
        instance.transform.position = startPosition;
        instance.transform.rotation = startRotation;
        instance.gameObject.SetActive(true);
    }

    private void SetUpgradePrice(int level)
    {
        _upgradePrice = level * UpgradePricePerLevel;
    }
}
