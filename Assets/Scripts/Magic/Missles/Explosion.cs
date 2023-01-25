using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : Missle
{
    [SerializeField] private AudioSource _audio;

    private SphereCollider _damageArea;
    private Vector3 _defaultScale;

    protected override void Init()
    {
        base.Init();
        _damageArea = GetComponent<SphereCollider>();
        IsActive = true;
        _defaultScale = transform.localScale;

        if (Duration <= 0.1f)
            Duration = 0.1f;
    }

    public override void Scale(float modifier)
    {
        transform.localScale = _defaultScale * modifier;
    }

    protected override void Deactivate()
    {
        _damageArea.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
            enemy.ApplyDamage(Damage);
    }

    protected override void ResetState()
    {
        base.ResetState();
        _damageArea.enabled = true;
        _audio.Play();
    }
}
