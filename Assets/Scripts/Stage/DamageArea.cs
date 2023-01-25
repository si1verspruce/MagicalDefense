using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _player.ApplyDamage(enemy.Damage);
            enemy.gameObject.SetActive(false);
        }
    }
}
