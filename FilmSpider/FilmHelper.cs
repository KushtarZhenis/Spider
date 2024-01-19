using System.Net;
using COMMON;
using HtmlAgilityPack;
using MODEL;

namespace FilmSpider;
public class FilmHelper
{
    public static bool IsContainsFilm(List<Film> filmList, int pageIdx)
    {
        foreach (Film film in filmList)
        {
            if (film.PageIdx == pageIdx) return true;
        }
        return false;
    }

    public static void WriteErrorMsg(string msg)
    {
        File.AppendAllText("/Users/kushtar/Desktop/SpiderSources/Film/Errors.txt", msg + Environment.NewLine);
    }

    public static async Task<string> GetPageHtml(string url)
    {
        try
        {
            HtmlDocument dom = HtmlHelper.GetHtmlDom(url);
            var node = dom.DocumentNode.SelectSingleNode("//script");

            string[] strArr = node.InnerHtml[18..].Split('=');
            string cookieName = strArr[0][2..];
            string cookieValue = strArr[1][..strArr[1].IndexOf(';')];

            var cookieContainer = new CookieContainer();
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler);

            cookieContainer.Add(new Uri(url), new Cookie(cookieName, cookieValue));
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                return res;
            }
            else return "";
        }
        catch (Exception ex)
        {
            WriteErrorMsg(ex.Message);
            return "";
        }
    }
}