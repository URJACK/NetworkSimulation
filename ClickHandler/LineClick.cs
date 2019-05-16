using UnityEngine;
using System.Collections;

public class LineClick : MonoBehaviour
{
    public GameObject lineObject;
    void OnMouseDown()
    {
        if(ControlDevicePanelManager.controlIndex == ControlDevicePanelManager.POINT)
        {
            ActionSessionRecorder recorder = ActionSessionRecorder.GetInstance();
            recorder.RecordSession(ChooseLineActionSession.GetInstance());
            ChooseLineActionSession clas = recorder.GetSession() as ChooseLineActionSession;
            clas.Init(lineObject);
        }
    }
}
