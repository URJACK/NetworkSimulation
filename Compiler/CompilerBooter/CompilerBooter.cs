using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompilerBooter : MonoBehaviour
{
    private Compiler compiler;
    void Start()
    {
        ExceptionMessage.DebugLog("正在尝试启动编译器......");
        compiler = Compiler.GetInstance();
    }

}
