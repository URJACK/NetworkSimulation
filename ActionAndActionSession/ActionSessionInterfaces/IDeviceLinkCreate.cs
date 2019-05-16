using UnityEngine;
using System.Collections;

public interface IDeviceLinkCreate
{
    GameObject GetLinkDeviceA();
    void SetLinkDeviceA(GameObject gameObject);
    GameObject GetLinkDeviceB();
    void SetLinkDeviceB(GameObject gameObject);
}