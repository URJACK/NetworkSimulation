using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 指令结构
/// </summary>
public class Instruction
{
    /// <summary>
    /// 指令类型
    /// 用来指明指令的用途
    /// </summary>
    public InstructionType type;
    /// <summary>
    /// 指令的参数1
    /// </summary>
    public string param1;
    /// <summary>
    /// 指令的参数2
    /// </summary>
    public string param2;
    /// <summary>
    /// 指令的构造函数
    /// </summary>
    /// <param name="type">指令的类型</param>
    public Instruction(InstructionType type)
    {
        this.type = type;
    }
}
