using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 界面栈，在界面栈中，我们在这里需要存入界面的调入情况，任何一个界面都能通过ESC按键进行重置
/// 界面的退出优先级要高于动作会话的优先级
/// </summary>
public class PanelStack : MonoBehaviour
{
    private static PanelStack instance;
    public static PanelStack GetInstance()
    {
        if (instance == null)
        {
            instance = new PanelStack();
        }
        return instance;
    }
    private PanelStack()
    {
        Debug.Log("生成界面调用栈");
    }
    private void Start()
    {
        instance = this;
        Debug.Log("界面调用栈初始化创建");
    }

    public GameObject deviceInfoPanel;
    public GameObject deviceInfoNameTextField;
    public Text deviceInfoTypeTextArea;
    public Text deviceInfoPortTextArea;
    private DeviceInfo deviceInfo;

    public GameObject compileInfoPanel;
    public GameObject compileInfoInputField;
    public Text compileInfoPorTextArea;
    private int compileInfoShowPortIndex = 0;

    /// <summary>
    /// 存储的界面栈内容
    /// </summary>
    private Stack<GameObject> panels = new Stack<GameObject>();

    /// <summary>
    /// 取得设备信息界面
    /// </summary>
    /// <returns></returns>
    public GameObject GetDeviceInfoPanel()
    {
        return deviceInfoPanel;
    }
    /// <summary>
    /// 获取到编译信息界面
    /// </summary>
    /// <returns></returns>
    public GameObject GetCompileInfoPanel()
    {
        return compileInfoPanel;
    }
    /// <summary>
    /// 设备信息界面的内容设置
    /// </summary>
    /// <param name="info">被设置的内容信息</param>
    public void InitInfoOfDeviceInfoPanel(DeviceInfo info)
    {
        deviceInfo = info;
        if (deviceInfo != null)
        {
            deviceInfoNameTextField.GetComponent<InputField>().text = deviceInfo.Name;
            deviceInfoTypeTextArea.text = deviceInfo.GetLayerString();
            deviceInfoPortTextArea.text = deviceInfo.GetPortsString();
            SetInfoOfCompileInfoPanelByDeviceInfo(compileInfoShowPortIndex);
        }
    }
    /// <summary>
    /// 初始化编译界面的内容设置
    /// </summary>
    /// <param name="index"></param>
    public void SetInfoOfCompileInfoPanelByDeviceInfo(int index)
    {
        if(deviceInfo != null)
        {
            compileInfoInputField.GetComponent<InputField>().text = deviceInfo.GetCode(index);
            compileInfoPorTextArea.text = "当前端口：" + compileInfoShowPortIndex;
        }
    }
    /// <summary>
    /// 保存设备的界面的内容信息
    /// </summary>
    public void SaveInfoOfDeviceInfoPanel()
    {
        if (deviceInfo != null)
        {
            deviceInfo.Name = deviceInfoNameTextField.GetComponent<InputField>().text;
        }
    }
    /// <summary>
    /// 往界面栈中存入一个界面对象，如果栈顶元素相同，则不允许存入
    /// </summary>
    /// <param name="gameObject">被存入的界面对象</param>
    public void Push(GameObject gameObject)
    {
        if (panels.Count == 0 || panels.Peek() != gameObject)
        {
            gameObject.SetActive(true);
            panels.Push(gameObject);
        }
    }
    /// <summary>
    /// 消除一个界面栈栈顶的元素
    /// </summary>
    /// <param name="gameObject"></param>
    public void Pop(GameObject gameObject)
    {
        if (panels.Count != 0)
        {
            if(gameObject == null || panels.Peek() == gameObject)
            {
                GameObject go = panels.Pop();
                go.SetActive(false);
            }
        }
    }

    public static string GameNameDeviceInfoPanel = "DeviceInfoPanel";
}