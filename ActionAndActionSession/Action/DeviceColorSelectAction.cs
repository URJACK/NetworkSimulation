using UnityEngine;
using System.Collections;

public class DeviceColorSelectAction : Action
{
    private IDeviceColorChange actionsession;
    public DeviceColorSelectAction(IDeviceColorChange session)
    {
        this.actionsession = session;
    }
    public override void Exec()
    {
        foreach (Transform child in actionsession.GetColorDevice().transform)
        {
            child.gameObject.GetComponent<MeshRenderer>().material = MaterialRecorder.materials[MaterialRecorder.DEVICESELECTED];
        }
    }
}