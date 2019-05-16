using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  动作会话
///  这个类是一个抽象类，用来规定传输的动作
/// </summary>
public abstract class ActionSession
{
    /// <summary>
    /// 通过“动作代码”到“执行动作”的映射
    /// </summary>
    private Dictionary<int, Action> actions;
    /// <summary>
    /// 记录器的key1为“行动代码”，key2是“原状态代码”，value是“目标状态代码”
    /// </summary>
    private Dictionary<int, Dictionary<int, int>> directions;
    /// <summary>
    /// key1是“行动代码”，key2是“状态代码”
    /// </summary>
    private Dictionary<int, Dictionary<int, List<int> >> actionCodes;
    /// <summary>
    /// 当前的状态码
    /// </summary>
    private int statusCode;

    protected Dictionary<int, Action> Actions { get => actions; set => actions = value; }
    protected int StatusCode { get => statusCode; set => statusCode = value; }
    public Dictionary<int, Dictionary<int, List<int>>> ActionCodes { get => actionCodes; set => actionCodes = value; }

    /// <summary>
    /// 执行器构造函数
    /// </summary>
    protected ActionSession()
    {
        this.actions = new Dictionary<int, Action>();
        this.actionCodes = new Dictionary<int, Dictionary<int, List<int> >>();
        this.directions = new Dictionary<int ,Dictionary<int, int> >();
        this.StatusCode = StatusNull;
    }
    /// <summary>
    /// 传入行动代码
    /// 进而改变动作会话当前的状态
    /// 如果改变状态失败，会尝试报错
    /// </summary>
    /// <param name="movementCode">行动代码</param>
    /// <returns></returns>
    public bool ChangeStatus(int movementCode)
    {
        if (actionCodes.ContainsKey(movementCode))
        {
            if (actionCodes[movementCode].ContainsKey(statusCode))
            {
                foreach (int code in actionCodes[movementCode][statusCode])
                {
                    Action action = actions[code];
                    //执行当前应该执行的动作
                    action.Exec();
                }
            }
        }
        if (movementCode != MoveBreak)
        {
            if (directions.ContainsKey(movementCode))
            {
                if (directions[movementCode].ContainsKey(statusCode))
                {
                    statusCode = directions[movementCode][statusCode];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            statusCode = StatusNull;
            return true;
        }
    }
    /// <summary>
    /// 建立起“动作代码”对“动作”的映射
    /// </summary>
    /// <param name="actionCode">动作代码</param>
    /// <param name="action">执行的动作函数对象</param>
    protected void SetAction(int actionCode, Action action)
    {
        this.actions.Add(actionCode, action);
    }
    /// <summary>
    /// 建立起“行动代码”与“状态代码”的导向
    /// </summary>
    /// <param name="movementCode">行动代码</param>
    /// <param name="statusCode">行动代码对应的行动执行后，会导致状态代码变化的数值</param>
    protected void SetDicrection(int movementCode, int srcStatusCode,int desStatusCode)
    {
        if (!directions.ContainsKey(movementCode))
        {
            directions.Add(movementCode, new Dictionary<int, int>());
        }
        Debug.Log("move:" + movementCode);
        Debug.Log("src:" + srcStatusCode);
        Debug.Log("des:" + desStatusCode);
        directions[movementCode].Add(srcStatusCode, desStatusCode);
    }
    /// <summary>
    /// 建立起“行动代码”和“状态代码”对“动作代码”的映射
    /// 这里可能会有“多个”动作的代码的映射
    /// </summary>
    /// <param name="movementCode">行动代码的数值</param>
    /// <param name="originStatusCode">执行行动代码之前的状态代码的数值</param>
    /// <param name="actionCode">动作代码的数值</param>
    protected void SetActionCode(int movementCode, int originStatusCode, int actionCode)
    {
        if (!actionCodes.ContainsKey(movementCode))
        {
            actionCodes.Add(movementCode, new Dictionary<int,List<int> >());
        }
        if (!actionCodes[movementCode].ContainsKey(originStatusCode))
        {
            actionCodes[movementCode].Add(originStatusCode, new List<int>());
        }
        actionCodes[movementCode][originStatusCode].Add(actionCode);
    }
    /// <summary>
    /// 取得当前的状态代码
    /// </summary>
    /// <returns>状态代码</returns>
    public int GetStatusCode()
    {
        return statusCode;
    }
    /// <summary>
    /// 尝试触发break事件的相关函数
    /// </summary>
    public void Break()
    {
        ChangeStatus(MoveBreak);
    }
    /// <summary>
    /// 执行动作
    /// </summary>
    /// <param name="code">被执行的动作代码</param>
    public void ExecAction(int code)
    {
        if (actions.ContainsKey(code))
        {
            actions[code].Exec();
        }
    }
    /// <summary>
    /// 空状态对应的代码值
    /// </summary>
    public static int StatusNull = 0;
    /// <summary>
    /// 打断操作对应的整型值
    /// </summary>
    public static int MoveBreak = 0;
}