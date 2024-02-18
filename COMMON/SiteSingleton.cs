using System;
using System.Collections.Concurrent;

namespace COMMON;

public class SiteSingleton
{
    private static SiteSingleton instance = null;
    private ConcurrentDictionary<string, string> keyValueDic = new ConcurrentDictionary<string, string>();
    private SiteSingleton() { }

    public static SiteSingleton GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new SiteSingleton();
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
    public void SetSiteBaseUrl(string url)
    {
        keyValueDic.AddOrUpdate("baseUrl", url, (key, oldValue) => url);
    }

    public string GetSiteBaseUrl()
    {
        if (keyValueDic.TryGetValue("baseUrl", out string url)) return url;
        return "";
    }
}