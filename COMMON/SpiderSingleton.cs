using System;
using System.Collections.Concurrent;

namespace COMMON;

public class SpiderSingleton
{
    private static SpiderSingleton instance = null;
    private ConcurrentDictionary<string, string> keyValueDic = new ConcurrentDictionary<string, string>();
    private SpiderSingleton() { }

    public static SpiderSingleton GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new SpiderSingleton();
            }
            return instance;
        }
    }

    public void SetConnectionString(string connStr)
    {
        keyValueDic.AddOrUpdate("connectionString", connStr, (key, oldValue) => connStr);
    }

    public string GetConnectionString()
    {
        if (keyValueDic.TryGetValue("connectionString", out string connStr)) return connStr;
        return "";
    }
}