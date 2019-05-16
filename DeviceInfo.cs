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

    private int portLength = 0;
    /// <summary>
    /// 当前类型设备的最大端口数量
    /// </summary>
    private int maxPortLength;
    /// <summary>
    /// 用来记录相邻连线的GameObject实体
    /// </summary>
    private GameObject[] ports = new GameObject[5];

    public string Name { get => name; set => name = value; }
    public int Layer { get => layer; set => layer = value; }

    public DeviceInfo(string name, int layer)
    {
        this.name = name;
        this.layer = layer;
        if (layer == LAYER_PC)
        {
            maxPortLength = LENGTH_PC;
        }
        else if (layer == LAYER_ROUTER)
        {
            maxPortLength = LENGTH_ROUTER;
        }
        else if (layer == LAYER_SWITCH)
        {
            maxPortLength = LENGTH_SWITCH;
        }
        else
        {
            maxPortLength = 0;
        }
    }
    /// <summary>
    /// 检查自己端口数量是否足够
    /// </summary>
    /// <returns>如果端口数量足够返回true</returns>
    public bool IsPortsEnough()
    {
        if(portLength < maxPortLength)
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
        for(int i = 0; i < LENGTH_MAXPORTS; ++i)
        {
            if(ports[i] == null)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 将当前设备加入连线
    /// </summary>
    /// <param name="line">被加入的连线对象</param>
    public void Link(GameObject line)
    {
        for(int i = 0; i < LENGTH_MAXPORTS; ++i)
        {
            if(ports[i] == null)
            {
                ports[i] = line;
                ++portLength;
                return;
            }
        }
    }
    /// <summary>
    /// 设备中删除这条连线
    /// </summary>
    /// <param name="line">被删除的连线对象实体</param>
    /// <returns>删除是否成功</returns>
    public bool UnLink(GameObject line)
    {
        for (int i = 0; i < LENGTH_MAXPORTS; ++i)
        {
            if (ports[i] == line)
            {
                ports[i] = null;
                --portLength;
                return true;
            }
        }
        return false;
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
        for(int i = 0; i < LENGTH_MAXPORTS; ++i)
        {
            if(ports[i] != null)
            {
                str += i;
                str += ";";
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
    /// <summary>
    /// 每一个设备都必须配备这么多隐藏的端口，不管最后是否会用上
    /// </summary>
    public static int LENGTH_MAXPORTS = LENGTH_SWITCH;
}