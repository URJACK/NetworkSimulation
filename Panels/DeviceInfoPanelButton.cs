using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeviceInfoPanelButton : MonoBehaviour
{
    public void Select(int index)
    {
        if(index == 1)
        {
            SelectQuit();
        }
        else if (index == 2)
        {
            SelectSave();
        }
        else if (index == 3)
        {
            Debug.Log("打开编译界面");
        }
        else if (index == 4)
        {
            Debug.Log("打开调试界面");
        }
    }
    private void SelectQuit()
    {
        PanelStack panelStack = PanelStack.GetInstance();
        panelStack.Pop(panelStack.GetDeviceInfoPanel());
    }
    private void SelectSave()
    {
        PanelStack panelStack = PanelStack.GetInstance();
        panelStack.SaveInfoOfDeviceInfoPanel();
    }
}
