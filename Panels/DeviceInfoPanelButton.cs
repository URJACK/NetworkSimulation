﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

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
            SelectCompile();
        }
        else if (index == 4)
        {
            SelectOperation();
        }
    }

    private void SelectOperation()
    {
        PanelStack panelStack = PanelStack.GetInstance();
        panelStack.SetOperationInfoByDeviceInfo();
        panelStack.Push(panelStack.GetOperationInfoPanel());
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
        MyLogger.GetInstance().Log("保存设备信息完成");
    }
    private void SelectCompile()
    {
        PanelStack panelStack = PanelStack.GetInstance();
        panelStack.Push(panelStack.GetCompileInfoPanel());
    }
}
