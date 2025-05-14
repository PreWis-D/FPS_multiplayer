using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : BasePanel
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private TMP_InputField _nicknameText;
    [SerializeField] private TMP_InputField _roomText;
    [SerializeField] private TMP_Text _statusText;

    private ConnectManager _connectManager;

    private static string _shutdownStatus;
    private const string _saveKey = "PlayerName";

    [Inject]
    public void Construct(ConnectManager connectManager)
    {
        _connectManager = connectManager;
    }

    public override void Init()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);     
        _exitButton.onClick.AddListener(OnExitButtonClicked);

        _connectManager.SendStatusMessage += OnSendStatusMessage;

        UpdateInfo();
    }

    private void UpdateInfo()
    {
        var nickname = PlayerPrefs.GetString(_saveKey);
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = "Player" + Random.Range(10000, 100000);
        }

        _nicknameText.text = nickname;

        _statusText.SetText(_shutdownStatus != null ? _shutdownStatus : string.Empty);
        _shutdownStatus = null;
    }

    #region Events
    private void OnStartButtonClicked()
    {
        PlayerPrefs.SetString(_saveKey, _nicknameText.text);
        _connectManager.StartGame(_nicknameText.text, _roomText.text).Forget();
    }

    private void OnSendStatusMessage(string text)
    {
        _statusText.SetText(text);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
    #endregion

    private void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);

        _connectManager.SendStatusMessage -= OnSendStatusMessage;
    }
}