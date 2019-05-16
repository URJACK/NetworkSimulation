using UnityEngine;
using System.Collections;

public class CompilePanelButton : MonoBehaviour
{
    public void Click(int index)
    {
        if(index < 5)
        {
            PanelStack.GetInstance().SetInfoOfCompileInfoPanelByDeviceInfo(index);
        }
        else if(index == SELECT_SAVEINFOANDQUIT)
        {
            SelectQuit();
        }
        else if(index == SELECT_CONSOLEINFO)
        {
            SelectConsoleInfo();
        }
        else if(index == SELECT_STARTCOMPILE)
        {
            SelectStartCompile();
        }
    }

    private void SelectQuit()
    {
        PanelStack panelStack = PanelStack.GetInstance();
        panelStack.Pop(panelStack.GetCompileInfoPanel());
    }
    private void SelectConsoleInfo()
    {
        PanelStack.GetInstance().SetCompiledInfomationByDeviceInfo();
    }
    private void SelectStartCompile()
    {
        PanelStack.GetInstance().StartPortCompile();
    }
    public static int SELECT_SAVEINFOANDQUIT = 5;
    public static int SELECT_CONSOLEINFO = 6;
    public static int SELECT_STARTCOMPILE = 7;
}
