using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : PauseScreen
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private AdSettings _ads;

    public override void OpenScreen()
    {
        base.OpenScreen();
        _screen.SetActive(true);
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
        _screen.SetActive(false);
    }
}
