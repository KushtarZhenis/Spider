using COMMON;
using MODEL;
using Film;
using HtmlAgilityPack;

Console.ForegroundColor = ConsoleColor.DarkGreen;

string siteUrl = "https://3bt0.com";
string filePath = "/Users/kushtar/Desktop/SpiderSources/Film/Test.json";
int maxPageNum = 3851;

DateTimeHelper.StartPoint(maxPageNum);
for (int pageIdx = 1; pageIdx <= maxPageNum; pageIdx++)
{
    List<string> urls = [];
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

    Console.Clear();
    DateTimeHelper.CalculatePointInLoop();
    string content = JsonHelper.SerializeObject(urls);
    await File.AppendAllTextAsync(filePath, content + ',');
}

Console.WriteLine("------------Over------------");