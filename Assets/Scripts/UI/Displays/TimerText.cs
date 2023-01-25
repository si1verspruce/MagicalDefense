using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerText : MonoBehaviour
{
    [SerializeField] private Session _stage;

    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _stage.TimeChanged += OnTimeChanged;
    }

    private void OnDisable()
    {
        _stage.TimeChanged -= OnTimeChanged;
    }

    private void OnTimeChanged(int time)
    {
        _textMeshPro.text = time.ToString();
    }
}
