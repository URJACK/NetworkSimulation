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

    private int portsLength;
    /// <summary>
    /// 用来记录相邻连线的GameObject实体
    /// </summary>
    private List<GameObject> ports = new List<GameObject>();

    public string Name { get => name; set => name = value; }
    public int Layer { get => layer; set => layer = value; }

    public DeviceInfo(string name, int layer)
    {
        this.name = name;
        this.layer = layer;
        if (layer == LAYER_PC)
        {
            portsLength = LENGTH_PC;
        }
        else if (layer == LAYER_ROUTER)
        {
            portsLength = LENGTH_ROUTER;
        }
        else if (layer == LAYER_SWITCH)
        {
            portsLength = LENGTH_SWITCH;
        }
        else
        {
            portsLength = 0;
        }
    }
    /// <summary>
    /// 检查自己端口数量是否足够
    /// </summary>
    /// <returns>如果端口数量足够返回true</returns>
    public bool IsPortsEnough()
    {
        Debug.Log("Name:" + name + "  Count:" + ports.Count);
        if(ports.Count < portsLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 检查当前是否能够可以加入该连线
    /// 如果该连线未加入，则可以被加入
    /// </summary>
    /// <param name="line">被加入的连线对象</param>
    /// <returns>是否可以加入该连线到该结点</returns>
    public bool IsLinkable(GameObject line)
    {
        if (ports.Count < this.portsLength)
        {
            return !ports.Contains(line);
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 将当前设备加入连线
    /// </summary>
    /// <param name="line">被加入的连线对象</param>
    public void Link(GameObject line)
    {
        this.ports.Add(line);
    }
    /// <summary>
    /// 设备中删除这条连线
    /// </summary>
    /// <param name="line">被删除的连线对象实体</param>
    /// <returns>删除是否成功</returns>
    public bool UnLink(GameObject line)
    {
        return ports.Remove(line);
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
    public static int LENGTH_SWITCH = 5;
    public static int LENGTH_ROUTER = 3;
    public static int LENGTH_PC = 1;
}