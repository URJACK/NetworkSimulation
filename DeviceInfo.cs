using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
    /// 当前记录的端口长度
    /// </summary>
    private int portLength = 0;
    /// <summary>
    /// 当前类型设备的最大端口数量
    /// </summary>
    private int maxPortLength;
    /// <summary>
    /// 用来记录相邻连线的GameObject实体
    /// </summary>
    private GameObject[] ports = new GameObject[LENGTH_MAXPORTS];
    /// <summary>
    /// 生成对应的设备代码区
    /// </summary>
    private string[] portCodes = new string[LENGTH_MAXPORTS];
    private string compiledInfo;

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
        for(int i = 0; i < maxPortLength; ++i)
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
    /// <summary>
    /// 获取对该接口编译后得到的信息
    /// </summary>
    /// <returns></returns>
    public string GetCompiledInfo()
    {
        return compiledInfo;
    }
    /// <summary>
    /// 设置通过设备信息的代码得到的编译结果信息
    /// </summary>
    /// <returns></returns>
    public void SetCompiledInfo(string info)
    {
        compiledInfo = info;
    }

    /// <summary>
    /// 设置代码
    /// </summary>
    /// <param name="code">在这里存储代码</param>
    /// <returns>必须在确保端口的情况下，设置代码才会有效</returns>
    public bool SetCode(int index, string code)
    {
        if (index >= 0 && index < maxPortLength && ports[index] != null)
        {
            portCodes[index] = code;
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 开始针对所录入的端口进行答辩
    /// 这里不需要对index进行检测，因为我们在调用这个方法的时候，我们已经完成了对代码的检测
    /// 最终会把编译的信息存入该设备的compileInfo变量中
    /// </summary>
    /// <param name="index">被检测的端口</param>
    public void StartCompile(int index)
    {
        Compiler.GetInstance().Compile(portCodes[index], this);
    }

    /// <summary>
    /// 取得代码
    /// </summary>
    /// <param name="index"></param>
    /// <returns>如果index对应端口未连接，返回null</returns>
    public string GetCode(int index)
    {
        if (index >= 0 && index < maxPortLength && ports[index] != null)
        {
            return portCodes[index];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 取得层级代表的字符串
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// 取得端口显示字符串
    /// </summary>
    /// <returns></returns>
    public string GetPortsString()
    {
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