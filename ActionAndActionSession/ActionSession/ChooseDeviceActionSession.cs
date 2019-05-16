using UnityEngine;
using System.Collections;

public class ChooseDeviceActionSession : ActionSession, IDeviceColorChange
{
    /// <summary>
    /// 外部传入用来修改设备的变量
    /// </summary>
    public GameObject deviceGameObject;

    private static ChooseDeviceActionSession instance;
    public static ChooseDeviceActionSession GetInstance()
    {
        if (instance == null)
        {
            instance = new ChooseDeviceActionSession();
        }
        return instance;
    }
    private ChooseDeviceActionSession() : base()
    {
        SetDicrection(MoveClickESC, StatusSelected, StatusNull);
        SetDicrection(MoveClickDelete, StatusSelected, StatusNull);

        SetActionCode(MoveClickESC, StatusSelected, ActionDeviceColorUnselect);
        SetActionCode(MoveBreak, StatusSelected, ActionDeviceColorUnselect);
        SetActionCode(MoveClickESC, StatusSelected, ActionDeviceInfoBack);
        SetActionCode(MoveBreak, StatusSelected, ActionDeviceInfoBack);
        SetActionCode(MoveClickDelete, StatusSelected, ActionDeviceColorUnselect);
        SetActionCode(MoveClickDelete, StatusSelected, ActionDeviceInfoBack);
        SetActionCode(MoveClickDelete, StatusSelected, ActionDeviceDelete);

        SetAction(ActionDeviceColorSelect, new DeviceColorSelectAction(this));
        SetAction(ActionDeviceColorUnselect, new DeviceColorUnselectAction(this));
        SetAction(ActionDeviceInfoPop, new DeviceInfomationPopAction(this));
        SetAction(ActionDeviceInfoBack, new DeviceInfomationBackAction(this));
        SetAction(ActionDeviceDelete, new DeviceDeleteAction(this));
    }

    public void Init(GameObject device)
    {
        StatusCode = StatusSelected;
        deviceGameObject = device;
        ExecAction(ActionDeviceColorSelect);
        ExecAction(ActionDeviceInfoPop);
    }

    public void SetColorDevice(GameObject gameObject)
    {
        deviceGameObject = gameObject;
    }

    public GameObject GetColorDevice()
    {
        return deviceGameObject;
    }

    public static int StatusSelected = 1;

    public static int MoveClickESC = 1001;
    public static int MoveClickDelete = 1002;

    public static int ActionDeviceInfoPop = 1;
    public static int ActionDeviceInfoBack = 2;
    public static int ActionDeviceColorSelect = 3;
    public static int ActionDeviceColorUnselect = 4;
    public static int ActionDeviceDelete = 5;
}