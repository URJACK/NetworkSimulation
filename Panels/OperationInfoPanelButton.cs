using UnityEngine;
using System.Collections;
using System;

public class OperationInfoPanelButton : MonoBehaviour
{
    /// <summary>
    /// 点击
    /// </summary>
    /// <param name="index">被选中的按钮编号</param>
    public void Click(int index)
    {
        if(index == SELECT_LISTEN)
        {
            SelectListen();
        }
        else if (index == SELECT_SEND)
        {
            SelectSend();
        }
        else if (index == SELECT_QUIT)
        {
            SelectQuit();
        }
        else if (index == SELECT_STOPLISTEN)
        {
            SelectStopListen();
        }
    }

    private void SelectStopListen()
    {
        PanelStack.GetInstance().StopListen();
        PanelStack.GetInstance().SetOperationInfoByDeviceInfo();
    }

    private void SelectQuit()
    {
        PanelStack.GetInstance().Pop(PanelStack.GetInstance().GetOperationInfoPanel());
    }

    private void SelectSend()
    {
        PanelStack.GetInstance().SendDebugListenMessage();
    }

    private void SelectListen()
    {
        PanelStack.GetInstance().StartListen();
        PanelStack.GetInstance().SetOperationInfoByDeviceInfo();
    }

    public static int SELECT_LISTEN = 1;
    public static int SELECT_SEND = 2;
    public static int SELECT_QUIT = 3;
    public static int SELECT_STOPLISTEN = 4;
}
