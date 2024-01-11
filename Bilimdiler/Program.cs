using MODEL;
using COMMON;
using HtmlAgilityPack;
using System.Net;

Console.ForegroundColor = ConsoleColor.DarkGreen;

// string articlesFilePath = "/Users/kushtar/Desktop/Github/CSharp/Spider/Bilimdiler/ArticleInfo.json";
// string articlesDirectoryPath = "/Users/kushtar/Desktop/Github/CSharp/Spider/Bilimdiler/Articles/";
// string mediaFilePath = "/Users/kushtar/Desktop/Github/CSharp/Spider/Bilimdiler/Media.txt";
// string errorMessageFilePath = "/Users/kushtar/Desktop/Github/CSharp/Spider/Bilimdiler/ErrorMessage.txt";
// string jsonContent = File.ReadAllText(articlesFilePath);
// Article[] articleList = JsonHelper.DeSerializeObject<Article[]>(jsonContent);

// List<string> mediaList = [];
// List<string> pathList = FileHelper.GetAllFilePath(articlesDirectoryPath);

// int count = 1;

int totalNum = 100;
DateTimeHelper.StartPoint(totalNum);

for (int i = 0; i < totalNum; i++)
{
    await Task.Delay(1000);
    Console.Clear();
    DateTimeHelper.CalculatePointInLoop();
}




// foreach (string filePath in pathList)
// {
//     await Task.Run(async () =>
//     {
//         #region Timer
//         DateTime currentTime = DateTime.Now;
//         count++;
//         double percent = Math.Round(100.00 * count / totalNum, 2);

//         Console.Clear();
//         Console.WriteLine();
//         Console.WriteLine($"{count} / {totalNum} => {percent}%");
//         Console.WriteLine();

//         TimeSpan timePassed = currentTime - beginTime;
//         long leftTotalSeconds = (int)Math.Round(timePassed.TotalSeconds / count * (totalNum - count));
//         TimeSpan timeLeft = new TimeSpan(TimeSpan.TicksPerSecond * leftTotalSeconds);

//         Console.WriteLine($"Total Time Passed => " + DateTimeHelper.FormatHMS(timePassed));
//         Console.WriteLine($"Total Time Left   => " + DateTimeHelper.FormatHMS(timeLeft));
//         #endregion

//         try
//         {
//             string html = File.ReadAllText(filePath);
//             HtmlDocument dom = new HtmlDocument();
//             dom.LoadHtml(html);
//             HtmlNodeCollection img = dom.DocumentNode.SelectNodes("//img");
//             HtmlNodeCollection audio = dom.DocumentNode.SelectNodes("//audio");
//             HtmlNodeCollection video = dom.DocumentNode.SelectNodes("//video");
//             foreach (var item in img)
//             {
//                 mediaList.Add($"{filePath} || {item.OuterHtml}");
//             }
//             foreach (var item in audio)
//             {
//                 mediaList.Add($"{filePath} || {item.OuterHtml}");
//             }
//             foreach (var item in video)
//             {
//                 mediaList.Add($"{filePath} || {item.OuterHtml}");
//             }
//         }
//         catch (Exception ex)
//         {
//             await File.AppendAllTextAsync(errorMessageFilePath, DateTime.Now + " : " + Environment.NewLine + ex.Message);
//         }
//     });
// }

// if (File.Exists(mediaFilePath))
// {
//     File.Delete(mediaFilePath);
// }
// foreach (string media in mediaList)
// {
//     await File.AppendAllTextAsync(mediaFilePath, Environment.NewLine + media);
// }

// foreach (Article article in articleList)
// {
//     await Task.Run(async () =>
//     {
//         #region Timer
//         DateTime currentTime = DateTime.Now;
//         count++;
//         double percent = Math.Round(100.00 * count / totalArticle, 2);

//         Console.Clear();
//         Console.WriteLine();
//         Console.WriteLine($"{count} / {totalArticle} => {percent}%");
//         Console.WriteLine();

//         TimeSpan timePassed = currentTime - beginTime;
//         long leftTotalSeconds = (int)Math.Round(timePassed.TotalSeconds / count * (totalArticle - count));
//         TimeSpan timeLeft = new TimeSpan(TimeSpan.TicksPerSecond * leftTotalSeconds);

//         Console.WriteLine($"Total Time Passed => " + DateTimeHelper.FormatHMS(timePassed));
//         Console.WriteLine($"Total Time Left   => " + DateTimeHelper.FormatHMS(timeLeft));
//         #endregion

//         string saveDirectory = Path.Combine(articlesDirectoryPath, article.Category);
//         string savePath = Path.Combine(saveDirectory, article.Id + ".txt");

//         if (!File.Exists(saveDirectory))
//         {
//             Directory.CreateDirectory(saveDirectory);
//         }

//         try
//         {
//             HtmlDocument dom = HtmlHelper.GetHtmlDom(article.PageUrl);
//             string content = dom.DocumentNode.SelectSingleNode("//div[@id='full-text']")?.InnerHtml;
//             if (!string.IsNullOrWhiteSpace(content))
//             {
//                 await File.WriteAllTextAsync(savePath, content);
//             }
//         }
//         catch (Exception ex)
//         {
//             await File.AppendAllTextAsync(errorMessageFilePath, DateTime.Now + " : " + Environment.NewLine + ex.Message);
//         }
//     });
// }

Console.WriteLine("---------------Over---------------");
