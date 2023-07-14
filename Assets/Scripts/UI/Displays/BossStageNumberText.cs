using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageNumberText : StageNumberText
{
    [SerializeField] private string _alternativePhrase;

    public override void Localize()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        string phrase = $"{PhraseBeforeNumber} {Stage.BossNumber + 1}";

        if (Stage.Number == Stage.BossNumber)
            SetText(_alternativePhrase);
        else
            SetText(phrase);
    }
}
