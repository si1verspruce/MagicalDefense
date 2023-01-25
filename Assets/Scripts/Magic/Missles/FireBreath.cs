using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : Missle
{
    [SerializeField] private ParticleSystem _fireBreath;
    [SerializeField] private AudioSource _audio;

    protected override void Deactivate()
    {
        base.Deactivate();
        _fireBreath.Stop();
    }

    protected override void Init()
    {
        base.Init();
        IsActive = true;
    }

    protected override void ResetState()
    {
        base.ResetState();
        _audio.Play();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
            enemy.ApplyDamage(Damage);
    }
}
