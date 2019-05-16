using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceClick : MonoBehaviour
{
    public GameObject device;
    void OnMouseDown()
    {
        ActionSessionRecorder recorder = ActionSessionRecorder.GetInstance();
        if (ControlDevicePanelManager.controlIndex == ControlDevicePanelManager.POINT)
        {
            recorder.RecordSession(ChooseDeviceActionSession.GetInstance());
            ChooseDeviceActionSession cdas = (ChooseDeviceActionSession)recorder.GetSession();
            cdas.Init(device);
        }
        else if (ControlDevicePanelManager.controlIndex == ControlDevicePanelManager.LINK)
        {
            ActionSession actionSession = recorder.GetSession();
            if (actionSession != null && actionSession.GetType().ToString() == "CreateDeviceLinkActionSession") {
                CreateDeviceLinkActionSession cdlas = actionSession as CreateDeviceLinkActionSession;
                if(cdlas.GetLinkDeviceA() != device)
                {
                    Debug.Log("OK");
                    cdlas.SetLinkDeviceB(device);
                    Debug.Log(cdlas.GetStatusCode());
                    cdlas.ChangeStatus(CreateDeviceLinkActionSession.MoveClickDeviceB);
                }
                else
                {
                    Debug.Log("NO");
                    recorder.ClearSession();
                }
            }
            else
            {
                recorder.RecordSession(CreateDeviceLinkActionSession.GetInstance());
                CreateDeviceLinkActionSession cdlas = recorder.GetSession() as CreateDeviceLinkActionSession;
                cdlas.Init(device);
            }
        }
    }
}
