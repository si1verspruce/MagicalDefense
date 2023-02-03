using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizableEntity : MonoBehaviour
{
    private void Awake()
    {
        var localizables = GetComponents<ILocalizable>();

        foreach (var localizable in localizables)
            localizable.Localize();
    }
}
