using UnityEngine;

using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 设备管理器
/// 能够控制在世界中的所有的设备
/// </summary>
public class DeviceManager
{
    /// <summary>
    /// 地面点击坐标记录
    /// </summary>
    public static Vector3 GroundClickPoint = new Vector3(0, 2, 0);
    /// <summary>
    /// 连线的额外距离
    /// </summary>
    private readonly static float constantLinkSpan = 1.8f;
    /// <summary>
    /// 设备记录表
    /// 使用一个设备Gameobject 的 hashcode 作为 key
    /// </summary>
    public static Dictionary<int, DeviceInfo> deviceTable = new Dictionary<int, DeviceInfo>();
    /// <summary>
    /// 连续记录表
    /// 使用一个连线Gameobject 的 hashcode 作为 key
    /// </summary>
    public static Dictionary<int, LineInfo> lineTable = new Dictionary<int, LineInfo>();
    /// <summary>
    /// 创建设备的同时，将设备的信息注册到设备管理器中
    /// </summary>
    /// <param name="hashcode">新创建设备Gameobject的hash值</param>
    /// <param name="deviceInfo">新创建设备的设备信息的引用</param>
    public static void CreateDevice(int hashcode, DeviceInfo deviceInfo)
    {
        deviceTable.Add(hashcode, deviceInfo);
    }
    /// <summary>
    /// 删除设备的同时，将设备的信息从设备管理器中删除
    /// </summary>
    /// <param name="hashcode">被删除的设备GameObject的hashcode</param>
    /// <param name="gameObject">被删除的设备gameobject</param>
    public static bool DeleteDevice(int hashcode)
    {
        //设备表信息的删除
        return deviceTable.Remove(hashcode);
    }
    /// <summary>
    /// 取得设备的信息
    /// </summary>
    /// <param name="hashcode">设备对象的hashcode</param>
    /// <returns></returns>
    public static DeviceInfo GetDeviceInfo(int hashcode)
    {
        if (deviceTable.ContainsKey(hashcode))
        {
            return deviceTable[hashcode];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 检查两个设备端口是否足够来建立一条新连线
    /// </summary>
    /// <param name="hashcodeA"></param>
    /// <param name="hashcodeB"></param>
    /// <returns></returns>
    public static bool ArePortsEnough(int hashcodeA, int hashcodeB)
    {
        DeviceInfo infoA = deviceTable[hashcodeA];
        DeviceInfo infoB = deviceTable[hashcodeB];
        return infoA.IsPortsEnough() && infoB.IsPortsEnough();
    }
    /// <summary>
    /// 创建两台设备之间的连线.
    /// 我们需要在连线表将外部传入的“对象的hash值”与我们设备管理器新创建的“连线信息”进行绑定
    /// </summary>
    /// <param name="hashcodeA">设备a 的hash值</param>
    /// <param name="hashcodeB">设备b 的hash值</param>
    /// <param name="linkObject">生成的连线对象</param>
    /// <returns>创建的成功或者失败</returns>
    public static bool CreateLink(int hashcodeA, int hashcodeB, GameObject linkObject)
    {
        DeviceInfo ainfo = deviceTable[hashcodeA];
        DeviceInfo binfo = deviceTable[hashcodeB];
        if (ainfo.IsLinkable(linkObject) && binfo.IsLinkable(linkObject))
        {
            LineInfo lineInfo = new LineInfo(ainfo, binfo);
            ainfo.Link(linkObject);
            binfo.Link(linkObject);
            lineTable.Add(linkObject.GetHashCode(), lineInfo);
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 取得连线的信息
    /// </summary>
    /// <returns></returns>
    public static LineInfo GetLineInfo(int hashCode)
    {
        if (lineTable.ContainsKey(hashCode))
        {
            return lineTable[hashCode];
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 外部传入这个连线对象
    /// </summary>
    public static bool DeleteLink(GameObject linkObject)
    {
        LineInfo lineInfo = lineTable[linkObject.GetHashCode()];
        DeviceInfo ainfo = lineInfo.GetAInfo();
        DeviceInfo binfo = lineInfo.GetBInfo();
        if (ainfo.UnLink(linkObject) && binfo.UnLink(linkObject))
        {
            Debug.Log("删除连线成功");
            return true;
        }
        else
        {
            Debug.Log("删除连线失败");
            return false;
        }
    }
    /// <summary>
    /// 取得旋转角度
    /// </summary>
    /// <param name="posA">A的坐标</param>
    /// <param name="posB">B的坐标</param>
    /// <returns></returns>
    public static Vector3 GetLinkLineRotateAngle(Vector3 posA, Vector3 posB)
    {
        Vector3 buffer = posB - posA;
        float tanValue = buffer.z / buffer.x;
        float tanRadius = Mathf.Atan(tanValue);
        float angle = tanRadius * 180 / Mathf.PI;
        return new Vector3(0, -angle, 0);
    }
    /// <summary>
    /// 取得两个交点的中间点
    /// </summary>
    /// <param name="posA">A的坐标</param>
    /// <param name="posB">B的坐标</param>
    /// <returns></returns>
    public static Vector3 GetLinkLinePos(Vector3 posA, Vector3 posB)
    {
        return (posA + posB) / 2;
    }
    /// <summary>
    /// 取得两点之间连线的缩放效果
    /// </summary>
    /// <param name="posA">A的坐标</param>
    /// <param name="posB">B的坐标</param>
    /// <returns></returns>
    public static Vector3 GetLinkLineScale(Vector3 posA, Vector3 posB)
    {
        float x = Vector3.Distance(posA, posB) - constantLinkSpan;
        if (x > 0)
        {
            return new Vector3(x, 0, 0);
        }
        else
        {
            return new Vector3(1, 0, 0);
        }
    }
}