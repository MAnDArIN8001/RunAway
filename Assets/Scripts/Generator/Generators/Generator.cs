using UnityEngine;

public abstract class Generator : MonoBehaviour
{
    [SerializeField] protected Transform _obstaclesInstancePoint;

    [SerializeField] protected Obstacle[] _obstacles;

    protected virtual void Awake()
    {
        if (_obstacles.Length == 0)
        {
            Debug.Log($"The generator {gameObject} doesnt contains any obstacles prefabs");
        }
    }

    public abstract Obstacle GenerateObstacle();
}
