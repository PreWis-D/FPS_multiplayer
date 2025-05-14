using Reflex.Attributes;
using UnityEngine;

public class EntryPointMeta : MonoBehaviour
{
    private UIHandlerMeta _uiHandlerMeta;

    [Inject]
    private void Construct(UIHandlerMeta uIHandlerMeta)
    {
        _uiHandlerMeta = uIHandlerMeta;
    }

    private void Start()
    {
        _uiHandlerMeta.Init();
    }
}