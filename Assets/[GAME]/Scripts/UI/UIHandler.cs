using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UIHandler : MonoBehaviour
{
    #region Core
    private List<IPanel> Panels = new List<IPanel>();

    public virtual void Init()
    {
        Panels.AddRange(GetComponentsInChildren<IPanel>());

        foreach (var panel in Panels)
            panel.Init();

        HideAllPanels();
    }

    protected void HideAllPanels(List<IPanel> extencionPanels = null)
    {
        List<IPanel> panelList = Panels;

        if (extencionPanels != null)
            panelList = GetUniquePanels(extencionPanels);

        foreach (var panel in panelList)
            panel.Hide();
    }

    protected IPanel GetPanel(PanelType type)
    {
        return Panels.First(p => p.Type == type);
    }

    protected List<IPanel> GetUniquePanels(List<IPanel> extencionPanels)
    {
        return Panels.Union(extencionPanels).ToList();
    }
    #endregion

    #region Enters
    public void EnterLobby()
    {
        HideAllPanels();
        GetPanel(PanelType.Lobby).Show();
    }

    public void EnterGameLoop()
    {
        HideAllPanels();
        GetPanel(PanelType.GameLoop).Show();
    }

    public void EnterWin()
    {
        HideAllPanels();
        GetPanel(PanelType.Win).Show();
    }

    public void EnterLose()
    {
        HideAllPanels();
        GetPanel(PanelType.Lose).Show();
    }

    public void EnterPause()
    {
        HideAllPanels();
        GetPanel(PanelType.Settings).Show();
    }
    #endregion
}