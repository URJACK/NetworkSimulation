using UnityEngine;
using System.Collections;

public class DeviceInfomationBackAction : Action
{
    public DeviceInfomationBackAction(ChooseDeviceActionSession chooseDeviceActionSession)
    {
        this.chooseDeviceActionSession = chooseDeviceActionSession;
    }
    private ChooseDeviceActionSession chooseDeviceActionSession;
    
    public override void Exec()
    {
        Debug.Log(".....收框 WOW!");
    }
}
