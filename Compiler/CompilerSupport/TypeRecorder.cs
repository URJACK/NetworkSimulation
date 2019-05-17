using System.Collections;
using System.Collections.Generic;

public enum CharacterType
{
    Name, Number, Symbol, Operator, None
}
public enum LexcialType
{
    Keyword, Constant, Variable, Equal, Symbol, Operator
}
public enum LayerType
{
    Function, Condition, Recycle, None
}
public enum InstructionType
{
    DefBit, DefByte, DefInt, DefFloat, DefBool,
    MovBit, MovByte, MovInt, MovFloat, MovBool,
    JumpNot, Jump,
    FunctionName, FunctionParam, FunctionExecute, Return,
    None
}
public enum GrammerType
{
    DefineFunction, DefineVariable, Move, Condition, Recycle, Single, None
}