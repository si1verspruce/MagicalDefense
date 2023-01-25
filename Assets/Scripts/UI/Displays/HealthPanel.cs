using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        OnHealthChanged(_player.Health);
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        _healthText.text = health.ToString();
    }
}
