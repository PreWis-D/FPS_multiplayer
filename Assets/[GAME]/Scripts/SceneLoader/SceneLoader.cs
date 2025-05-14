using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public AsyncOperation AsyncOperation { get; private set; }

    public void LoadScene(SceneType sceneType)
    {
        LoadingScene((int)sceneType);
    }

    private void LoadingScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}