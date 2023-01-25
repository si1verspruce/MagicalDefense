using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPool : InstancePool
{
    [SerializeField] private int _copyCount;

    public Instance[] Expand(Instance pooledInstance)
    {
        return Expand(pooledInstance, _copyCount);
    }
}
