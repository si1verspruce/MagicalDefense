using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationSetter : MonoBehaviour
{
    [SerializeField] private Locale _defaultLocale;

    private void Awake()
    {
        LocalizationSettings.SelectedLocale = _defaultLocale;
    }
}
