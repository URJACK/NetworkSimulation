using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserOfCompile : MonoBehaviour
{
    public InputField inputField;
    public void Compile()
    {
        Compiler compiler = Compiler.GetInstance();
    }
    public void GetWords()
    {
        Compiler compiler = Compiler.GetInstance();
        Debug.Log("正在尝试检查词法分析结果.......");
        for (int i = 0; i < compiler.compileWords.Count; ++i)
        {
            CompileWord compileWord = ((CompileWord)compiler.compileWords[i]);
            ExceptionMessage.DebugLog(compileWord.content);
            ExceptionMessage.DebugLog(compileWord.type.ToString());
        }
    }
}