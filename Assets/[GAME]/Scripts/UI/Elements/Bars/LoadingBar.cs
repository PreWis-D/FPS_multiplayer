using UnityEditor;
using UnityEngine;

public class LoadingBar : ProgressBar
{
    private SceneLoader _loader;

    public void Init(SceneLoader loader)
    {
        _loader = loader;
        _initialValue = 0;
    }

    protected override void Update()
    {
        _currentValue = _loader.AsyncOperation.progress;

        base.Update();
    }
}