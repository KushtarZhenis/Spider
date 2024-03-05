using System.Collections.Specialized;
using System.Net.Http.Headers;

namespace COMMON;

public class LanguageHelper
{
    public static string LanguagePack()
    {
        using (HttpClient client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://www.sozdikqor.org");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/query/all");
            HttpResponseMessage response = client.SendAsync(request).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
