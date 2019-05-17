using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExceptionMessage
{
    /// <summary>
    /// 基于行列坐标，创建一个报错信息
    /// </summary>
    /// <param name="msg">错误原因</param>
    /// <param name="line">报错信息的行坐标</param>
    /// <param name="index">报错信息的列坐标</param>
    /// <returns></returns>
    public static string CreateByLineAndIndex(string msg, int line, int index)
    {
        msg += " Line:";
        msg += line;
        msg += " Column:";
        msg += index;
        return msg;
    }
    /// <summary>
    /// 打印调试信息
    /// </summary>
    /// <param name="str">需要打印的调试信息</param>
    public static void DebugLog(string str)
    {
        Debug.Log(str);
    }
}
