using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InstancePool))]
public abstract class Spawner : Instance
{
    [SerializeField] protected float Interval;

    private float _timeFromSpawn;
    private InstancePool _pool;

    protected void Init(SpawnedObject[] spawnedObjects)
    {
        _pool = GetComponent<InstancePool>();

        foreach (var spawnedObject in spawnedObjects)
            _pool.Expand(spawnedObject.Instance, spawnedObject.CopyCount);

        _timeFromSpawn = Interval;
    }

    private void Update()
    {
        SpawnPerInterval();
    }

    protected abstract Instance GetSpawnedInstance();

    protected abstract Vector3 GetSpawnPosition();

    protected abstract Quaternion GetSpawnedObjectRotation();

    protected virtual void SpawnPerInterval()
    {
        if (_timeFromSpawn >= Interval)
        {
            Spawn();

            _timeFromSpawn = 0;
        }
        else
        {
            _timeFromSpawn += Time.deltaTime;
        }
    }

    protected virtual void Spawn()
    {
        var instance = _pool.GetInstance(GetSpawnedInstance());
        var position = GetSpawnPosition();
        var rotation = GetSpawnedObjectRotation();
        InstantiateObject(instance, position, rotation);
    }

    protected void Spawn(Instance instance, Vector3 position, Quaternion rotation)
    {
        InstantiateObject(_pool.GetInstance(instance), position, rotation);
    }

    private void InstantiateObject(Instance instance, Vector3 position, Quaternion rotation)
    {
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);
        OnInstantiated(instance);
    }

    protected virtual void OnInstantiated(Instance instance) { }

    [System.Serializable]
    public class SpawnedObject
    {
        public Instance Instance;
        public int CopyCount;
    }
}
