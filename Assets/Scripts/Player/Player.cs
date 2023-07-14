using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(SpellPool))]
public class Player : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private int _health;
    [SerializeField, Range(0, 1)] private float _healthFractionAfterRessurection;
    [SerializeField] private SaveLoadSystem _saveLoadSystem;

    private int _currentHealth;
    private List<Spell> _spells = new List<Spell>();
    private int _money;
    private int _gems;
    private SpellPool _pool;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int> GemsChanged;
    public event UnityAction<Spell> SpellAdded;

    public int Health => _currentHealth;
    public int Money => _money;
    public int Gems => _gems;

    private void Awake()
    {
        SetDefaultHealth();
    }

    public void LoadState(string saveData)
    {
        var data = JsonUtility.FromJson<SaveData>(saveData);

        _money = data.money;
        _gems = data.gems;

        AddMoney(0);
    }

    public void LoadByDefault()
    {
        AddMoney(0);
    }

    public Spell GetSpell(List<ElementType> combination)
    {
        return _spells.FirstOrDefault(spell => spell.CompareCombinations(combination));
    }

    public void Ressurect()
    {
        _currentHealth = (int)(_health * _healthFractionAfterRessurection);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void AddMoney(int money)
    {
        if (money >= 0)
        {
            _money += money;

            MoneyChanged?.Invoke(_money);
        }
    }

    public void AddGem()
    {
        _gems++;

        GemsChanged?.Invoke(_gems);
    }

    public void ApplyDamage(int damage)
    {
        if (damage >= 0)
        {
            _currentHealth -= damage;

            HealthChanged?.Invoke(_currentHealth);
        }
    }

    public void UpgradeSpell(Spell spell)
    {
        _money -= spell.UpgradePrice;
        spell.OnLevelIncrease();
        MoneyChanged?.Invoke(_money);
        var instances = _pool.GetInstances(spell.InstanceToCreate);
        ScalePoolInstances(instances, spell.ScaleModifier);

        _saveLoadSystem.SaveAll();
    }

    public void BuySpell(Spell spell)
    {
        _gems -= spell.BuyPrice;
        GemsChanged?.Invoke(_gems);
        spell.Buy();
        AddSpell(spell);

        _saveLoadSystem.SaveAll();
    }

    public void AddSpell(Spell spell)
    {
        _pool = GetComponent<SpellPool>();

        _spells.Add(spell);
        var instances = _pool.Expand(spell.InstanceToCreate);
        ScalePoolInstances(instances, spell.ScaleModifier);

        SpellAdded?.Invoke(spell);
    }

    public object SaveState()
    {
        SaveData data = new SaveData()
        {
            money = _money,
            gems = _gems
        };

        return data;
    }

    public void Reset()
    {
        SetDefaultHealth();
    }

    private void SetDefaultHealth()
    {
        _currentHealth = _health;
        HealthChanged?.Invoke(_currentHealth);
    }

    private void ScalePoolInstances(Instance[] instances, float modifier)
    {
        foreach (var instance in instances)
            if (instance.TryGetComponent(out IScaleable scaleble))
                scaleble.Scale(modifier);
    }

    [Serializable]
    private struct SaveData
    {
        public int money;
        public int gems;
    }
}
