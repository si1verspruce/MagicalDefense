using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _spawnPoint;

    private void OnEnable()
    {
        _screen.gameObject.SetActive(true);
        EnemyTime.SetTimeActivity(false);
    }

    public void SpawnUnit()
    {
        _spawner.SpawnUnit(_enemy, _spawnPoint.transform.position, _spawner.transform.localRotation);
    }

    public void Finish()
    {
        EnemyTime.SetTimeActivity(true);
        Destroy(_screen.gameObject);
        gameObject.SetActive(false);
    }

    public void DestroyScreen()
    {
        Destroy(_screen.gameObject);
    }
}
