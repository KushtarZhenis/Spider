using COMMON;
using Film;
using HtmlAgilityPack;
using MODEL;

Console.ForegroundColor = ConsoleColor.DarkGreen;

string siteUrl = "https://3bt0.com";
string filePath = "/Users/kushtar/Desktop/SpiderSources/Film/Test.json";
int maxPageNum = 3850;
List<string> urls = [];

DateTimeHelper.StartPoint(maxPageNum);
for (int pageIdx = 1; pageIdx <= maxPageNum; pageIdx++)
{
    string pageUrl = Path.Combine(siteUrl, $"movie/1.html?page={pageIdx}");
    string html = await FilmHelper.GetPageHtml(pageUrl);
    HtmlDocument dom = new HtmlDocument();
    dom.LoadHtml(html);

    var nodes = dom.DocumentNode.SelectNodes("//a[contains(@class, 'block')]");
    if (nodes == null) continue;
    foreach (var node in nodes)
    {
        urls.Add(node.GetAttributeValue("href", "Empty").Replace("..", siteUrl));
    }
    DateTimeHelper.CalculatePointInLoop();
}

string content = JsonHelper.SerializeObject(urls);
Console.WriteLine(urls.Count);
await File.WriteAllTextAsync(filePath, content);
Console.WriteLine("------------Over------------");