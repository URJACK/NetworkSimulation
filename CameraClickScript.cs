﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClickScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //摄像机需要设置MainCamera的Tag这里才能找到
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObj = hitInfo.collider.gameObject;
                Vector3 hitPoint = hitInfo.point;
                if (gameObj.name == "Ground")
                {
                    int index = ControlDevicePanelManager.controlIndex;
                    //在这里创建一个物件
                    if (index == ControlDevicePanelManager.PC || index == ControlDevicePanelManager.ROUTER || index == ControlDevicePanelManager.SWITCH)
                    {
                        GameObject prefab;
                        if (index == ControlDevicePanelManager.PC)
                        {
                            //在这里微调碰撞到的位置
                            hitPoint.y += 1.5f;
                            prefab = (GameObject)Resources.Load("Prefabs/DevicePC");
                        }
                        else if (index == ControlDevicePanelManager.ROUTER)
                        {
                            hitPoint.y += 0.6f;
                            prefab = (GameObject)Resources.Load("Prefabs/DeviceRouter");
                        }
                        else if (index == ControlDevicePanelManager.SWITCH)
                        {
                            hitPoint.y += 0.6f;
                            prefab = (GameObject)Resources.Load("Prefabs/DeviceSwitch");
                        }
                        else
                        {
                            return;
                        }
                        DeviceManager.GroundClickPoint = hitPoint;
                        //hitInfo.transform.gameObject.SendMessage("OnMouseDown");
                        GameObject newObject = Instantiate(prefab, DeviceManager.GroundClickPoint, Quaternion.Euler(0, 0, 0)) as GameObject;
                        if (index == ControlDevicePanelManager.PC)
                        {
                            DeviceManager.CreateDevice(newObject.GetHashCode(), new DeviceInfo("DefaultPC", DeviceInfo.LAYER_PC));
                        }
                        else if(index == ControlDevicePanelManager.ROUTER)
                        {
                            DeviceManager.CreateDevice(newObject.GetHashCode(), new DeviceInfo("DefaultRouter", DeviceInfo.LAYER_ROUTER));
                        }
                        else if (index == ControlDevicePanelManager.SWITCH)
                        {
                            DeviceManager.CreateDevice(newObject.GetHashCode(), new DeviceInfo("DefaultSwitch", DeviceInfo.LAYER_SWITCH));
                        }
                        ControlDevicePanelManager.Select(0, null);
                    }
                }
            }
        }
    }
}