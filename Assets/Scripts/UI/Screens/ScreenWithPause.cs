using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWithPause : MonoBehaviour
{
    [SerializeField] private GamePause _pause;

    private void OnEnable()
    {
        _pause.RequestPause(gameObject);
    }

    private void OnDisable()
    {
        _pause.RequestPlay(gameObject);
    }
}
