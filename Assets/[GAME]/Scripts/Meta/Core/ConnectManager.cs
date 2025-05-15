using Cysharp.Threading.Tasks;
using Fusion;
using Reflex.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectManager : MonoBehaviour
{
    [SerializeField] private bool _forceSinglePlayer;

    private MetaConfig _metaConfig;
    private SceneLoader _sceneLoader;
    private NetworkRunner _runnerInstance;

    public event Action<string> SendStatusMessage;
    public event Action<string> SendShutdownMessage;

    [Inject]
    private void Construct(MetaConfig metaConfig, SceneLoader sceneLoader)
    {
        _metaConfig = metaConfig;
        _sceneLoader = sceneLoader;
    }

    public async UniTask StartGame(string nickname, string room)
    {
        await Disconnect();

        _runnerInstance = Instantiate(_metaConfig.RunnerPrefab);

        var events = _runnerInstance.GetComponent<NetworkEvents>();
        events.OnShutdown.AddListener(OnShutdown);

        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(SceneRef.FromIndex((int)SceneType.Game));

        var startArguments = new StartGameArgs()
        {
            GameMode = Application.isEditor && _forceSinglePlayer ? GameMode.Single : GameMode.Shared,
            SessionName = room,
            PlayerCount = _metaConfig.MaxPlayerCount,

            SessionProperties = new Dictionary<string, SessionProperty> { ["GameMode"] = "Shooter" },
            Scene = sceneInfo,
        };

        var statusMessage = startArguments.GameMode == GameMode.Single ? "Starting single-player..." : "Connecting...";
        SendStatusMessage?.Invoke(statusMessage);

        var startTask = _runnerInstance.StartGame(startArguments);
        await startTask;

        if (startTask.Result.Ok)
            SendStatusMessage?.Invoke("");
        else
            SendStatusMessage?.Invoke($"Connection Failed: {startTask.Result.ShutdownReason}");
    }

    public async void DisconnectClicked()
    {
        await Disconnect();
    }

    public async void BackToMenu()
    {
        await Disconnect();

        SceneManager.LoadScene(0);
    }

    public async UniTask Disconnect()
    {
        if (_runnerInstance == null)
            return;

        SendStatusMessage?.Invoke("Disconnecting...");

        var events = _runnerInstance.GetComponent<NetworkEvents>();
        events.OnShutdown.RemoveListener(OnShutdown);

        await _runnerInstance.Shutdown();
        _runnerInstance = null;

        _sceneLoader.LoadScene(SceneType.Meta);
    }

    private void OnShutdown(NetworkRunner runner, ShutdownReason reason)
    {
        SendShutdownMessage?.Invoke($"Shutdown: {reason}");    

        _sceneLoader.LoadScene(SceneType.Meta);
    }
}