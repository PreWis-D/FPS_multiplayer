using System.Collections;
using UnityEngine;

public class UIHandlerMeta : UIHandler
{
    public override void Init()
    {
        base.Init();

        GetPanel(PanelType.Lobby).Show();
    }
}