using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastElectricTrap : Spell
{
    [SerializeField] private float _positionY;

    public override void Cast(Instance createdInstance, Vector3 targetPosition)
    {
        var position = new Vector3(targetPosition.x, _positionY, targetPosition.z);
        ResetInstance(createdInstance, position, Quaternion.identity);
    }
}
