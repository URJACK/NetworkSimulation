using UnityEngine;
using System.Collections;

/// <summary>
/// 设备信息
/// </summary>
public class DeviceInfo
{
    private string name;
    private string address;
    private int layer;

    public string Name { get => name; set => name = value; }
    public string Address { get => address; set => address = value; }
    public int Layer { get => layer; set => layer = value; }

    public static int LAYER_MAC = 1;
    public static int LAYER_NET = 2;
    public static int LAYER_TERM = 3;
}
