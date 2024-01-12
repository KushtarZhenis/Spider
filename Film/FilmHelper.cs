using COMMON;
using MODEL;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace Film;
public class FilmHelper
{
    private static readonly string imgDirectoryPath = "/Users/kushtar/Desktop/Github/CSharp/Films/Films/FilmIamges";

    private static readonly string errorMessageFilePath = "/Users/kushtar/Desktop/Github/CSharp/Films/Films/Errors.txt";
    private static int errorCount = 0;

    public static async Task StartDownLoading(int totalNum, string filmUrlsFilePath)
    {
        int count = 1;
        Console.ForegroundColor = ConsoleColor.Green;
        string jsonContent = File.ReadAllText(filmUrlsFilePath);

        List<MODEL.Film> filmList = JsonHelper.DeSerializeObject<List<MODEL.Film>>(jsonContent);

        foreach (MODEL.Film film in filmList)
        {
            string id = film.Id;
            string imgUrl = film.ImageUrl;
            string extension = Path.GetExtension(imgUrl);
            string imgSavePath = imgDirectoryPath + '/' + id + extension;

            try
            {
                if (new FileInfo(imgSavePath).Exists == false)
                {
                    await DownloadImage(imgSavePath, imgUrl);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(errorMessageFilePath, $"{Environment.NewLine}Error {++errorCount} : At id = {id}{Environment.NewLine}{ex.Message}");
            }
            finally
            {
                Console.Clear();
                double percentage = Math.Round(100.00 * count / totalNum, 2);
                Console.WriteLine($"{Environment.NewLine + Environment.NewLine}  Finished : {count} / {totalNum} => {percentage}%{Environment.NewLine + Environment.NewLine}");
                count++;
            }
        }
        Console.WriteLine("---------------Over---------------");
        Console.ResetColor();
    }

    public static async Task DownloadImage(string savePath, string imageUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(imageUrl);
            response.EnsureSuccessStatusCode();

            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes(savePath, imageBytes);
        }
    }

    public static bool IsContainsPageUrl(string pageUrl, List<string> filmList)
    {
        bool isContains = false;
        foreach (string film in filmList)
        {
            if (film.Equals(pageUrl, StringComparison.OrdinalIgnoreCase))
            {
                isContains = true;
                break;
            }
        }
        return isContains;
    }

    public static string GetPageHtmlDom(string url)
    {
        string html = HtmlHelper.GetHtmlDomAsString(url);

        string cookieName = "ge_js_validator_88";
        string pattern = $@"{cookieName}=(.*?);";
        Match match = Regex.Match(html, pattern);
        string cookieValue = match.Success ? match.Groups[1].Value : string.Empty;

        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Cookie", $"{cookieName}={cookieValue}");
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}