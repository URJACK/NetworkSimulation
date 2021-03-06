﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceLinkCreateAction : Action
{
    IDeviceLinkCreate actionsession;
    public DeviceLinkCreateAction(IDeviceLinkCreate session)
    {
        actionsession = session;
    }
    public override void Exec()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/LinkUnit");
        GameObject gameObjectA, gameObjectB;
        gameObjectA = actionsession.GetLinkDeviceA();
        gameObjectB = actionsession.GetLinkDeviceB();
        if (DeviceManager.ArePortsEnough(gameObjectA.GetHashCode(), gameObjectB.GetHashCode()))
        {
            MyLogger.GetInstance().Log("成功创建连线");
            Vector3 pos = DeviceManager.GetLinkLinePos(gameObjectA.transform.position, gameObjectB.transform.position);
            Vector3 angle = DeviceManager.GetLinkLineRotateAngle(gameObjectA.transform.position, gameObjectB.transform.position);
            Vector3 scale = DeviceManager.GetLinkLineScale(gameObjectA.transform.position, gameObjectB.transform.position);
            GameObject newObject = GameObject.Instantiate(prefab, pos, Quaternion.Euler(angle)) as GameObject;
            newObject.transform.localScale += scale;
            DeviceManager.CreateLink(actionsession.GetLinkDeviceA().GetHashCode(), actionsession.GetLinkDeviceB().GetHashCode(), newObject);
            ControlDevicePanelManager.Select(0, null);
        }
        else
        {
            MyLogger.GetInstance().Log("设备端口数量不足");
            ControlDevicePanelManager.Select(0, null);
        }
    }
}