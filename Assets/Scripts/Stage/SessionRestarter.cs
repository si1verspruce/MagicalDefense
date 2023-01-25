using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionRestarter : MonoBehaviour
{
    public void Restart()
    {
        foreach (var item in FindObjectsOfType<MonoBehaviour>().OfType<IResetOnRestart>())
            item.Reset();
    }
}
