using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] GameObject[] _firstGroup;
    [SerializeField] GameObject[] _secondGroup;

    public void SwitchObjects()
    {
        if (_firstGroup[0].activeSelf)
        {
            SetActiveObjects(_firstGroup, false);
            SetActiveObjects(_secondGroup, true);
        }
        else
        {
            SetActiveObjects(_firstGroup, true);
            SetActiveObjects(_secondGroup, false);
        }
    }

    private void SetActiveObjects(GameObject[] items, bool isActive)
    {
        foreach (var item in items)
            item.SetActive(isActive);
    }
}
