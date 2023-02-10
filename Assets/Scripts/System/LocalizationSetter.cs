using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSetter : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLanguageCode();

    private void Awake()
    {
        StartCoroutine(SetLocale());
    }

    private IEnumerator SetLocale()
    {
        yield return LocalizationSettings.InitializationOperation;

        string localeCode = GetLanguageCode();

        if (LocalizationSettings.SelectedLocale.Identifier.Code != localeCode)
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.First(locale => locale.Identifier.Code == localeCode);
    }
}
