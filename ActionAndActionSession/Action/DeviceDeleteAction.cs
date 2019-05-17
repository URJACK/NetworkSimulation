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
            MyLogger.GetInstance().Log("设备删除成功");
        }
        else
        {
            MyLogger.GetInstance().Log("设备删除失败");
        }
    }
}
