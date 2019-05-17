using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardAndMouseControl : MonoBehaviour
{
    private bool ready = true;

    private static string CHOOSEDEVICE_SESSION = "ChooseDeviceActionSession";
    private static string CHOOSELINE_SESSION = "ChooseLineActionSession";
    void Start()
    {
        Compiler.GetInstance();        
    }

    void OnGUI()
    {
        if (Input.GetKey(KeyCode.Delete) && ready)
        {
            ready = false;
            ActionSession session = ActionSessionRecorder.GetInstance().GetSession();
            if(session == null)
            {
                return;
            }
            else if (session.GetType().ToString() == CHOOSEDEVICE_SESSION)
            {
                ChooseDeviceActionSession actionsession = session as ChooseDeviceActionSession;
                actionsession.ChangeStatus(ChooseDeviceActionSession.MoveClickDelete);
            }
            else if(session.GetType().ToString() == CHOOSELINE_SESSION)
            {
                ChooseLineActionSession actionSession = session as ChooseLineActionSession;
                actionSession.ChangeStatus(ChooseLineActionSession.MoveClickDelete);
            }
        }
        else if (Input.GetKey(KeyCode.Escape) && ready)
        {
            ready = false;
            ActionSession session = ActionSessionRecorder.GetInstance().GetSession();
            if(PanelStack.GetInstance().GetStackSize() > 0) {
                PanelStack.GetInstance().Pop(null);
            }
            else if(session == null)
            {
                return;
            }
            else if (session.GetType().ToString() == CHOOSEDEVICE_SESSION)
            {
                ChooseDeviceActionSession actionsession = session as ChooseDeviceActionSession;
                actionsession.ChangeStatus(ChooseDeviceActionSession.MoveClickESC);
            }
            else if(session.GetType().ToString() == CHOOSELINE_SESSION)
            {
                ChooseLineActionSession actionsession = session as ChooseLineActionSession;
                actionsession.ChangeStatus(ChooseLineActionSession.MoveClickESC);
            }
        }
        else if (Event.current.type == EventType.MouseDown)
        {
            ready = true;
        }
    }
}
