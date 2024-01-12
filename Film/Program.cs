using COMMON;
using MODEL;
using Film;
using HtmlAgilityPack;
using System.Net.Mime;

// await FilmHelper.StartDownLoading();

string homeUrl = "https://3bt0.com";
string partialUrl = "/movie/1.html?page=";
string filmUrlsFilePath = "/Users/kushtar/Desktop/Github/CSharp/Films/Films/FilmPageUrls.json";
string testingFilePath = "/Users/kushtar/Desktop/Github/CSharp/Films/Films/Test.txt";
string errorFilePath = "/Users/kushtar/Desktop/Github/CSharp/Films/Films/Errors.txt";
int totalPages = 3848;

Console.ForegroundColor = ConsoleColor.Green;
string jsonContent = File.ReadAllText(filmUrlsFilePath);
List<MODEL.Film> jsonList = JsonHelper.DeSerializeObject<List<MODEL.Film>>(jsonContent);
List<string> filmList = jsonList.Select(x => x.PageUrl).ToList();
jsonList.Clear();

await Task.Run(async () =>
{
    for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
    {
        string html = FilmHelper.GetPageHtmlDom(homeUrl + partialUrl + pageNumber);
        HtmlDocument dom = new HtmlDocument();
        dom.LoadHtml(html);
        if (dom == null || dom.DocumentNode == null)
        {
            await File.AppendAllTextAsync(errorFilePath, Environment.NewLine + $"Error : {homeUrl + partialUrl + pageNumber} url is invalid");
            continue;
        }

        HtmlNodeCollection links = dom.DocumentNode.SelectNodes("//a[contains(@class, 'block text-block')]");
        if (links != null)
        {
            int totalLinks = links.Count;
            foreach (HtmlNode link in links)
            {
                string content = string.Empty;
                try
                {
                    string url = homeUrl + link.GetAttributeValue("href", "").Replace("..", "");
                    if (url != homeUrl && !FilmHelper.IsContainsPageUrl(url, filmList))
                    {
                        content += url + Environment.NewLine;
                    }
                }
                catch (Exception ex)
                {
                    await File.AppendAllTextAsync(errorFilePath, Environment.NewLine + $"Error : {ex.Message}");
                }
                finally
                {
                    await File.AppendAllTextAsync(testingFilePath, content);
                    double pagePercent = Math.Round(100.00 * pageNumber / totalPages, 2);
                    double linkPercent = Math.Round(100.00 * (links.IndexOf(link) + 1) / totalLinks, 2);

                    Console.Clear();
                    Console.WriteLine(Environment.NewLine + $"{pageNumber} / {totalPages} => {pagePercent}%");
                    Console.WriteLine(Environment.NewLine + $"{links.IndexOf(link) + 1} / {totalLinks} => {linkPercent}%");
                }
            }
        }
    }
});
