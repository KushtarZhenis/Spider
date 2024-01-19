using COMMON;
using MODEL;
using SongSpider;

string siteUrl = "https://www.ankui.kz";
// string postUrl = "https://www.ankui.kz/kz/GetMusicInfo";
// string songHomePageUrl = "https://www.ankui.kz/kz/ander";
string songsFilePath = "/Users/kushtar/Desktop/SpiderSources/AnkuiKz/Songs.json";
string mp3DirectoryPath = "/Users/kushtar/Desktop/SpiderSources/AnkuiKZ/mp3";
string errorFilePath = "/Users/kushtar/Desktop/SpiderSources/AnkuiKZ/Errors.txt";
Console.ForegroundColor = ConsoleColor.Green;


string content = File.ReadAllText(songsFilePath);
List<Song> songList = JsonHelper.DeSerializeObject<List<Song>>(content);

DateTimeHelper.StartPoint(songList.Count);

foreach (Song song in songList)
{
    string url = song.Data.Url;
    string fileName = Path.GetFileName(url);
    string savePath = Path.Combine(mp3DirectoryPath, fileName);
    string downUrl = siteUrl + url;
    if (File.Exists(savePath)) continue;

    try
    {
        await FileHelper.DownloadFile(savePath, downUrl);
    }
    catch (Exception ex)
    {
        File.AppendAllText(errorFilePath, $"Downloading File - {downUrl}: {ex.Message}{Environment.NewLine}");
    }
    DateTimeHelper.CalculatePointInLoop();
}




// for (int pageIdx = 1; pageIdx <= totalPage; pageIdx++)
// {
//     string pageUrl = Path.Combine(songHomePageUrl, pageIdx + ".html");
//     List<string> songIdList = SongHelper.GetSongIdList(pageUrl);
//     List<Song> songList = [];

//     foreach (string songId in songIdList)
//     {
//         string jsonString = "{\"musicId\":\"" + songId + "\",\"language\":\"kz\"}";
//         string content = await SongHelper.SendPostRequest(postUrl, jsonString);
//         if (string.IsNullOrEmpty(content)) continue;
//         Song song = JsonHelper.DeSerializeObject<Song>(content);
//         songList.Add(song);
//     }
//     string text = JsonHelper.SerializeObject(songList).Trim('[').Trim(']') + ",";
//     File.AppendAllText(songsFilePath, text);


//     DateTimeHelper.CalculatePointInLoop();
// }
