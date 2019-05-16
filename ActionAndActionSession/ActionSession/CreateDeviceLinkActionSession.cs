using UnityEngine;
using System.Collections;

public class CreateDeviceLinkActionSession : ActionSession, IDeviceColorChange, IDeviceLinkCreate
{
    private static CreateDeviceLinkActionSession instance;

    private GameObject deviceA;
    private GameObject deviceB;

    public static ActionSession GetInstance()
    {
        if (instance == null)
        {
            instance = new CreateDeviceLinkActionSession();
        }
        return instance;
    }
    private CreateDeviceLinkActionSession() : base()
    {
        SetDicrection(MoveClickDeviceB, StatusASelected, StatusNull);

        SetActionCode(MoveClickDeviceB, StatusASelected, ActionDeviceColorUnselected);
        SetActionCode(MoveClickDeviceB, StatusASelected, ActionLinked);
        SetActionCode(MoveBreak, StatusASelected, ActionDeviceColorUnselected);

        SetAction(ActionDeviceColorSelected, new DeviceColorSelectAction(this));
        SetAction(ActionDeviceColorUnselected, new DeviceColorUnselectAction(this));
        SetAction(ActionLinked, new DeviceLinkCreateAction(this));
    }
    

    public void Init(GameObject gameObject)
    {
        deviceA = gameObject;
        //第一台设备被赋值后，第二台设备会被初始化
        deviceB = null;
        StatusCode = StatusASelected;
        ExecAction(ActionDeviceColorSelected);
    }

    public GameObject GetColorDevice()
    {
        return deviceA;
    }

    public void SetColorDevice(GameObject gameObject)
    {
        deviceA = gameObject;
    }

    public GameObject GetLinkDeviceA()
    {
        return deviceA;
    }

    public void SetLinkDeviceA(GameObject gameObject)
    {
        deviceA = gameObject;
    }

    public GameObject GetLinkDeviceB()
    {
        return deviceB;
    }

    public void SetLinkDeviceB(GameObject gameObject)
    {
        deviceB = gameObject;
    }

    public static int StatusASelected = 1;

    public static int MoveClickDeviceB = 1101;
    
    public static int ActionDeviceColorSelected = 1;
    public static int ActionDeviceColorUnselected = 2;
    public static int ActionLinked = 3;
}