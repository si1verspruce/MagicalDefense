using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missle : Instance, IScaleable
{
    protected const int Damage = 1;

    [SerializeField] protected float BaseDuration;
    [SerializeField] private float _lifetimeAfterDuration;

    protected bool IsActive;
    protected float Duration;
    protected float CurrentLifetime;
    private bool _isDeactivated;

    public void Launch(Vector3 targetPosition, float speed)
    {
        StartCoroutine(MoveToTarget(targetPosition, speed));
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Duration = BaseDuration;
    }

    private void OnEnable()
    {
        ResetState();
    }

    private void Update()
    {
        DoWhileAlive();
    }

    public virtual void Scale(float modifier) { }
    protected virtual void Deactivate() { }
    protected virtual void OnTargetAchieved() { }

    protected virtual void ResetState()
    {
        CurrentLifetime = 0;
        _isDeactivated = false;
    }

    protected virtual void DoWhileAlive()
    {
        if (IsActive == false)
            return;

        if (CurrentLifetime >= Duration)
        {
            if (_isDeactivated == false)
            {
                Deactivate();
                _isDeactivated = true;
            }

            if (CurrentLifetime >= _lifetimeAfterDuration)
                gameObject.SetActive(false);
        }

        CurrentLifetime += Time.deltaTime;
    }

    protected IEnumerator MoveToTarget(Vector3 targetPosition, float speed)
    {
        float scaledVelocity = speed * Time.deltaTime;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, scaledVelocity);

            yield return null;
        }

        OnTargetAchieved();
    }
}
