using System.Collections.Concurrent;

namespace COMMON;

public class SiteSingleton
{
    private static readonly object _lock = new();
    private static SiteSingleton instance = null;
    private ConcurrentDictionary<string, string> keyValueDic = [];
    private SiteSingleton() { }

    public static SiteSingleton GetInstance
    {
        get
        {
            lock (_lock)
            {
                instance ??= new SiteSingleton();
                return instance;
            }
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
    public void SetSiteUrl(string url)
    {
        keyValueDic.AddOrUpdate("siteUrl", url, (key, oldValue) => url);
    }

    public string GetSiteUrl()
    {
        if (keyValueDic.TryGetValue("siteUrl", out string url)) return url;
        return "";
    }
}