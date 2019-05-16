using UnityEngine;
using System.Collections;

/// <summary>
/// 编译器模块
/// </summary>
public class Compiler 
{
    private static Compiler instance;
    public static Compiler GetInstance()
    {
        if(instance == null)
        {
            instance = new Compiler();
        }
        return instance;
    }
    private Compiler()
    {
        Debug.Log("编译器模块已经生成");
    }

    /// <summary>
    /// 开始编译代码
    /// </summary>
    /// <param name="code">需要被编译的源码</param>
    /// <returns>编译最终的结果</returns>
    public bool Compile(string code,DeviceInfo deviceInfo)
    {
        string CompiledResult = "编译失败";
        deviceInfo.SetCompiledInfo(CompiledResult);
        return false;
    }
}
