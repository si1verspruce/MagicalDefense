using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : Instance
{
    private const string AnimatorDefaultState = "Run";
    private const string AnimatorSpeedFloat = "Speed";
    private const string AnimatorDiedTrigger = "Died";

    [SerializeField] private EnemyParameters _parameters;
    [SerializeField] private float _animationSpeed;

    private Animator _animator;
    private Player _player;
    private Collider _collider;
    private Rigidbody _mainRigidbody;
    private float _currentHealth;
    private float _currentMoveSpeed;

    public float MoveSpeed => _currentMoveSpeed;
    public int Damage => _parameters.Damage;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _mainRigidbody = GetComponent<Rigidbody>();

        _animator.speed = _animationSpeed;
    }

    private void OnEnable()
    {
        EnemyTime.TimeActivityChanged += OnTimeActivityChanged;
        UpdateState(true);
        _animator.Play(AnimatorDefaultState);
        _mainRigidbody.isKinematic = true;
        _currentHealth = _parameters.Health;
    }

    private void OnDisable()
    {
        EnemyTime.TimeActivityChanged -= OnTimeActivityChanged;
    }

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        UpdateState(false);
        _animator.SetTrigger(AnimatorDiedTrigger);

        StartCoroutine(DoAfterDelay(FallUnderground, _parameters.FallUndergroundDelay));
        StartCoroutine(DoAfterDelay(DisableThis, _parameters.DeactivateDelay));

        _player.AddMoney(_parameters.Reward);
    }

    private IEnumerator DoAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);

        action();
    }

    private void FallUnderground()
    {
        _mainRigidbody.isKinematic = false;
    }

    private void DisableThis()
    {
        gameObject.SetActive(false);
    }

    private void UpdateState(bool isActive)
    {
        _collider.enabled = isActive;
        _currentMoveSpeed = _parameters.MoveSpeed * Convert.ToInt32(isActive);
    }

    private void OnTimeActivityChanged(bool isTimeActive)
    {
        if (isTimeActive == false)
            _animator.SetFloat(AnimatorSpeedFloat, 0);
        else if (_currentHealth > 0)
            _animator.SetFloat(AnimatorSpeedFloat, 1);
    }
}
