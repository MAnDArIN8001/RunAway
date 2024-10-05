using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GeneratorController : MonoBehaviour
{
    public event Action<Obstacle> OnCreateObstacle;

    private bool _isEnabled = true;

    [SerializeField] private float _generationMinReloadTime;
    [SerializeField] private float _generationMaxReloadTime;

    [SerializeField] private Generator _generator;

    private PlayerLife _playerLife;

    [Inject]
    private void Initialize(PlayerLife playerLife)
    {
        _playerLife = playerLife;
    }

    private void Awake()
    {
        GenerateObstacle();

        StartCoroutine(ReloadGenerator());
    }

    private void OnEnable()
    {
        if (_playerLife is not null)
        {
            _playerLife.OnDied += DisableGenerator;
        }
    }

    private void OnDisable()
    {
        if (_playerLife is not null)
        {
            _playerLife.OnDied -= DisableGenerator;
        }
    }

    private void GenerateObstacle()
    {
        Obstacle obstacleInstance = _generator.GenerateObstacle();

        OnCreateObstacle?.Invoke(obstacleInstance);
    }

    private void DisableGenerator()
    {
        _isEnabled = false;

        StopAllCoroutines();
    }

    private IEnumerator ReloadGenerator()
    {
        while (_isEnabled)
        {
            float randomReloadTime = UnityEngine.Random.Range(_generationMinReloadTime, _generationMaxReloadTime);

            yield return new WaitForSeconds(randomReloadTime);

            GenerateObstacle();
        }
    }
}
