using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDevicePanelManager : MonoBehaviour
{
    public static Dictionary<int, Image> imageMap = new Dictionary<int, Image>();
    public static int controlIndex = 0;
    public static void Select(int index, Image image)
    {
        foreach (KeyValuePair<int, Image> kv in imageMap)
        {
            kv.Value.color = Color.white;
        }
        if (index != 0)
        {
            if (!imageMap.ContainsKey(index))
            {
                imageMap.Add(index, image);
            }
        }
        if (image != null)
        {
            image.color = Color.red;
        }
        controlIndex = index;
        ActionSessionRecorder.GetInstance().ClearSession();
    }
    
    public static int POINT = 1;
    public static int PC = 2;
    public static int ROUTER = 3;
    public static int SWITCH = 4;
    public static int LINK = 5;
}