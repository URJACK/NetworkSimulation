using UnityEngine;
using System.Collections;

/// <summary>
/// 动作会话记录器，single-instance
/// 能够记录下用户的动作
/// </summary>
public class ActionSessionRecorder
{
    private static ActionSessionRecorder instance;
    public static ActionSessionRecorder GetInstance()
    {
        if (instance == null)
        {
            instance = new ActionSessionRecorder();
        }
        return instance;
    }
    private ActionSessionRecorder()
    {
        Debug.Log("已经创建动作会话记录器");
    }

    /// <summary>
    /// 被记录的actionSession对象本身
    /// </summary>
    private ActionSession actionSession;
    /// <summary>
    /// 记录一个新的动作会话，会触发前一个动作会话的break动作
    /// </summary>
    /// <param name="session">新的动作会话实例</param>
    public void RecordSession(ActionSession session)
    {
        if (actionSession != null)
        {
            actionSession.Break();
        }
        actionSession = session;
    }
    /// <summary>
    /// 调用记录的动作会话对象去执行移动代码
    /// </summary>
    /// <param name="movementCode">被执行的动作代码</param>
    public bool ChangeStatus(int movementCode)
    {
        if(actionSession != null)
        {
            return actionSession.ChangeStatus(movementCode);
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 取得被记录的actionsession对象
    /// </summary>
    /// <returns></returns>
    public ActionSession GetSession()
    {
        return actionSession;
    }
    /// <summary>
    /// 清空动作会话
    /// </summary>
    public void ClearSession()
    {
        if (actionSession != null)
        {
            actionSession.Break();
        }
        actionSession = null;
    }
}