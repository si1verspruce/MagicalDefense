using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstancePool : MonoBehaviour, IResetOnRestart
{
    [SerializeField] private Transform _container;

    protected Dictionary<Instance, List<Instance>> Pool = new Dictionary<Instance, List<Instance>>();

    public Instance[] Expand(Instance pooledInstance, int copyCount)   
    {
        Instance[] instances = new Instance[copyCount];

        if (Pool.ContainsKey(pooledInstance) == false)
            Pool.Add(pooledInstance, new List<Instance>());

        for (int i = 0; i < copyCount; i++)
        {
            var instance = Instantiate(pooledInstance, _container);
            instance.gameObject.SetActive(false);
            Pool[pooledInstance].Add(instance);
            instances[i] = instance;
        }

        return instances;
    }

    public Instance GetInstance(Instance requestedInstance)
    {
        var instances = Pool[requestedInstance];
        var instance = instances.FirstOrDefault(instance => instance.gameObject.activeSelf == false);

        if (instance == null)
        {
            var newInstance = Instantiate(requestedInstance, _container);
            Pool[requestedInstance].Add(newInstance);

            return newInstance;
        }
        else
        {
            return instance;
        }
    }

    public Instance[] GetInstances(Instance requestedInstance)
    {
        return Pool[requestedInstance].ToArray();
    }

    public void Reset()
    {
        foreach (KeyValuePair<Instance, List<Instance>> instancesByInstanceType in Pool)
            foreach (Instance instance in instancesByInstanceType.Value)
                instance.gameObject.SetActive(false);
    }
}
