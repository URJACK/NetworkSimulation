﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDeviceButton : MonoBehaviour
{
    public Image image;
    public void Select(int index)
    {
        ControlDevicePanelManager.Select(index, image);
    }
}
