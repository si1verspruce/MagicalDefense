using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSetter : MonoBehaviour
{
    [SerializeField] private string _localeChecker;
    [SerializeField] private GameObject _loadingScreen;

    private bool _isLocalized = false;

    [DllImport("__Internal")]
    private static extern string GetLanguageCode();

    private void Awake()
    {
        StartCoroutine(SetLocale());
    }

    public void CloseLoadingScreen()
    {
        if (_isLocalized)
            _loadingScreen.SetActive(false);
    }

    private IEnumerator SetLocale()
    {
        yield return LocalizationSettings.InitializationOperation;

        _isLocalized = true;
        string localeCode = GetLanguageCode();

        if (LocalizationSettings.SelectedLocale.Identifier.Code != localeCode)
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales.First(locale => locale.Identifier.Code == localeCode);
        else
            CloseLoadingScreen();
    }
}
