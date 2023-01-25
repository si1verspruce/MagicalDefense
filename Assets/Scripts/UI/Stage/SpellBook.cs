using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    [SerializeField] private SpellShortView _view;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _container;
    [SerializeField] private Button _back;
    [SerializeField] private GamePause _gamePauseToggle;

    private List<SpellShortView> _views = new List<SpellShortView>();

    private void OnEnable()
    {
        _player.SpellAdded += InstantiateSpellView;
        _back.onClick.AddListener(DeactivateScreen);
    }

    private void OnDisable()
    {
        _player.SpellAdded -= InstantiateSpellView;
        _back.onClick.RemoveListener(DeactivateScreen);
    }

    public void ActivateScreen()
    {
        _gamePauseToggle.RequestPause(gameObject);
        _screen.SetActive(true);
    }

    public void DeactivateScreen()
    {
        _gamePauseToggle.RequestPlay(gameObject);
        _screen.SetActive(false);
    }

    private void InstantiateSpellView(Spell spell)
    {
        var newView = Instantiate(_view, _container);

        _views.Add(newView);
        newView.Init(spell);
    }
}
