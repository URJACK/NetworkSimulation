using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 编译、执行器是核心模块，
/// 它可以解析文本，并且根据文本生成指令
/// </summary>
public class Compiler
{
    private static Compiler instance;
    /// <summary>
    /// 取得编译器实例
    /// </summary>
    /// <returns></returns>
    public static Compiler GetInstance()
    {
        if (instance == null)
        {
            instance = new Compiler();
        }
        return instance;
    }
    /// <summary>
    /// 关键字记录器
    /// 用来存储编译过程中所有的关键字
    /// </summary>
    public HashSet<string> keywordRecorder;
    /// <summary>
    /// 符号记录器
    /// 编译器能够识别的所有的单字节符号的集合
    /// </summary>
    public HashSet<char> symbolRecorder;
    /// <summary>
    /// 单运算符记录器
    /// 编译器能够识别的所有的单字节运算符集合
    /// </summary>
    public HashSet<char> operatorSingleRecorder;
    /// <summary>
    /// 多运算符记录器
    /// 编译器能够识别的所有的多字节运算符集合
    /// </summary>
    public HashSet<string> operatorMultipleRecorder;
    /// <summary>
    /// 编译单词记录器
    /// 词法分析器将分析之后得到的所有单词全部放入记录器之中
    /// </summary>
    public ArrayList compileWords;
    /// <summary>
    /// 词法分析器
    /// 能够分析原式字串的内容，并将他们转为单词赋给编译器。
    /// </summary>
    private LexicalAnalyzer lexicalAnalyzer;
    /// <summary>
    /// 语法语义分析器
    /// 能够将得到的词语序列转为合适的指令
    /// 指令会暂存，并确认无误后会发送给执行器
    /// </summary>
    private GrammarAndSemanticAnalyser grammarAndSemanticAnalyser;
    /// <summary>
    /// 缓存执行器
    /// </summary>
    private Executor cacheExecutor;
    /// <summary>
    /// 实际执行器
    /// </summary>
    private Executor realExecutor;
    /// <summary>
    /// 构造函数
    /// 我们需要优先尝试生成关键字记录器
    /// </summary>
    private Compiler()
    {
        CompilerInitial compilerInitial = CompilerInitial.GetInstance();
        this.keywordRecorder = new HashSet<string>();
        //初始化关键字序列
        compilerInitial.InitKeywordRecorder(this.keywordRecorder);
        this.compileWords = new ArrayList();
        this.symbolRecorder = new HashSet<char>();
        this.operatorSingleRecorder = new HashSet<char>();
        this.operatorMultipleRecorder = new HashSet<string>();
        //初始化符号与运算符序列
        compilerInitial.InitOperatorAndSymbolRecorder(this.symbolRecorder,this.operatorSingleRecorder ,this.operatorMultipleRecorder);
        //生成词法分析器实例，并将该编译器自身的实例给予词法分析器
        this.lexicalAnalyzer = LexicalAnalyzer.GetInstance();
        this.lexicalAnalyzer.SetCompiler(this);
        //生成语法语义分析器实例，并将该编译器自身的实例给予语法语义分析器
        this.grammarAndSemanticAnalyser = GrammarAndSemanticAnalyser.GetInstance();
        this.grammarAndSemanticAnalyser.SetCompiler(this);
        //生成执行器
        this.cacheExecutor = Executor.GetCacheInstance();
        this.realExecutor = Executor.GetRealInstance();
        //启动编译器开关
        this.availableFlag = true;
        ExceptionMessage.DebugLog("编译器生成完毕......");
    }
    /// <summary>
    /// 是否允许编译的标记
    /// </summary>
    public bool availableFlag;
    /// <summary>
    /// 读取初始文本应该调用的函数
    /// 在读取完初始文本之后，我们会尝试着对初始文本进行词法分析。
    /// </summary>
    public bool Compile(string str,DeviceInfo deviceInfo)
    {
        if (availableFlag)
        {
            //已经启用编译器，对外部暴露这个接口
            availableFlag = false;
            //先将已经编译获得的单词集合进行一次清空
            if (this.compileWords != null)
            {
                this.compileWords.Clear();
                try
                {
                    //开始词法分析
                    this.lexicalAnalyzer.Analysis(this.compileWords, str);
                    deviceInfo.SetCompiledInfo("Lexical Analyze Has Completed");
                    availableFlag = true;
                    return true;
                }
                catch (Exception e)
                {
                    //抓出异常并打印
                    deviceInfo.SetCompiledInfo(e.Message);
                    availableFlag = true;
                    return false;
                }
            }
            else
            {
                deviceInfo.SetCompiledInfo("编译单词序列出现空指针的错误");
                return false;
            }
        }
        else
        {
            deviceInfo.SetCompiledInfo("编译器正在进行处理，请勿重复调用");
            return false;
        }
    }
}