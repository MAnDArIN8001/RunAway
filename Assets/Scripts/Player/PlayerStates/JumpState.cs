using System;
using UnityEngine;

public class JumpState : State
{
    public override event Action OnStateClosed;

    private float _jumpForce;

    private Rigidbody2D _rigidbody;

    public JumpState(float jumpForce, Rigidbody2D rigidbody)
    {
        _jumpForce = jumpForce;
        _rigidbody = rigidbody;
    }

    public override void Enter()
    {
        _rigidbody.velocity = Vector3.up * _jumpForce;
    }

    public override void Exit()
    {
        OnStateClosed = null;
    }

    public override void BreakState()
    {
        _rigidbody.velocity = Vector3.down * _jumpForce;

        OnStateClosed?.Invoke();
    }

    public override void Update()
    {

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            OnStateClosed?.Invoke();
        }
    }
}
