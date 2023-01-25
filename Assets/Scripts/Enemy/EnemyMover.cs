using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        float moveSpeed = _enemy.MoveSpeed;
        float scaledMoveSpeed = moveSpeed * Time.deltaTime * Convert.ToInt32(EnemyTime.IsActive);
        transform.Translate(Vector3.forward * scaledMoveSpeed);
    }
}
