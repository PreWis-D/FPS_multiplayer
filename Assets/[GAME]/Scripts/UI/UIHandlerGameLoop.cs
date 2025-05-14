using System.Collections;
using UnityEngine;

public class UIHandlerGameLoop : UIHandler
{
    public override void Init()
    {
        base.Init();

        GetPanel(PanelType.Lobby).Show();
    }
}