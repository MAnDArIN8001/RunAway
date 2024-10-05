using System;
using UnityEngine;

public class Obstacle : MonoBehaviour 
{
    public event Action<Obstacle> OnDestroyed;
    private bool _isActive = true;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private Vector2 _movementDirection;

    private void FixedUpdate()
    {
        if (_isActive)
        {
            transform.Translate(_movementDirection * _movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        if (collision.TryGetComponent<Destroyer>(out var destroyer))
        {
            Destroy(gameObject);

            OnDestroyed?.Invoke(this);
        }
    }

    public void Disable()
    {
        _isActive = false;
    }
}
