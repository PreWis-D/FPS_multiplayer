using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadScene(SceneType sceneType)
    {
        LoadingScene((int)sceneType);
    }

    private void LoadingScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}