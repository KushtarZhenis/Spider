using COMMON;
using System.Net;
using NAudio.Wave;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Net.Mime;
using Newtonsoft.Json.Linq;

namespace SongManager;
public class SongHelper
{
    private static readonly string homeUrl = "https://www.ankui.kz/";
    private static readonly string songsUrl = "https://www.ankui.kz/kz/ander/";
    private static readonly string partialUrl = ".html";
    private static readonly string apiUrl = "https://www.ankui.kz/kz/GetMusicInfo";
    private static readonly string mp3DirectoryPath = "/Users/kushtar/Desktop/Github/CSharp/Song/Song/MP3";
    private static readonly string errorsFilePath = "/Users/kushtar/Desktop/Github/CSharp/Song/Song/Errors.txt";
    private static readonly string indexFilePath = "/Users/kushtar/Desktop/Github/CSharp/Song/Song/PageIndex.txt";
    private static readonly string songUrlsFilePath = "/Users/kushtar/Desktop/Github/CSharp/Song/Song/SongUrls.json";
    private static readonly int totalPage = 383;
    private static int errorCount = 0;

    public static async Task StartDownloading()
    {
        int count = 1;
        Console.ForegroundColor = ConsoleColor.Green;
        string jsonContent = File.ReadAllText(songUrlsFilePath);
        JArray jsonArray = DecodeHtmlEntitiesInArray(jsonContent);
        int totalNum = jsonArray.Count;
        foreach (JToken item in jsonArray)
        {
            string songUrl = item["data"]["url"].Value<string>().Replace("https://www.ankui.kz/", "https://www.ankui.kz");
            string name = Path.GetFileName(songUrl);
            string extension = Path.GetExtension(songUrl);
            string mp3SavePath = Path.Combine(mp3DirectoryPath, name);

            try
            {
                if (new FileInfo(mp3SavePath).Exists == false)
                {
                    File.Create(mp3SavePath);
                    await DownloadMp3Async(mp3SavePath, songUrl);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(errorsFilePath, $"{Environment.NewLine}Error {++errorCount} : At Url = {songUrl}{Environment.NewLine}{ex.Message}");
            }
            finally
            {
                Console.Clear();
                double percentage = Math.Round(100.00 * count / totalNum, 2);
                Console.WriteLine($"{Environment.NewLine + Environment.NewLine}  Finished : {count} / {totalNum} => {percentage}%{Environment.NewLine + Environment.NewLine}");
                count++;
            }
            Console.WriteLine("---------------Over---------------");
            Console.ResetColor();
        }
    }

    public static async Task StartAddingUrls()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        await Task.Run(async () =>
          {
              File.WriteAllText(songUrlsFilePath, "[" + Environment.NewLine);
              int pageNumber = Convert.ToInt32(File.ReadAllText(indexFilePath));

              for (; pageNumber <= totalPage; pageNumber++)
              {
                  try
                  {
                      string pageUrl = songsUrl + pageNumber + partialUrl;
                      List<string> songPageUrls = GetPageUrlList(pageUrl);
                      string appendingContent = string.Empty;
                      foreach (string songPageUrl in songPageUrls)
                      {
                          string songId = songPageUrl[7..].Replace(".html", "");
                          var formData = new Dictionary<string, string> 
                          { 
                            { "musicid", $"{songId}" }, 
                            { "language", "kz" }, 
                            };
                          string jsonResponse = await SendPostRequest(apiUrl, formData);
                          PostResponse json = JsonConvert.DeserializeObject<PostResponse>(jsonResponse);
                          string mp3Url = homeUrl + json.Data.Url;

                          appendingContent += @$"
    {{
        ""message"": ""{json.Message}"",
        ""status"": ""{json.Status}"",
        ""backUrl"": ""{json.BackUrl}"",
        ""data"": 
        {{
            ""url"": ""{mp3Url}"",
            ""lyric"": ""{json.Data.Lyric}""
        }}
    }},{Environment.NewLine}";
                          string destinationFilePath = Path.Combine(mp3DirectoryPath, songId, ".mp3");
                      }
                      File.AppendAllText(songUrlsFilePath, appendingContent);
                  }
                  catch (Exception ex)
                  {
                      File.AppendAllText(errorsFilePath, ex.Message);
                  }
                  finally
                  {
                      double percentage = Math.Round(100.00 * pageNumber / totalPage, 2);
                      Console.Clear();
                      Console.WriteLine($"{Environment.NewLine + Environment.NewLine}  Finished : {pageNumber} / {totalPage} => {percentage}%{Environment.NewLine + Environment.NewLine}");
                      File.WriteAllText(indexFilePath, pageNumber.ToString());
                  }
              }
          });
    }

    // private static List<string> GetPageUrlList(string PageUrl)
    // {
    //     List<string> res = new List<string>();
    //     HtmlDocument html = HtmlHelper.GetHtmlDom(PageUrl);
    //     var links = html.DocumentNode.SelectNodes("//a[@class='name-link']");
    //     foreach (var link in links)
    //     {
    //         string url = link.GetAttributeValue("href", "");
    //         res.Add(url);
    //     }

    //     return res;
    // }

    static async Task DownloadMp3Async(string url, string savePath)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes(savePath, imageBytes);
        }
    }

    private static JArray DecodeHtmlEntitiesInArray(string input)
    {
        JArray jsonArray = JArray.Parse(input);

        foreach (JToken item in jsonArray)
        {
            DecodeHtmlEntitiesInObject(item);
        }

        return jsonArray;
    }

    private static void DecodeHtmlEntitiesInObject(JToken token)
    {
        foreach (JProperty property in token.Children<JProperty>())
        {
            if (property.Value.Type == JTokenType.String)
            {
                property.Value = WebUtility.HtmlDecode(property.Value.ToString());
            }
            else if (property.Value.Type == JTokenType.Object || property.Value.Type == JTokenType.Array)
            {
                DecodeHtmlEntitiesInObject(property.Value);
            }
        }
    }

    private static async Task<string> SendPostRequest(string apiUrl, Dictionary<string, string> formData)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            var content = new FormUrlEncodedContent(formData);

            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }

    private class PostResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public string BackUrl { get; set; }
        public ResponseData Data { get; set; }
    }

    private class ResponseData
    {
        public string Url { get; set; }
        public string Lyric { get; set; }
    }

}
