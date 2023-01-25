using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellShortView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Transform _combinationContainer;
    [SerializeField] private Image _magicElementView;

    protected Spell Spell;

    public int Level => Spell.Level;
    public event UnityAction<int> LevelChanged;

    public virtual void Init(Spell spell)
    {
        Spell = spell;

        _label.text = Spell.Label;
        InvokeLevelChanged(Spell.Level);
        var combination = Spell.GetCombination();

        foreach (var element in combination)
        {
            var view = Instantiate(_magicElementView, _combinationContainer);
            view.sprite = element.Sprite;
        }
    }

    protected void InvokeLevelChanged(int level)
    {
        LevelChanged?.Invoke(level);
    }
}
