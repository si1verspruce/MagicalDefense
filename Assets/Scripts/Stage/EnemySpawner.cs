using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : Spawner, IResetOnRestart
{
    [SerializeField] private SpawnedObject _baseEnemy;
    [SerializeField] private SpawnedEnemy[] _advancedEnemies;
    [SerializeField] private Instance _boss;
    [SerializeField] private Vector3 _minWorldPosition;
    [SerializeField] private Vector3 _maxWorldPosition;
    [SerializeField] private Vector3 _bossSpawnPosition;
    [SerializeField] private Player _player;
    [SerializeField] private Session _stage;
    [SerializeField, Min(0)] private float _perStageSpawnFrequencyDivider;

    private Dictionary<Instance, Vector2> _spawnNumbersByInstances = new Dictionary<Instance, Vector2>();
    private Instance _currentBoss;
    private float _defaultInterval;

    private void Awake()
    {
        SpawnedObject[] spawnedObjects = new SpawnedObject[1 + _advancedEnemies.Length];
        spawnedObjects[0] = _baseEnemy;
        int advancedEnemiesFirstPosition = 1;
        _defaultInterval = Interval;

        for (int i = advancedEnemiesFirstPosition; i < spawnedObjects.Length; i++)
        {
            spawnedObjects[i] = _advancedEnemies[i - 1];
        }

        Init(spawnedObjects);
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        if (_currentBoss != null)
        {
            Destroy(_currentBoss.gameObject);
            _currentBoss = null;
        }

        ScaleInterval();
        FillSpawnNumberDictionary();
        TrySpawnBoss();
    }

    public void SpawnUnit(Instance instance, Vector3 position, Quaternion rotation)
    {
        Spawn(instance, position, rotation);
    }

    protected override void SpawnPerInterval()
    {
        if (EnemyTime.IsActive)
            base.SpawnPerInterval();
    }

    protected override Instance GetSpawnedInstance()
    {
        float value = Random.Range(0f, 1f);

        foreach (var spawnNumbers in _spawnNumbersByInstances)
            if (value >= spawnNumbers.Value.x && value <= spawnNumbers.Value.y)
                return spawnNumbers.Key;

        return null;
    }

    protected override Quaternion GetSpawnedObjectRotation()
    {
        return transform.rotation;
    }

    protected override Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(_minWorldPosition.x, _maxWorldPosition.x);
        float positionY = Random.Range(_minWorldPosition.y, _maxWorldPosition.y);
        float positionZ = Random.Range(_minWorldPosition.z, _maxWorldPosition.z);

        return new Vector3(positionX, positionY, positionZ);
    }

    protected override void OnInstantiated(Instance instance)
    {
        instance.GetComponent<Enemy>().Init(_player);
    }

    private void ScaleInterval()
    {
        Interval = _defaultInterval / (1 + _stage.Number * _perStageSpawnFrequencyDivider);
    }

    private void TrySpawnBoss()
    {
        if (_stage.Number == _stage.BossNumber)
        {
            _currentBoss = Instantiate(_boss, _bossSpawnPosition, GetSpawnedObjectRotation(), transform);
            ((Enemy)_currentBoss).Init(_player);
        }
    }

    private Dictionary<Instance, float> GetSpawnChancesByInstances()
    {
        Dictionary<Instance, float> spawnChancesByInstances = new Dictionary<Instance, float>();
        float totalSpawnChance = 0;

        foreach (var spawnedEnemy in _advancedEnemies)
        {
            float spawnChance = 1 - 1 / (1 + spawnedEnemy.SpawnChancePerStageModifier * _stage.Number);
            spawnChancesByInstances[spawnedEnemy.Instance] = spawnChance;
            totalSpawnChance += spawnChance;
        }

        if (totalSpawnChance > 1)
        {
            DownscaleSpawnChances(spawnChancesByInstances, totalSpawnChance);
            spawnChancesByInstances[_baseEnemy.Instance] = 0;
        }
        else
        {
            spawnChancesByInstances[_baseEnemy.Instance] = 1 - totalSpawnChance;
        }

        return spawnChancesByInstances;
    }

    private void DownscaleSpawnChances(Dictionary<Instance, float> spawnChancesByInstances, float totalSpawnChance)
    {
        float multiplier = 1 / totalSpawnChance;
        var instances = spawnChancesByInstances.Keys.ToList();

        foreach (var instance in instances)
            spawnChancesByInstances[instance] *= multiplier;
    }

    private void FillSpawnNumberDictionary()
    {
        Dictionary<Instance, float> spawnChancesByInstances = GetSpawnChancesByInstances();
        _spawnNumbersByInstances[_baseEnemy.Instance] = new Vector2(0, spawnChancesByInstances[_baseEnemy.Instance]);
        float lastMaxNumber = spawnChancesByInstances[_baseEnemy.Instance];
        float roundingTreshold = 0.999f;

        foreach (var spawnedEnemy in _advancedEnemies)
        {
            if (lastMaxNumber + spawnChancesByInstances[spawnedEnemy.Instance] >= roundingTreshold)
            {
                _spawnNumbersByInstances[spawnedEnemy.Instance] = new Vector2(lastMaxNumber, 1);
            }
            else
            {
                _spawnNumbersByInstances[spawnedEnemy.Instance] = new Vector2(lastMaxNumber, lastMaxNumber + spawnChancesByInstances[spawnedEnemy.Instance]);
                lastMaxNumber = _spawnNumbersByInstances[spawnedEnemy.Instance].y;
            }
        }
    }

    [System.Serializable]
    public class SpawnedEnemy : SpawnedObject
    {
        [Range(0, 1)] public float SpawnChancePerStageModifier;
    }
}
