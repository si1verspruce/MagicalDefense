using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StageNumberText : MonoBehaviour, ILocalizable
{
    [SerializeField] protected Session Stage;
    [SerializeField] protected string PhraseBeforeNumber;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Localize();
    }

    public virtual void Localize()
    {
        string phrase = $"{PhraseBeforeNumber} {Stage.Number + 1}";
        SetText(phrase);
    }

    protected void SetText(string text)
    {
        _text.text = text;
    }
}
