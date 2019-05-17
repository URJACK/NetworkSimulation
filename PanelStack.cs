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
    private int compileInfoShowPortIndex = -1;


    public GameObject operationInfoPanel;
    /// <summary>
    /// 监听信息的显示区域
    /// </summary>
    public Text listenInfoTextArea;
    /// <summary>
    /// 接受监听信息的窗口
    /// </summary>
    public GameObject listenInfoTextField;
    /// <summary>
    /// 发送调试信息的窗口
    /// </summary>
    public GameObject sendInfoTextField;
    public GameObject srcAddressField;
    public GameObject desAddressField;
    public GameObject srcPortField;
    public GameObject desPortField;

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
    /// 取得操作界面
    /// </summary>
    /// <returns></returns>
    public GameObject GetOperationInfoPanel()
    {
        return operationInfoPanel;
    }
    /// <summary>
    /// 取得存储的设备信息
    /// </summary>
    /// <returns></returns>
    public DeviceInfo GetDeviceInfo()
    {
        return deviceInfo;
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
        if (deviceInfo != null)
        {
            deviceInfo.SetCode(compileInfoShowPortIndex, compileInfoInputField.GetComponent<InputField>().text);
            compileInfoInputField.GetComponent<InputField>().text = deviceInfo.GetCode(index);
            compileInfoShowPortIndex = index;
            compileInfoPorTextArea.text = "当前端口：" + compileInfoShowPortIndex;
        }
    }
    /// <summary>
    /// 显示通过编译得到的信息
    /// </summary>
    public void SetCompiledInfomationByDeviceInfo()
    {
        if(deviceInfo != null)
        {
            deviceInfo.SetCode(compileInfoShowPortIndex, compileInfoInputField.GetComponent<InputField>().text);
            compileInfoInputField.GetComponent<InputField>().text = deviceInfo.GetCompiledInfo();
            //将当前的下标指针置为无效
            compileInfoShowPortIndex = -1;
            compileInfoPorTextArea.text = "当前端口：" + compileInfoShowPortIndex;
        }
    }
    /// <summary>
    /// 显示操作界面的数据信息
    /// </summary>
    public void SetOperationInfoByDeviceInfo()
    {
        listenInfoTextArea.text = deviceInfo.GetListenStatusString();
        sendInfoTextField.GetComponent<InputField>().text = "";
        srcAddressField.GetComponent<InputField>().text = deviceInfo.GetListenAddress();
        srcPortField.GetComponent<InputField>().text = "" + deviceInfo.GetListenPort();
        listenInfoTextField.GetComponent<InputField>().text = deviceInfo.GetListenMessage();
    }
    /// <summary>
    /// 开始尝试编译
    /// </summary>
    /// <returns>当前端口的代码是否可以编译</returns>
    public bool IsCompileAble()
    {
        if (deviceInfo.GetCode(compileInfoShowPortIndex) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 开始对当前的端口代码进行编译
    /// </summary>
    public void StartPortCompile()
    {
        deviceInfo.StartCompile(compileInfoShowPortIndex);
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
    /// 停止监听状态
    /// </summary>
    public void StopListen()
    {
        if(deviceInfo != null)
        {
            deviceInfo.SetListenStatus(false);
        }
    }
    /// <summary>
    /// 开始监听状态
    /// </summary>
    public void StartListen()
    {
        try
        {
            if (deviceInfo != null)
            {
                deviceInfo.SetListenStatus(true);
                deviceInfo.SetListenAddress(srcAddressField.GetComponent<InputField>().text);
                deviceInfo.SetListenPort(int.Parse(srcPortField.GetComponent<InputField>().text));
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
    /// <summary>
    /// 发送调试信息
    /// </summary>
    public void SendDebugListenMessage()
    {
        try
        {
            deviceInfo.SetListenAddress(srcAddressField.GetComponent<InputField>().text);
            deviceInfo.SetListenPort(int.Parse(srcPortField.GetComponent<InputField>().text));
            deviceInfo.DebugSendListenMessage(desAddressField.GetComponent<InputField>().text, int.Parse(desPortField.GetComponent<InputField>().text), sendInfoTextField.GetComponent<InputField>().text);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
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
    /// <summary>
    /// 取得界面栈大小
    /// </summary>
    /// <returns>界面栈大小数值</returns>
    public int GetStackSize()
    {
        return panels.Count;
    }

    public static string GameNameDeviceInfoPanel = "DeviceInfoPanel";
}