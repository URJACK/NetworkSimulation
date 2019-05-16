using UnityEngine;
using System.Collections;

public class DeviceInfomationPopAction : Action
{
    public DeviceInfomationPopAction(ChooseDeviceActionSession chooseDeviceActionSession)
    {
        this.chooseDeviceActionSession = chooseDeviceActionSession;
    }
    private ChooseDeviceActionSession chooseDeviceActionSession;

    public override void Exec()
    {
        Debug.Log(".....弹框 Boom!");
    }
}
