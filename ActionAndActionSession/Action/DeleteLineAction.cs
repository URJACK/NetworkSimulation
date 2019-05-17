using UnityEngine;
using System.Collections;

public class DeleteLineAction : Action
{
    IDeviceColorChange actionsession;
    public DeleteLineAction(IDeviceColorChange session)
    {
        actionsession = session;
    }
    public override void Exec()
    {
        GameObject lineObject = actionsession.GetColorDevice();
        if (DeviceManager.DeleteLink(lineObject))
        {
            GameObject.Destroy(lineObject);
            MyLogger.GetInstance().Log("成功删除连线");
        }
        else
        {
            MyLogger.GetInstance().Log("删除连线失败");
        }
    }
}
