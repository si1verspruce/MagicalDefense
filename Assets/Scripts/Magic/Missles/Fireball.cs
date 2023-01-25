using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Fireball : Missle
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private ParticleSystem[] _fireEffects;
    [SerializeField] private AudioSource _audio;

    private MeshRenderer _renderer;

    protected override void Init()
    {
        base.Init();
        _renderer = GetComponent<MeshRenderer>();
    }

    public override void Scale(float modifier)
    {
        _explosion.Scale(modifier);
    }

    private void Activate()
    {
        IsActive = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        _explosion.gameObject.SetActive(true);
    }

    protected override void Deactivate()
    {
        _renderer.enabled = false;

        foreach (var effect in _fireEffects)
            effect.Stop();
    }

    protected override void OnTargetAchieved()
    {
        Activate();
    }

    protected override void ResetState()
    {
        base.ResetState();
        _audio.Play();
        IsActive = false;
        _renderer.enabled = true;
        _explosion.gameObject.SetActive(false);

        foreach (var effect in _fireEffects)
            effect.Play();
    }
}
