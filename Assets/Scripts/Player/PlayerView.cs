using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private PlayerStateController _playerStateController;

    private Animator _animator;

    private void Awake()
    {
        _playerStateController = GetComponent<PlayerStateController>();

        if (_playerStateController is null)
        {
            Debug.LogError($"The gameObject {gameObject} doesnt containse component PlayerStateController");
        }

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_playerStateController is not null)
        {
            _playerStateController.OnJumpStateChanged += HandleJumpStateChangings;
            _playerStateController.OnSlideStateChanged += HandleSlideStateChangings;
        }
    }

    private void OnDisable()
    {
        if (_playerStateController is not null)
        {
            _playerStateController.OnJumpStateChanged -= HandleJumpStateChangings;
            _playerStateController.OnSlideStateChanged -= HandleSlideStateChangings;
        }
    }

    private void HandleJumpStateChangings(bool newJumpState)
    {
        _animator.SetBool(PlayerAnimatorConsts.IsJumpingKey, newJumpState);
    }

    private void HandleSlideStateChangings(bool newSlideState)
    {
        _animator.SetBool(PlayerAnimatorConsts.IsSlidingKey, newSlideState);
    }
}
