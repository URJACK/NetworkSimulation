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
    }

    private void SelectQuit()
    {
        PanelStack.GetInstance().Pop(PanelStack.GetInstance().GetOperationInfoPanel());
    }

    private void SelectSend()
    {
        Debug.Log("send");
    }

    private void SelectListen()
    {
        Debug.Log("listen");
    }

    public static int SELECT_LISTEN = 1;
    public static int SELECT_SEND = 2;
    public static int SELECT_QUIT = 3;
}
