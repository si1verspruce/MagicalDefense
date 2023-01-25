using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy", order = 51)]
public class EnemyParameters : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private float _fallUndergroundDelay;
    [SerializeField] private float _deactivateDelay;

    public int Health => _health;
    public float MoveSpeed => _moveSpeed;
    public int Damage => _damage;
    public int Reward => _reward;
    public float FallUndergroundDelay => _fallUndergroundDelay;
    public float DeactivateDelay => _deactivateDelay;
}
