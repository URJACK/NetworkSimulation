using UnityEngine;
using System.Collections;

public class DeviceInfomationPopAction : Action
{
    public DeviceInfomationPopAction(IDeviceColorChange session)
    {
        this.actionsession = session;
    }
    private IDeviceColorChange actionsession;

    public override void Exec()
    {
        Debug.Log(".....弹框 Boom!");
        PanelStack panelStack = PanelStack.GetInstance();
        panelStack.SetInfoOfDeviceInfoPanel(DeviceManager.GetDeviceInfo(actionsession.GetColorDevice().GetHashCode()));
        panelStack.Push(panelStack.GetDeviceInfoPanel());
    }
}
