using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellReadyEffect : MonoBehaviour
{
    [SerializeField] private PlayerCaster _caster;
    [SerializeField] private ParticleSystem _effect;

    private void OnEnable()
    {
        _caster.SpellDone += ChangeState;
    }

    private void OnDisable()
    {
        _caster.SpellDone -= ChangeState;
    }

    private void ChangeState(bool isSpellDone)
    {
        if (isSpellDone)
        {
            _effect.Simulate(_effect.main.duration);
            _effect.Play();
        }
        else
        {
            _effect.Stop(withChildren: true, stopBehavior: ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
