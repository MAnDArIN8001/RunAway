using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public event Action OnDied;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out var obstacle))
        {
            OnDied?.Invoke();
        }
    }
}
