using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StageNumberText : MonoBehaviour
{
    [SerializeField] protected Session Stage;

    protected string Phrase;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Init();
    }

    protected virtual void Init() { }

    private void OnEnable()
    {
        Phrase = $"Stage {Stage.Number + 1}";
        SetText(Phrase);
    }

    protected void SetText(string text)
    {
        _text.text = text;
    }
}
