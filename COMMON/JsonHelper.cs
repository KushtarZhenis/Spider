using Newtonsoft.Json;

namespace COMMON;
public class JsonHelper
{
    public static string SerializeObject(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T DeSerializeObject<T>(string str)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        catch
        {
            return default;
        }

    }
}
