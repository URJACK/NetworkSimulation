using UnityEngine;
using System.Collections;

public class LineInfo
{
    private DeviceInfo infoA;
    private DeviceInfo infoB;
    public LineInfo(DeviceInfo a, DeviceInfo b)
    {
        infoA = a;
        infoB = b;
    }
    public DeviceInfo GetAInfo()
    {
        return infoA;
    }
    public DeviceInfo GetBInfo()
    {
        return infoB;
    }
}
