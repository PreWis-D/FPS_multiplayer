using Reflex.Core;
using UnityEngine;

public class InstallerMeta : MonoBehaviour, IInstaller
{
    [SerializeField] private MetaConfig _metaConfig;

    private ContainerBuilder _containerBuilder;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        _containerBuilder = containerBuilder;

        BindMetaConfig();
        BindUIHandler();
        BindSceneLoader();
        BindConnectManager();
    }

    private void BindUIHandler()
    {
        _containerBuilder.AddSingleton(Instantiate(_metaConfig.UIHandlerMetaPrefab));
    }

    private void BindSceneLoader()
    {
        _containerBuilder.AddSingleton(typeof(SceneLoader));
    }

    private void BindMetaConfig()
    {
        _containerBuilder.AddSingleton(_metaConfig);
    }

    private void BindConnectManager()
    {
        _containerBuilder.AddSingleton(Instantiate(_metaConfig.ConnectManagerPrefab));
    }
}