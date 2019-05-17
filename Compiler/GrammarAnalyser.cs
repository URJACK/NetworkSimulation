using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 语法分析器是在词法分析的基础之上，对单词的组成进行结构分析。从而得到正确的指令，语法分析器需要与位置触发器搭配使用。
/// 此处因为语法与语义的检测功能较为耦合，单独拆分开反而实现起来略显冗杂，故将语法分析和语义分析两个功能模块放在了一起。
/// 语法分析、语义分析是逐条进行的，
/// 每一条基本语句的分析结果（也就是生成的指令、变量命名空间、函数命名空间）都会暂存到一个缓冲数据结构中去，
/// 如果最终无误，缓冲数据结构才会将内容拷贝到执行器中去。
/// 与词法分析器相同，都需要在生成实例后设置编译器自身作为它的引用
/// </summary>
public class GrammarAndSemanticAnalyser
{
    private static GrammarAndSemanticAnalyser instance;
    public static GrammarAndSemanticAnalyser GetInstance()
    {
        if(instance == null)
        {
            instance = new GrammarAndSemanticAnalyser();
        }
        return instance;
    }
    private GrammarAndSemanticAnalyser()
    {
        compileLayers = new Stack<CompileLayer>();
        //新设立一个breakStack从而为break; 语句提供准确的跳转地址
        breakStacks = new Stack<Instruction>();
        ExceptionMessage.DebugLog("语法编译器生成完毕......");
    }
    public void SetCompiler(Compiler compiler)
    {
        this.compiler = compiler;
    }
    public void SetExecutor(Executor executor)
    {
        this.executor = executor;
    }
    /// <summary>
    /// 编译器的引用实例
    /// </summary>
    private Compiler compiler;
    /// <summary>
    /// 执行器的引用实例
    /// </summary>
    private Executor executor;
    /// <summary>
    /// 编译用层级序列
    /// </summary>
    private Stack<CompileLayer> compileLayers;
    /// <summary>
    /// 编译用break专用序列
    /// break专用序列是一个专门用来处理break逻辑的序列
    /// 当进入一个新建的“循环逻辑层”的时候，我们会尝试为stack新增一个InstructionType.None的指令
    /// 当接收到一个break;语句的时候，我们会尝试将该指令的引用传入当前的Stack之中
    /// </summary>
    private Stack<Instruction> breakStacks;
    /// <summary>
    /// 正在读取的语法已经识别出来的语法模式
    /// </summary>
    private GrammerType readingGrammerType;
    /// <summary>
    /// 语法分析时，遍历单词使用的循环变量
    /// </summary>
    private int readingCursor;
    /// <summary>
    /// 语法分析执行过程
    /// 关于语法分析的设计思路，
    /// 与词法分析类似，我们采用模式匹配的状态思路，每一个单词或者两个电词都必然对应着一种模式
    /// </summary>
    /// <param name="compileWords">词法分析得到的单词序列</param>
    public void Analysis(ArrayList compileWords)
    {
        ExceptionMessage.DebugLog("正在初始化语法分析变量......");
        readingGrammerType = GrammerType.None;
        compileLayers.Clear();
        breakStacks.Clear();
        //给编译的层级默认添加一个默认层
        CompileLayer defaultLayer = new CompileLayer(LayerType.None, null);
        compileLayers.Push(defaultLayer);
        //读取的游标置为0，从最开始开始读取单词
        readingCursor = 0;
        ExceptionMessage.DebugLog("......初始化语法分析变量完成");
        ExceptionMessage.DebugLog("即将进行语法分析......");
        for(; readingCursor < compileWords.Count; ++readingCursor)
        {
            //取得当前正在读取的单词
            CompileWord currentWord = (CompileWord)compileWords[readingCursor];
            if(readingGrammerType == GrammerType.None)
            {
                ActionMode_None(currentWord);
            }
        }
        ExceptionMessage.DebugLog("语法分析结束......");
    }
    /// <summary>
    /// 无单词的时候的行为模式
    /// </summary>
    /// <param name="currentWord"></param>
    private void ActionMode_None(CompileWord currentWord)
    {

    }
}
