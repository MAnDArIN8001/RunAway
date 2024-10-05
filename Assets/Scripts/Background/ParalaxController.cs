using UnityEngine;
using Zenject;

public class ParalaxController : MonoBehaviour
{
    [SerializeField] private Paralx[] _paralaxes;

    private PlayerLife _playerLife;

    [Inject]
    private void Initialize(PlayerLife playerLife)
    {
        _playerLife = playerLife;
    }

    private void OnEnable()
    {
        if (_playerLife is not null)
        {
            _playerLife.OnDied += DisableParalax;
        }
    }

    private void OnDisable()
    {
        if (_playerLife is not null)
        {
            _playerLife.OnDied -= DisableParalax;
        }
    }

    private void DisableParalax()
    {
        ChangeParalxSpeed(0);
    }

    private void ChangeParalxSpeed(float newSpeed)
    {
        foreach (var paralax in _paralaxes)
        {
            paralax.ScrollSpeed = newSpeed;
        }
    }
}
