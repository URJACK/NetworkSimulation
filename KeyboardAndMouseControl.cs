using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAndMouseControl : MonoBehaviour
{
    private bool ready = true;
    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Delete) && ready)
        {
            ready = false;
            ActionSession session = ActionSessionRecorder.GetInstance().GetSession();
            if (session != null && session.GetType().ToString() == "ChooseDeviceActionSession")
            {
                ChooseDeviceActionSession actionsession = session as ChooseDeviceActionSession;
                actionsession.ChangeStatus(ChooseDeviceActionSession.MoveClickDelete);
            }
        }
        else if (Input.GetKey(KeyCode.Escape) && ready)
        {
            ready = false;
            ActionSession session = ActionSessionRecorder.GetInstance().GetSession();
            if (session != null && session.GetType().ToString() == "ChooseDeviceActionSession")
            {
                ChooseDeviceActionSession actionsession = session as ChooseDeviceActionSession;
                actionsession.ChangeStatus(ChooseDeviceActionSession.MoveClickESC);
            }
        }
        else if (Event.current.type == EventType.MouseDown)
        {
            ready = true;
        }
    }
}
