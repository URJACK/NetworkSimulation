using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 执行器
/// 是双例模式
/// </summary>
public class Executor
{
    private static Executor cacheInstance;
    private static Executor realInstance;
    public static Executor GetCacheInstance()
    {
        if(cacheInstance == null)
        {
            ExceptionMessage.DebugLog("缓存执行器正在生成");
            cacheInstance = new Executor();
        }
        return cacheInstance;
    }
    public static Executor GetRealInstance()
    {
        if(realInstance == null)
        {
            ExceptionMessage.DebugLog("实际执行器正在生成");
            realInstance = new Executor();
        }
        return realInstance;
    }
    private Executor() { }
}
