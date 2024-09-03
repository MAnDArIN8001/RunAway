using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainInput>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<SwipeInput>().AsSingle().NonLazy();
    }
}
