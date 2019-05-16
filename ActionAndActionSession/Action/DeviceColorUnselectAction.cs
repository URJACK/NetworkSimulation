using UnityEngine;
using System.Collections;

public class DeviceColorUnselectAction : Action
{
    private IDeviceColorChange actionsession;
    public DeviceColorUnselectAction(IDeviceColorChange session)
    {
        this.actionsession = session;
    }
    public override void Exec()
    {
        foreach (Transform child in actionsession.GetColorDevice().transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = MaterialRecorder.materials[MaterialRecorder.DEVICENORMAL];
        }
    }
}