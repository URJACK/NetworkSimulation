using UnityEngine;
using System.Collections;

public class DeviceDeleteAction : Action
{
    IDeviceColorChange actionsession;
    public DeviceDeleteAction(IDeviceColorChange session)
    {
        actionsession = session;
    }

    public override void Exec()
    {
        if (DeviceManager.DeleteDevice(actionsession.GetColorDevice().GetHashCode()))
        {
            GameObject.Destroy(actionsession.GetColorDevice());
            Debug.Log("删除成功");
        }
        else
        {
            Debug.Log("删除失败");
        }
    }
}
