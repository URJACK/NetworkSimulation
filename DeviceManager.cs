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
    private readonly static float constantLinkSpan = 1;

    /// <summary>
    /// 设备记录表
    /// 使用每一个对象Gameobject对应的hashcode作为key
    /// </summary>
    public static Dictionary<int, DeviceInfo> deviceTable = new Dictionary<int, DeviceInfo>();
    /// <summary>
    /// 设备链接表
    /// 存储链接关系 双向存储。若“结点2”与“结点3”链接，那么
    /// deviceLinkTable[2].Contains(3) == true
    /// deviceLinkTable[3].Contains(2) == true
    /// key：节点A设备的hashcode
    /// value：与节点A相连的结点的hashcode的<list>
    /// </summary>
    public static Dictionary<int, List<int>> deviceLinkTable = new Dictionary<int, List<int>>();
    /// <summary>
    /// 连线对象记录表 存储连线对象本身
    /// 这里我们商定链接表的结构是单向存储。即，如果是有“结点2”与“结点3”链接，那么我们会为它们进行连线，连线的结果在链接表中是单向的，
    /// 即有可能是linkTable[2][3] 也有可能是 linkTable[3][2]。
    /// 链接表：之所以这里要存储一个链接表的原因是因为，我们在删除设备的时候，需要删除该设备周围的连线实例
    /// key：节点A设备的hashcode
    /// key2：节点B设备的hashcode
    /// value：连线对应的Gameobject
    /// </summary>
    public static Dictionary<int, Dictionary<int, GameObject>> linkTable = new Dictionary<int, Dictionary<int, GameObject>>();
    /// <summary>
    /// 创建设备的同时，将设备的信息注册到设备管理器中
    /// </summary>
    /// <param name="hashcode">新创建设备Gameobject的hash值</param>
    /// <param name="deviceInfo">新创建设备的设备信息的引用</param>
    public static void CreateDevice(int hashcode, DeviceInfo deviceInfo)
    {
        deviceTable.Add(hashcode, deviceInfo);
        Debug.Log(deviceInfo);
    }
    /// <summary>
    /// 删除设备的同时，将设备的信息从设备管理器中删除
    /// 同时准备查询设备相关的连线信息
    /// </summary>
    /// <param name="hashcode">被删除的设备GameObject的hashcode</param>
    /// <param name="gameObject">被删除的设备gameobject</param>
    public static void DeleteDevice(int hashcode, GameObject gameObject)
    {
        //设备表信息的删除
        deviceTable.Remove(hashcode);
        GameObject.Destroy(gameObject);
        if (deviceLinkTable.ContainsKey(hashcode))
        {
            //获取到自己周围结点的列表
            List<int> list = deviceLinkTable[hashcode];
            foreach (int neiborHashcode in list)
            {
                //针对双向存储的链表进行交叉删除
                if (linkTable.ContainsKey(hashcode) && linkTable[hashcode].ContainsKey(neiborHashcode))
                {
                    GameObject.Destroy(linkTable[hashcode][neiborHashcode]);
                    linkTable[hashcode].Remove(neiborHashcode);
                }
                else if (linkTable.ContainsKey(neiborHashcode) && linkTable[neiborHashcode].ContainsKey(hashcode))
                {
                    GameObject.Destroy(linkTable[neiborHashcode][hashcode]);
                    linkTable[neiborHashcode].Remove(hashcode);
                }
            }

            //删除自己作为结点列表
            deviceLinkTable.Remove(hashcode);
            //如果包含了该结点，我们会尝试删除该结点
            foreach (KeyValuePair<int, List<int>> kvp in deviceLinkTable)
            {
                //删除自己作为邻接结点列表中的内容
                kvp.Value.Remove(hashcode);
            }
        }
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
    /// 创建连线
    /// </summary>
    public static void CreateLink(int hashcodeA, int hashcodeB, GameObject linkObject)
    {
        //连线对象记录表的维护
        if (!linkTable.ContainsKey(hashcodeA))
        {
            linkTable.Add(hashcodeA, new Dictionary<int, GameObject>());
        }
        linkTable[hashcodeA].Add(hashcodeB, linkObject);
        //设备链接表的维护
        if (!deviceLinkTable.ContainsKey(hashcodeA))
        {
            deviceLinkTable.Add(hashcodeA, new List<int>());
        }
        deviceLinkTable[hashcodeA].Add(hashcodeB);
        if (!deviceLinkTable.ContainsKey(hashcodeB))
        {
            deviceLinkTable.Add(hashcodeB, new List<int>());
        }
        deviceLinkTable[hashcodeB].Add(hashcodeA);
    }
    /// <summary>
    /// 删除链接
    /// </summary>
    /// <param name="hashcodeA">结点A的hash值</param>
    /// <param name="hashcodeB">结点B的hash值</param>
    public static void DeleteLink(int hashcodeA, int hashcodeB)
    {
        //对设备链接表的内容进行修改
        if (deviceLinkTable.ContainsKey(hashcodeA))
        {
            deviceLinkTable[hashcodeA].Remove(hashcodeB);
        }
        if (deviceLinkTable.ContainsKey(hashcodeB))
        {
            deviceLinkTable[hashcodeB].Remove(hashcodeA);
        }
        //对链接对象记录表进行修改
        if (linkTable.ContainsKey(hashcodeA) && linkTable[hashcodeA].ContainsKey(hashcodeB))
        {
            //消除掉连线对象本身
            GameObject.Destroy(linkTable[hashcodeA][hashcodeB]);
            linkTable[hashcodeA].Remove(hashcodeB);
        }
        else if (linkTable.ContainsKey(hashcodeB) && linkTable[hashcodeB].ContainsKey(hashcodeA))
        {
            //消除掉连线对象本身
            GameObject.Destroy(linkTable[hashcodeB][hashcodeA]);
            linkTable[hashcodeB].Remove(hashcodeA);
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
    public static Vector3 GetLinkLineScale(Vector3 posA,Vector3 posB)
    {
        float x = Vector3.Distance(posA, posB) - constantLinkSpan;
        if (x > 0) {
            return new Vector3(x, 0, 0);
        }
        else
        {
            return new Vector3(1, 0, 0);
        }
    }
}