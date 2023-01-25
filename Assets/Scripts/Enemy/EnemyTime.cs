using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EnemyTime
{
    private static bool _isTimeActive = true;

    public static bool IsActive => _isTimeActive;

    public static event UnityAction<bool> TimeActivityChanged;

    public static void SetTimeActivity(bool isActive)
    {
        _isTimeActive = isActive;
        TimeActivityChanged?.Invoke(isActive);
    }
}
