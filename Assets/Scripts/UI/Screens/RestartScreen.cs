using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RestartScreen : MonoBehaviour
{
    [SerializeField] protected SessionRestarter Restarter;

    public void RestartSession()
    {
        CloseScreen();
        Restarter.Restart();
    }

    public abstract void CloseScreen();
}
