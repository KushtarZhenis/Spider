using HtmlAgilityPack;

namespace COMMON;
public class HtmlHelper
{
    public static HtmlDocument GetHtmlDom(string url)
    {

        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load(url);

        return document;
    }

    public static string GetHtmlDomAsString(string url)
    {

        HtmlWeb web = new HtmlWeb();
        string document = web.Load(url).DocumentNode.OuterHtml;

        return document;
    }

}