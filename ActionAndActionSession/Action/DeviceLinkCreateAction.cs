using System.Collections;
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
        Debug.Log("连线");
        GameObject prefab = (GameObject)Resources.Load("Prefabs/LinkUnit");
        GameObject gameObjectA, gameObjectB;
        gameObjectA = actionsession.GetLinkDeviceA();
        gameObjectB = actionsession.GetLinkDeviceB();
        Vector3 pos = DeviceManager.GetLinkLinePos(gameObjectA.transform.position, gameObjectB.transform.position);
        Vector3 angle = DeviceManager.GetLinkLineRotateAngle(gameObjectA.transform.position, gameObjectB.transform.position);
        Vector3 scale = DeviceManager.GetLinkLineScale(gameObjectA.transform.position, gameObjectB.transform.position);
        GameObject newObject = GameObject.Instantiate(prefab, pos, Quaternion.Euler(angle)) as GameObject;
        newObject.transform.localScale += scale;
        DeviceManager.CreateLink(actionsession.GetLinkDeviceA().GetHashCode(), actionsession.GetLinkDeviceB().GetHashCode(), newObject);
        ControlDevicePanelManager.Select(0, null);
    }
}