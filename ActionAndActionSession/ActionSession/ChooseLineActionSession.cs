using UnityEngine;
using System.Collections;

public class ChooseLineActionSession : ActionSession, IDeviceColorChange
{
    public GameObject lineObject;

    private static ChooseLineActionSession instance;
    public static ChooseLineActionSession GetInstance()
    {
        if (instance == null)
        {
            instance = new ChooseLineActionSession();
        }
        return instance;
    }
    private ChooseLineActionSession()
    {
        SetDicrection(MoveClickDelete, StatusSelected, StatusNull);
        SetDicrection(MoveClickESC, StatusSelected, StatusNull);

        SetActionCode(MoveClickESC, StatusSelected, ActionDeviceColorUnselect);
        SetActionCode(MoveClickDelete, StatusSelected, ActionDeviceColorUnselect);
        SetActionCode(MoveClickDelete, StatusSelected, ActionDeleteLine);
        SetActionCode(MoveBreak, StatusSelected, ActionDeviceColorUnselect);

        SetAction(ActionDeviceColorSelect, new DeviceColorSelectAction(this));
        SetAction(ActionDeviceColorUnselect, new DeviceColorUnselectAction(this));
        SetAction(ActionDeleteLine, new DeleteLineAction(this));
    }
    public void Init(GameObject line)
    {
        lineObject = line;
        StatusCode = StatusSelected;
        ExecAction(ActionDeviceColorSelect);
    }
    public GameObject GetColorDevice()
    {
        return lineObject;
    }
    public void SetColorDevice(GameObject gameObject)
    {
        lineObject = gameObject;
    }

    public static int StatusSelected = 1;

    public static int MoveClickESC = 3001;
    public static int MoveClickDelete = 3002;

    public static int ActionDeviceColorSelect = 1;
    public static int ActionDeviceColorUnselect = 2;
    public static int ActionDeleteLine = 3;
}