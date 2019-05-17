using System.Collections;
using System.Collections.Generic;

public class CompilerInitial
{
    private static CompilerInitial instance;
    public static CompilerInitial GetInstance()
    {
        if(instance == null)
        {
            instance = new CompilerInitial();
        }
        return instance;
    }
    private CompilerInitial()
    {
        ExceptionMessage.DebugLog("编译器初始器生成完毕......");
    }
    /// <summary>
    /// 生成符号记录器
    /// 符号记录器中记录的单字节符号与多字节符号
    /// 之所以分为单字节符号与多字节符号是因为单字节的符号能够组成成为多字节符号
    /// 而单字节符号与多字节符号分开存储是因为在检测生成单词的时候单字节单独存储是一个必须的操作
    /// </summary>
    public void InitOperatorAndSymbolRecorder(HashSet<char> symbolRecorder, HashSet<char> operatorSingleRecorder, HashSet<string> operatorMultipleRecorder)
    {
        symbolRecorder.Add(',');
        symbolRecorder.Add(';');
        symbolRecorder.Add('{');
        symbolRecorder.Add('}');
        symbolRecorder.Add('[');
        symbolRecorder.Add(']');
        symbolRecorder.Add('(');
        symbolRecorder.Add(')');

        operatorSingleRecorder.Add('.');
        operatorSingleRecorder.Add('=');
        operatorSingleRecorder.Add('>');
        operatorSingleRecorder.Add('<');
        operatorSingleRecorder.Add('+');
        operatorSingleRecorder.Add('-');
        operatorSingleRecorder.Add('*');
        operatorSingleRecorder.Add('/');
        operatorSingleRecorder.Add('%');
        operatorSingleRecorder.Add('^');
        operatorSingleRecorder.Add('&');
        operatorSingleRecorder.Add('|');
        operatorSingleRecorder.Add('!');

        operatorMultipleRecorder.Add("<=");
        operatorMultipleRecorder.Add("==");
        operatorMultipleRecorder.Add(">=");
        operatorMultipleRecorder.Add("+=");
        operatorMultipleRecorder.Add("-=");
        operatorMultipleRecorder.Add("*=");
        operatorMultipleRecorder.Add("/=");
        operatorMultipleRecorder.Add("||");
        operatorMultipleRecorder.Add("&&");
    }
    /// <summary>
    /// 初始化关键字记录器
    /// </summary>
    public void InitKeywordRecorder(HashSet<string> keywordRecorder)
    {
        keywordRecorder.Add("void");
        keywordRecorder.Add("bool");
        keywordRecorder.Add("float");
        keywordRecorder.Add("int");
        keywordRecorder.Add("byte");
        keywordRecorder.Add("bit");
        keywordRecorder.Add("return");
        keywordRecorder.Add("break");
        keywordRecorder.Add("while");
        keywordRecorder.Add("if");
    }
}