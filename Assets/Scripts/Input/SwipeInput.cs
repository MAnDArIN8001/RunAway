using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeInput : IDisposable
{
    public event Action OnSwipedUp;
    public event Action OnSwipedDown;

    private bool _isSwiping;

    private float _swipeSensitivity = 5f;

    private Vector2 _startSwipePosition;
    private Vector2 _endSwipePosition;

    private MainInput _mainInput;

    public SwipeInput(MainInput mainInput)
    {
        _mainInput = mainInput;

        _mainInput.Enable();

        _mainInput.Player.SwipeEvent.started += HandleSwipe;
        _mainInput.Player.SwipeEvent.canceled += HandleSwipe;
    }

    public void Dispose()
    {
        _mainInput.Player.SwipeEvent.started -= HandleSwipe;
        _mainInput.Player.SwipeEvent.canceled -= HandleSwipe;

        _mainInput.Disable();

        OnSwipedUp = null;
        OnSwipedDown = null;
    }

    private void HandleSwipe(InputAction.CallbackContext context)
    {
        if (context.started && !_isSwiping)
        {
            _startSwipePosition = _mainInput.Player.SwipeData.ReadValue<Vector2>();
            _isSwiping = true;
        }

        if (context.canceled && _isSwiping)
        {
            _endSwipePosition = _mainInput.Player.SwipeData.ReadValue<Vector2>();
            _isSwiping = false;

            DetectSwipe();
        }
    }

    private void DetectSwipe() 
    {
        Vector2 swipeDelta = _endSwipePosition - _startSwipePosition;

        if (Mathf.Abs(swipeDelta.y) < _swipeSensitivity)
        {
            Debug.LogWarning("Too short swipe");

            return;
        }

        if (swipeDelta.y > 0)
        {
            OnSwipedUp?.Invoke();
        }
        else if (swipeDelta.y < 0)
        {
            OnSwipedDown?.Invoke();
        }
    }
}
