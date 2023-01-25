using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSpellMaker))]
[RequireComponent(typeof(SpellPool))]
public class PlayerCaster : MonoBehaviour
{
    private Player _player;
    private PlayerSpellMaker _spellMaker;
    private Spell _currentSpell;
    private SpellPool _pool;

    public event UnityAction<bool> SpellDone;

    public Spell CurrentSpell => _currentSpell;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _spellMaker = GetComponent<PlayerSpellMaker>();
        _pool = GetComponent<SpellPool>();
    }

    private void OnEnable()
    {
        _spellMaker.CombinationUpdated += OnCombinationUpdated;
    }

    private void OnDisable()
    {
        _spellMaker.CombinationUpdated -= OnCombinationUpdated;
    }

    public void OnCastInput(Vector3 targetPosition)
    {
        if (_currentSpell == null)
        {
            _spellMaker.UnselectCurrentCombination();
        }
        else
        {
            _currentSpell.Cast(_pool.GetInstance(_currentSpell.InstanceToCreate), targetPosition);
            _spellMaker.DeactivateCurrentCombination();
        }
    }

    private void OnCombinationUpdated(List<ElementType> elements)
    {
        _currentSpell = _player.GetSpell(elements);
        bool isSpellDone = _currentSpell != null;
        SpellDone?.Invoke(isSpellDone);
    }
}
