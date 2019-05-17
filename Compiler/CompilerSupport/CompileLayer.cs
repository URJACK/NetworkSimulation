using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 编译层级
/// </summary>
public class CompileLayer
{
    /// <summary>
    /// 表示当前层的类型
    /// </summary>
    public LayerType type;
    /// <summary>
    /// 表示当前层的开始PC编号，一般用于记录层的跳转指令所在编号
    /// </summary>
    public Instruction startInstruction;
    /// <summary>
    /// 传入Layer的信息构造
    /// </summary>
    /// <param name="type">层的类型</param>
    /// <param name="startInstruction">跳转指令的引用</param>
    public CompileLayer(LayerType type,Instruction startInstruction)
    {
        this.type = type;
        this.startInstruction = startInstruction;
    }
}
