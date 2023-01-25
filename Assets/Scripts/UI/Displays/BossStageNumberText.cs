using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageNumberText : StageNumberText
{
    private string AlternativePhrase;

    protected override void Init()
    {
        AlternativePhrase = "Boss Stage!";
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        Phrase = $"Boss on stage {Stage.BossNumber + 1}";

        if (Stage.Number == Stage.BossNumber)
            SetText(AlternativePhrase);
        else
            SetText(Phrase);
    }
}
