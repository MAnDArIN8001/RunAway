using Cinemachine;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Quaternion _playerInstanceRotation;

    [SerializeField] private Transform _playerInstancePoint;

    [SerializeField] private GameObject _playerPrefab;

    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;

    public override void InstallBindings()
    {
        GameObject playerInstance = Container.InstantiatePrefab(_playerPrefab, _playerInstancePoint.position, _playerInstanceRotation, null);

        Container.Bind<PlayerLife>().FromInstance(playerInstance.GetComponent<PlayerLife>());

        _playerVirtualCamera.Follow = playerInstance.transform;
    }
}
