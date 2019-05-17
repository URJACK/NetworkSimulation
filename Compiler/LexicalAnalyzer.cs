using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 词法分析器
/// 词法分析器能够将一段原式文本进行代入，从而尝试将他们解析成为一个个的单词
/// 每一个单词都应该具有“位置”，“文本内容”，“词性”这三个属性
/// 因为在根据语法进行编译指令的时候，每一个单词都是一个最基本的单位。
/// 关于“位置”，当语句发生错误的时候，我们需要有一种方式能够准确的告知我们出错的位置，故单词需要“位置”这一属性。
/// “词性”是为了快速进行语法分析而设置的属性。
/// “文本内容”是在进行语法分析和语义分析的时候，需要进行进一步的判定而必须的内容。
/// 词法分析器的生成必须在编译器之后!!!!!!
/// </summary>
public class LexicalAnalyzer
{
    private static LexicalAnalyzer instance;
    public static LexicalAnalyzer GetInstance()
    {
        if (instance == null)
        {
            instance = new LexicalAnalyzer();
        }
        return instance;
    }
    private CharacterType readStatus;
    private string readBuffer;
    private bool floatNumberFlag;
    private bool rowNumIncreaseFlag;
    private int rowNum;
    private int colNum;
    private int cursor;
    private Compiler compiler;
    /// <summary>
    /// 词法分析器构造函数
    /// </summary>
    private LexicalAnalyzer()
    {
        //词法分析器的构造函数中实际上什么也不需要做，成员变量的初始化会在Analysis()方法中去完成
        ExceptionMessage.DebugLog("词法分析器生成完毕......");
    }
    /// <summary>
    /// 设置词法分析器的编译器对象
    /// </summary>
    /// <param name="compiler"></param>
    public void SetCompiler(Compiler compiler)
    {
        this.compiler = compiler;
        if(this.compiler != null)
        {
            ExceptionMessage.DebugLog("词法分析器已经成功配备编译器.....");
        }
    }
    /// <summary>
    /// 解析单词
    /// </summary>
    /// <param name="list">解析的单词内容会被添加到这个list里面去</param>
    /// <param name="text">被解析的原文本</param>
    public void Analysis(ArrayList list, string text)
    {
        ExceptionMessage.DebugLog("开始初始化词法分析变量......");
        readStatus = CharacterType.None;
        readBuffer = "";
        //这个标记位会在回滚后被置为false，当其为false之后，就意味着无法在触发colNum与rowNum的变化函数
        rowNumIncreaseFlag = true;
        //这个标记位会在每次读取新单词的时候进行重置，从而当检测到两次小数点的时候会进行报错
        floatNumberFlag = false;
        //当前阅读的代码行数
        rowNum = 1;
        //当前阅读的代码列数
        colNum = 1;
        ExceptionMessage.DebugLog("准备进行词法分析......");
        for (cursor = 0; cursor < text.Length; ++cursor)
        {
            char character = text[cursor];
            //控制rowNum与colNum的变化
            if (rowNumIncreaseFlag)
            {
                ColAndRowNumIncrease(character);
            }
            else
            {
                rowNumIncreaseFlag = true;
            }
            //尝试取得这个字符所代表的状态
            CharacterType currentStatus = GetCharacterType(character);

            //如果状态当前还是为空的情况
            if (readStatus == CharacterType.None)
            {
                readStatus = currentStatus;
                readBuffer = "";
                floatNumberFlag = false;
                if (currentStatus != CharacterType.None)
                {
                    readBuffer += character;
                }
            }
            //如果当前不为空，则开始比较两个状态，如果发生冲突，则要决定回滚
            else
            {
                if (readStatus == CharacterType.Name)
                {
                    //此时完全可以拓展变量名的长度
                    if (currentStatus == CharacterType.Name || currentStatus == CharacterType.Number)
                    {
                        readBuffer += character;
                    }
                    //此时无法再被识别成为变量名。无论是“符号”或者“运算符”或者“空”
                    else
                    {
                        //自动生成单词以及游标回溯
                        CombineWordAndCursorBack(list);
                    }
                }
                else if (readStatus == CharacterType.Number)
                {
                    //如果在读取数字的过程中随后遇见了CharacterType.Name，那么此时我们会上报这个错误
                    if (currentStatus == CharacterType.Name)
                    {
                        throw new Exception(ExceptionMessage.CreateByLineAndIndex("variable's name is invalid", rowNum, colNum));
                    }
                    //数字的长度是可以扩展
                    else if (currentStatus == CharacterType.Number)
                    {
                        readBuffer += character;
                    }
                    //在检测到是符号的时候，我们需要立即对符号判定是不是小数点
                    else if (currentStatus == CharacterType.Operator)
                    {
                        //如果确实是一个小数点，我们就需要判定是不是多次读入了小数点这种情况
                        if (character == '.')
                        {
                            if (floatNumberFlag == false)
                            {
                                readBuffer += character;
                                floatNumberFlag = true;
                            }
                            else
                            {
                                throw new Exception(ExceptionMessage.CreateByLineAndIndex("float number is invalid", rowNum, colNum));
                            }
                        }
                        //如果不是小数点而是其他的运算符，我们也可以直接生成这个单词
                        else
                        {
                            CombineWordAndCursorBack(list);
                        }
                    }
                    //当检测到是可以“空”以及“符号”类型时，我们也会直接尝试生成这个单词，并进行一次回溯
                    else
                    {
                        CombineWordAndCursorBack(list);
                    }
                }
                //在这里判定运算符的时候，我们采取一种这样的方式
                //我们没有必要去把一个符号的最大长度限定为2字节，我们可以默认符号是多字节的
                //所以我们在这里依旧对符号的判定采取回溯的方式
                else if(readStatus == CharacterType.Operator)
                {
                    //如果后续也是运算符，我们可以尝试先添加它
                    if (currentStatus == CharacterType.Operator)
                    {
                        readBuffer += character;
                    }
                    //如果后续不是运算符，我们就可以直接合并了
                    else
                    {
                        CombineWordAndCursorBack(list);
                    }
                }
                //在这里判定符号是非常简单的，我们只需要查找进行一次单纯的回溯即可
                //根本无需判定当前读入的字符究竟是什么
                else
                {
                    CombineWordAndCursorBack(list);
                }
            }
        }
        CombineWordAndCursorBack(list);
        ExceptionMessage.DebugLog("......词法分析已经结束");
    }
    /// <summary>
    /// 根据当前传入的字符，来控制colNum与rowNum的移动
    /// </summary>
    /// <param name="character"></param>
    private void ColAndRowNumIncrease(char character)
    {
        //检测代码的行数与列数
        //如果当前是一个换行符
        if (character == '\n')
        {
            ++rowNum;
            colNum = 0;
        }
        //如果不是换行符
        else
        {
            ++colNum;
        }
    }
    /// <summary>
    /// 合并编译单词并且移动游标
    /// </summary>
    /// <param name="list"></param>
    private void CombineWordAndCursorBack(ArrayList list)
    {
        CompileWord compileWord = CreateCompileWord();
        //将生成的编译单词添加进入外部传入的序列中
        list.Add(compileWord);
        //readBuffer = ""; 
        //这里不需要对readBuffer做清空，因为每次重新准备进入接受动作的时候，会自动清空。
        //与readBuffer类似 floatNumberFlag也不需要被重置为false
        readStatus = CharacterType.None;
        //尝试让遍历指针回溯
        --cursor;
        //回滚之后第一次再次检测字符的时候，无法再让横纵坐标移动了
        rowNumIncreaseFlag = false;
    }
    /// <summary>
    /// 合并编译单词
    /// </summary>
    /// <param name="list"></param>
    private void CombineWord(ArrayList list)
    {
        CompileWord compileWord = CreateCompileWord();
        //将生成的编译单词添加进入外部传入的序列中
        list.Add(compileWord);
        //readBuffer = ""; 
        //这里不需要对readBuffer做清空，因为每次重新准备进入接受动作的时候，会自动清空。
        //与readBuffer类似 floatNumberFlag也不需要被重置为false
        readStatus = CharacterType.None;
    }
    /// <summary>
    /// 根据一个传入原式单词字符串，来生成一个单词
    /// </summary>
    /// <returns></returns>
    private CompileWord CreateCompileWord()
    {
        CompileWord compileWord = new CompileWord();
        compileWord.colNum = this.colNum;
        compileWord.rowNum = this.rowNum;
        compileWord.content = this.readBuffer;
        //当现在是“命名”词性的时候
        if (readStatus == CharacterType.Name)
        {
            //如果是单词是关键字
            if (this.compiler.keywordRecorder.Contains(compileWord.content))
            {
                compileWord.type = LexcialType.Keyword;
            }
            else if (compileWord.content == "true" || compileWord.content == "false")
            {
                compileWord.type = LexcialType.Constant;
            }
            else
            {
                compileWord.type = LexcialType.Variable;
            }
        }
        else if (readStatus == CharacterType.Number)
        {
            //如果最后一位不是小数点，才可以被视作数值常量
            if (compileWord.content[compileWord.content.Length - 1] != '.')
            {
                compileWord.type = LexcialType.Constant;
            }
            else
            {
                throw new Exception(ExceptionMessage.CreateByLineAndIndex("the float number is invalid", rowNum, colNum));
            }
        }
        else if(readStatus == CharacterType.Operator)
        {
            //当长度为1的时候，我们需要检测是否是赋值符号、或者是否是普通单字节符号
            //赋值符号也在单字节符号集之中，故我们需要先判定赋值符号，提高其判定优先级
            if (compileWord.content.Length == 1)
            {
                //如果优先是赋值符号，则优先命名为赋值符号
                if (compileWord.content[0] == '=')
                {
                    compileWord.type = LexcialType.Equal;
                }
                //如果在单运算符包括内，那么我们可以命名为运算符
                else if (this.compiler.operatorSingleRecorder.Contains(compileWord.content[0]))
                {
                    compileWord.type = LexcialType.Operator;
                }
                //如果不包括在运算符内，实际上按常理将是不可能的，因为不然readStatus就不可能为Operator了
                else
                {
                    throw new Exception(ExceptionMessage.CreateByLineAndIndex("No suitable single-operator", rowNum, colNum));
                }
            }
            //当长度为多的时候
            else
            {
                if (this.compiler.operatorMultipleRecorder.Contains(compileWord.content))
                {
                    compileWord.type = LexcialType.Operator;
                }
                else
                {
                    throw new Exception(ExceptionMessage.CreateByLineAndIndex("this kind of symbol'" + compileWord.content + "'is not exist", rowNum, colNum));
                }
            }
        }
        //当判定状态是符号的时候，我们可以直接写为符号
        else
        {
            compileWord.type = LexcialType.Symbol;
        }
        return compileWord;
    }
    /// <summary>
    /// 这个方法可以传入一个“字符类型”CharacterType以及传入一个单个单词所代表字符串
    /// 根据这两个内容，就能够生成一个“编译单词”对象。
    /// </summary>
    /// <param name="readBuffer">单个单词的字串内容</param>
    /// <param name="characterType">当前读取单词的状态</param>
    /// <param name="rowNum">当前单词读取的时候的行坐标</param>
    /// <param name="colNum">当前单词读取的时候的列坐标</param>
    /// <returns></returns>
    private CharacterType GetCharacterType(char c)
    {
        if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_')
        {
            return CharacterType.Name;
        }
        else if (c >= '0' && c <= '9')
        {
            return CharacterType.Number;
        }
        else
        {
            if (compiler.symbolRecorder.Contains(c))
            {
                return CharacterType.Symbol;
            }
            else if(compiler.operatorSingleRecorder.Contains(c))
            {
                return CharacterType.Operator;
            }
            else
            {
                return CharacterType.None;
            }
        }
    }
}