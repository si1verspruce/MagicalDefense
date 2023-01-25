using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : ElectricTrap, IScaleable
{
    [SerializeField] private AudioSource _voltage;
    [SerializeField] private float _targetPositionZ;
    [SerializeField] private float _startPostionZ;
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;
    [SerializeField] private float _changeDirectionStep;
    [SerializeField] private float _moveSpeed;

    private float _defaultMoveSpeed;
    private Vector3 _targetPosition;
    private bool _isMovingForward = true;

    protected override void Init()
    {
        base.Init();
        _defaultMoveSpeed = _moveSpeed;
    }

    public override void Scale(float modifier)
    {
        _moveSpeed = _defaultMoveSpeed * modifier;
    }

    protected override void DoWhileAlive()
    {
        base.DoWhileAlive();

        if (_isMovingForward)
        {
            if (transform.position.z >= _targetPositionZ)
                SetMoveDirection(false);
        }
        else
        {
            if (transform.position.z <= _startPostionZ)
                SetMoveDirection(true);
        }

        if (transform.position == _targetPosition)
            _targetPosition = new Vector3(Random.Range(_minOffset, _maxOffset), transform.position.y, transform.position.z + _changeDirectionStep);

        transform.position =  Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
    }

    protected override void ResetState()
    {
        base.ResetState();
        _isMovingForward = true;
        SetMoveDirection(_isMovingForward);
        _targetPosition = transform.position;
        _voltage.Play();

    }

    private void SetMoveDirection(bool isMovingForward)
    {
        _isMovingForward = isMovingForward;

        if ((_isMovingForward && _changeDirectionStep < 0) ||
            (_isMovingForward == false && _changeDirectionStep > 0))
        {
            _changeDirectionStep *= -1;
        }
    }
}
