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

        // Add listener for shutdowns so we can handle unexpected shutdowns
        var events = _runnerInstance.GetComponent<NetworkEvents>();
        events.OnShutdown.AddListener(OnShutdown);

        var sceneInfo = new NetworkSceneInfo();
        sceneInfo.AddSceneRef(SceneRef.FromIndex((int)SceneType.Game));

        var startArguments = new StartGameArgs()
        {
            GameMode = Application.isEditor && _forceSinglePlayer ? GameMode.Single : GameMode.Shared,
            SessionName = room,
            PlayerCount = _metaConfig.MaxPlayerCount,
            // We need to specify a session property for matchmaking to decide where the player wants to join.
            // Otherwise players from Platformer scene could connect to ThirdPersonCharacter game etc.
            SessionProperties = new Dictionary<string, SessionProperty> { ["GameMode"] = "Shooter" },
            Scene = sceneInfo,
        };

        var statusMessage = startArguments.GameMode == GameMode.Single ? "Starting single-player..." : "Connecting...";
        SendStatusMessage?.Invoke(statusMessage);

        var startTask = _runnerInstance.StartGame(startArguments);
        await startTask;

        if (startTask.Result.Ok)
        {
            SendStatusMessage?.Invoke("");
            //StatusText.text = "";
            //PanelGroup.gameObject.SetActive(false);
        }
        else
        {
            SendStatusMessage?.Invoke($"Connection Failed: {startTask.Result.ShutdownReason}");
            //StatusText.text = $"Connection Failed: {startTask.Result.ShutdownReason}";
        }
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

    //public void TogglePanelVisibility()
    //{
    //    if (PanelGroup.gameObject.activeSelf && _runnerInstance == null)
    //        return; // Panel cannot be hidden if the game is not running

    //    PanelGroup.gameObject.SetActive(!PanelGroup.gameObject.activeSelf);
    //}

    //private void Update()
    //{
    //    // Enter/Esc key is used for locking/unlocking cursor in game view.
    //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        TogglePanelVisibility();
    //    }

    //    if (PanelGroup.gameObject.activeSelf)
    //    {
    //        StartGroup.SetActive(_runnerInstance == null);
    //        DisconnectGroup.SetActive(_runnerInstance != null);
    //        RoomText.interactable = _runnerInstance == null;
    //        NicknameText.interactable = _runnerInstance == null;

    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //    }
    //}

    public async UniTask Disconnect()
    {
        if (_runnerInstance == null)
            return;

        SendStatusMessage?.Invoke("Disconnecting...");
        //PanelGroup.interactable = false;

        // Remove shutdown listener since we are disconnecting deliberately
        var events = _runnerInstance.GetComponent<NetworkEvents>();
        events.OnShutdown.RemoveListener(OnShutdown);

        await _runnerInstance.Shutdown();
        _runnerInstance = null;

        // Reset of scene network objects is needed, reload the whole scene
        _sceneLoader.LoadScene(SceneType.Meta);
    }

    private void OnShutdown(NetworkRunner runner, ShutdownReason reason)
    {
        // Unexpected shutdown happened (e.g. Host disconnected)

        // Save status into static variable, it will be used in OnEnable after scene load
        SendShutdownMessage?.Invoke($"Shutdown: {reason}");    
        Debug.LogWarning($"Shutdown: {reason}");

        // Reset of scene network objects is needed, reload the whole scene
        _sceneLoader.LoadScene(SceneType.Meta);
    }
}