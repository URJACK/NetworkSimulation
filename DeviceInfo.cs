using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 设备信息
/// </summary>
public class DeviceInfo
{
    /// <summary>
    /// 设备的名字
    /// </summary>
    private string name;
    /// <summary>
    /// 设备所处的层级
    /// </summary>
    private int layer;
    /// <summary>
    /// 用来记录相邻连线的GameObject实体
    /// </summary>
    private List<GameObject> ports = new List<GameObject>();

    public string Name { get => name; set => name = value; }
    public int Layer { get => layer; set => layer = value; }

    public DeviceInfo(string name,int layer)
    {
        this.name = name;
        this.layer = layer;
    }

    public string GetLayerString()
    {
        if (layer == LAYER_SWITCH)
        {
            return "交换机";
        }
        else if (layer == LAYER_ROUTER)
        {
            return "路由器";
        }
        else if (layer == LAYER_PC)
        {
            return "终端";
        }
        else
        {
            return "不存在的层级";
        }
    }
    public string GetPortsString()
    {
        int index = 0;
        string str = "";
        foreach (GameObject obj in ports)
        {
            if (obj != null)
            {
                str += string.Format("%d:linked; ", index);
            }
        }
        if (str == "")
        {
            return "无";
        }
        else
        {
            return str;
        }
    }
    public override string ToString()
    {
        return name + ":" + layer;
    }

    public static int LAYER_SWITCH = ControlDevicePanelManager.SWITCH;
    public static int LAYER_ROUTER = ControlDevicePanelManager.ROUTER;
    public static int LAYER_PC = ControlDevicePanelManager.PC;
}