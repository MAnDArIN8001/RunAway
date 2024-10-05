using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObstaclesController : MonoBehaviour
{
    private PlayerLife _playerLife;

    [SerializeField] private GeneratorController _generatorController;

    [SerializeField] private List<Obstacle> _obstacles;

    [Inject]
    private void Initialize(PlayerLife playerLife)
    {
        _playerLife = playerLife;
    }

    private void OnEnable()
    {
        if (_playerLife is not null)
        {
            _playerLife.OnDied += HandlePlayerDeath;
        }

        if (_generatorController is not null)
        {
            _generatorController.OnCreateObstacle += HandleNewObstacle;
        }
    }

    private void OnDisable()
    {
        if (_playerLife is not null)
        {
            _playerLife.OnDied -= HandlePlayerDeath;
        }

        if (_generatorController is not null)
        {
            _generatorController.OnCreateObstacle -= HandleNewObstacle;
        }
    }

    private void HandleNewObstacle(Obstacle obstacle)
    {
        if (!_obstacles.Contains(obstacle))
        {
            _obstacles.Add(obstacle);

            obstacle.OnDestroyed += HandleObstacleDestruction;
        }
    }

    private void HandlePlayerDeath()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.Disable();
        }
    }

    private void HandleObstacleDestruction(Obstacle obstacle)
    {
        if (_obstacles.Contains(obstacle))
        {
            _obstacles.Remove(obstacle);

            obstacle.OnDestroyed -= HandleObstacleDestruction;
        }
    }
}
