using Reflex.Core;
using UnityEngine;

public class InstallerGameloop : MonoBehaviour, IInstaller
{
    [SerializeField] private GameloopConfig _gameloopConfig;

    private ContainerBuilder _containerBuilder;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        _containerBuilder = containerBuilder;

        BindGameloopConfig();
        BindUIHandler();
        BindPlayerInput();
        BindInputManager();
        BindLevel();
    }

    private void BindGameloopConfig()
    {
        _containerBuilder.AddSingleton(_gameloopConfig);
    }

    private void BindUIHandler()
    {
        _containerBuilder.AddSingleton(Instantiate(_gameloopConfig.PrefabsConfig.UIHandlerGameLoopPrefab));
    }

    private void BindPlayerInput()
    {
        _containerBuilder.AddSingleton(typeof(PlayerActions));
    }

    private void BindInputManager()
    {
        _containerBuilder.AddSingleton(typeof(InputManager));
    }

    private void BindLevel()
    {
        _containerBuilder.AddSingleton(Instantiate(_gameloopConfig.PrefabsConfig.LevelPrefab));
    }
}