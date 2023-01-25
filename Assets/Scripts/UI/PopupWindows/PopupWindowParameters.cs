using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PopupWindowParameters 
{
    public const string ValueTag = "#value#";

    [Header("To insert values in the text use tag: #value#")]
    public string message;
    public bool isShowWindow;

    [HideInInspector] public string[] values;
}
