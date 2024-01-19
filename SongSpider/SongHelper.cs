using COMMON;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;

namespace SongSpider;
public class SongHelper
{
    public static List<string> GetSongIdList(string PageUrl)
    {
        List<string> res = [];
        HtmlDocument html = HtmlHelper.GetHtmlDom(PageUrl);
        var links = html.DocumentNode.SelectNodes("//a[@class='name-link']");
        if (links != null)
        {
            foreach (var link in links)
            {
                string id = link.GetAttributeValue("href", "").Replace("/kz/an/", "").Replace(".html", "");
                res.Add(id);
            }
        }
        return res;
    }

    public static async Task<string> SendPostRequest(string postUrl, string option)
    {
        using HttpClient httpClient = new HttpClient();

        try
        {
            var formData = JsonHelper.DeSerializeObject<Dictionary<string, string>>(option);
            var content = new FormUrlEncodedContent(formData);

            HttpResponseMessage response = await httpClient.PostAsync(postUrl, content);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            File.AppendAllText("/Users/kushtar/Desktop/SpiderSources/Film/Errors.txt", "SendPostRequest: " + ex.Message + Environment.NewLine);
            return "";
        }
    }
}
