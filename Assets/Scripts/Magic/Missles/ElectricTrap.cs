using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : Missle
{
    [SerializeField] private int _triggerCount;
    [SerializeField] private ParticleSystem _spark;
    [SerializeField] private AudioSource _sparkSound;
    [SerializeField] private AudioSource _electricity;

    private int _currentTriggerCount;
    private float _defaultDuration;

    protected override void Init()
    {
        base.Init();
        _defaultDuration = BaseDuration;
    }

    public override void Scale(float modifier)
    {
        BaseDuration = _defaultDuration * modifier;
        Duration = BaseDuration;
    }

    protected override void ResetState()
    {
        base.ResetState();
        _electricity.Play();
        IsActive = true;
        _currentTriggerCount = _triggerCount;
        _spark.time = 0;
        _spark.Stop();
        _spark.gameObject.SetActive(false);
    }

    protected override void Deactivate()
    {
        if (_spark.gameObject.activeSelf == false)
            gameObject.SetActive(false);

        _currentTriggerCount = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_currentTriggerCount > 0)
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(Damage);
                _sparkSound.Play();
                _spark.gameObject.SetActive(true);
                _spark.time = 0;
                _spark.Play();
                _currentTriggerCount--;

                if (_currentTriggerCount <= 0)
                    CurrentLifetime = Duration;
            }
    }
}
