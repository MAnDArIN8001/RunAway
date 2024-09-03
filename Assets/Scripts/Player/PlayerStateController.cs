using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStateController : MonoBehaviour
{
    public event Action<bool> OnSlideStateChanged;
    public event Action<bool> OnJumpStateChanged;

    private bool _isJumping;
    private bool _isSliding;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slideTime;

    [SerializeField] private BehaviourState _currentState;

    [SerializeField] private State _state;

    private Rigidbody2D _rigidbody;

    private SwipeInput _swipeInput;

    [Inject]
    private void Initialize(SwipeInput swipeInput)
    {
        _swipeInput = swipeInput;
    }

    private void Awake()
    {
         _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (_swipeInput is not null)
        {
            _swipeInput.OnSwipedUp += HandleSwipeUp;
            _swipeInput.OnSwipedDown += HandleSwipeDown;
        }
    }

    private void OnDisable()
    {
        if (_swipeInput is not null)
        {
            _swipeInput.OnSwipedUp -= HandleSwipeUp;
            _swipeInput.OnSwipedDown -= HandleSwipeDown;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_state is not null)
        {
            _state.OnCollisionEnter2D(collision);
        }
    }

    private void HandleSwipeUp()
    {
        if (_currentState == BehaviourState.Jumping)
        {
            return;
        }

        if (_state is not null
            && _currentState == BehaviourState.Sliding)
        {
            _state.BreakState();
            _state.Exit();
            _state = null;

            _isSliding = false;

            OnSlideStateChanged?.Invoke(_isSliding);
        }

        _currentState = BehaviourState.Jumping;

        _state = new JumpState(_jumpForce, _rigidbody);
        _state.Enter();
        _state.OnStateClosed += HandleJumpEnd;

        _isJumping = true;

        OnJumpStateChanged?.Invoke(_isJumping);
    }

    private void HandleSwipeDown()
    {
        if (_currentState == BehaviourState.Sliding)
        {
            return;
        }

        if (_state is not null
            && _currentState == BehaviourState.Jumping)
        {
            _state.BreakState();
            _state.Exit();
            _state = null;

            _isJumping = false;

            OnJumpStateChanged?.Invoke(_isJumping);
        }

        _currentState = BehaviourState.Sliding;

        _state = new SlideState(_slideTime, this);
        _state.Enter();
        _state.OnStateClosed += HandleSlideEnd;

        _isSliding = true;

        OnSlideStateChanged?.Invoke(_isSliding);
    }

    private void HandleJumpEnd()
    {
        _isJumping = false;
        _currentState = BehaviourState.Runing;

        OnJumpStateChanged?.Invoke(_isJumping);

        _state.OnStateClosed -= HandleJumpEnd;
    }

    private void HandleSlideEnd()
    {
        _isSliding = false;
        _currentState = BehaviourState.Runing;

        OnSlideStateChanged?.Invoke(_isSliding);

        _state.OnStateClosed -= HandleSlideEnd;
    }
}
