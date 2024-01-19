using COMMON;
using MODEL;
using FilmSpider;
using HtmlAgilityPack;

Console.ForegroundColor = ConsoleColor.DarkGreen;

string siteUrl = "https://3bt0.com";
string filePath = "/Users/kushtar/Desktop/SpiderSources/Film/Film.json";
string indexFilePath = "/Users/kushtar/Desktop/SpiderSources/Film/index.txt";
int maxPageNum = 3855;

string oldContent = File.ReadAllText(filePath);
List<Film> oldFilmList = JsonHelper.DeSerializeObject<List<Film>>(oldContent);

string content = File.ReadAllText(indexFilePath);
int pageIdx = 1;
if (int.TryParse(content, out int res)) pageIdx = res;

DateTimeHelper.StartPoint(maxPageNum, pageIdx);
for (; pageIdx <= maxPageNum; pageIdx++)
{
    if (oldFilmList != null && FilmHelper.IsContainsFilm(oldFilmList, pageIdx))
    {
        DateTimeHelper.ResetPoint();
        continue;
    }

    List<Film> filmList = [];
    string homePagesUrl = Path.Combine(siteUrl, $"movie/1.html?page={pageIdx}");

    string html = await FilmHelper.GetPageHtml(homePagesUrl);
    HtmlDocument dom = new HtmlDocument();
    dom.LoadHtml(html);

    var nodes = dom.DocumentNode.SelectNodes("//a[contains(@class, 'block')]");
    if (nodes == null) continue;

    foreach (var node in nodes)
    {
        var titleNode = node.SelectSingleNode(".//div[contains(@class, 'ov_hid')]/h5");
        string publishYear = titleNode.SelectSingleNode(".//span[contains(@class, 'type--fine-print')]")?.InnerText ?? "";
        string douBanRate = titleNode.SelectSingleNode(".//span[contains(@class, 'tag-picture2')]")?.InnerText.Trim() ?? "";
        var img = node.SelectSingleNode(".//div[contains(@class, 'bgimgcov')]");
        string thumbnailUrl = img?.GetAttributeValue("style", "").Replace("background-image:url", "").Trim('(', ')') ?? "";

        string title = titleNode?.InnerText.Trim() ?? "";
        if (!string.IsNullOrEmpty(publishYear)) title = title.Replace(publishYear, "");
        if (!string.IsNullOrEmpty(douBanRate)) title = title.Replace(douBanRate, "");

        filmList.Add(new Film
        {
            Title = title.Trim(),
            PageIdx = pageIdx,
            PublishYear = publishYear.Trim('(', ')'),
            DouBanRate = douBanRate,
            ThumbnailUrl = thumbnailUrl,
            PageUrl = node.GetAttributeValue("href", "")?.Replace("..", siteUrl)
        });
    }

    Console.Clear();
    DateTimeHelper.CalculatePointInLoop();
    content = JsonHelper.SerializeObject(filmList).Replace("[", "").Replace("]", "") + ",";
    File.AppendAllText(filePath, content);
    File.WriteAllText(indexFilePath, pageIdx.ToString());
}

Console.WriteLine("------------Over------------");