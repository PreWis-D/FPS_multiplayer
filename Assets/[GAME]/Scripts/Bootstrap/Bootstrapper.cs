using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private SceneType _sceneType = SceneType.Meta;

    private SceneLoader _loader;

    private void Awake()
    {
        _loader = new SceneLoader();
    }

    private void OnEnable()
    {
        StartGame();
    }

    private void StartGame()
    {
        _loader.LoadScene(_sceneType);    
    }
}