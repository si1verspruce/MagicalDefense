using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        OnGemsChanged(_player.Gems);
        _player.GemsChanged += OnGemsChanged;
    }

    private void OnDisable()
    {
        _player.GemsChanged -= OnGemsChanged;
    }

    private void OnGemsChanged(int gems)
    {
        _value.text = gems.ToString();
    }
}
