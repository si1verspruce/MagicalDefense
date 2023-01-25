using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SpellLevelText : MonoBehaviour
{
    [SerializeField] private SpellShortView _view;

    private TextMeshProUGUI _text;

    public void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _view.LevelChanged += OnLevelChanged;
        OnLevelChanged(_view.Level);
    }

    private void OnDisable()
    {
        _view.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        _text.text = level.ToString();
    }
}
