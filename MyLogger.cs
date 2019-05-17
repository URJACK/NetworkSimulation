using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MyLogger : MonoBehaviour
{
    /// <summary>
    /// 打印信息
    /// </summary>
    /// <param name="msg"></param>
    public GameObject logPanel;
    public Image logImage;
    public Text logText;

    private static MyLogger instance;
    public static MyLogger GetInstance()
    {
        Compiler.GetInstance();
        if(instance == null)
        {
            instance = new MyLogger();
        }
        return instance;
    }
    private MyLogger()
    {
        Debug.Log("打印者");
    }
    void Start()
    {
        instance = this;
    }
    public static float basicAlphaUnit = 1f / 255;
    private float defaultInfoPanelColorRed = 47 / 255f;      //默认交互信息窗口红色值
    private float defaultInfoPanelColorGreen = 47 / 255f;    //默认交互信息窗口绿色值
    private float defaultInfoPanelColorBlue = 47 / 255f;     //默认交互信息窗口蓝色值

    public void Log(string msg)
    {
        logText.text = msg;
        StartCoroutine(RefreshDisplayIEnumerator());
        
        /*
        int i = 0;
        for (; i < 255; i+= 1)
        {
            logImage.color = new Color(defaultInfoPanelColorRed, defaultInfoPanelColorGreen, defaultInfoPanelColorBlue, i * basicAlphaUnit);
        }
        for (; i >= 0; i -= 1)
        {
            logImage.color = new Color(defaultInfoPanelColorRed, defaultInfoPanelColorGreen, defaultInfoPanelColorBlue, i * basicAlphaUnit);
        }
        */

    }


    IEnumerator RefreshDisplayIEnumerator()
    {
        int i = 0;
        logPanel.SetActive(true);
        
        for (; i < 255; i += 5)
        {
            logImage.color = new Color(defaultInfoPanelColorRed, defaultInfoPanelColorGreen, defaultInfoPanelColorBlue, i * basicAlphaUnit);
            yield return null;
        }
        for (; i >= 0; i -= 5)
        {
            logImage.color = new Color(defaultInfoPanelColorRed, defaultInfoPanelColorGreen, defaultInfoPanelColorBlue, i * basicAlphaUnit);
            yield return null;
        }
        logPanel.SetActive(false);
        
    }
}
