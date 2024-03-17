using System.Diagnostics;
using COMMON;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;


string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string mp3Directory = PathHelper.Combine(desktopPath, "Sources/Mp3/Mp3en");
string deleteDirectory = PathHelper.Combine(desktopPath, "Sources/Mp3/Delete");
string notFinishedPath = PathHelper.Combine(desktopPath, "Sources/Mp3/notfinished.json");
string errorLog = PathHelper.Combine(desktopPath, "Logs/Mp3Downloads");

if (!Directory.Exists(mp3Directory)) Directory.CreateDirectory(mp3Directory);
if (!Directory.Exists(errorLog)) Directory.CreateDirectory(errorLog);


List<string> deletePathList = FileHelper.GetAllFilePath(deleteDirectory);
List<string> mp3PathList = FileHelper.GetAllFilePath(mp3Directory);
List<string> errorList = [];
int count = 0;
int errorCount = 0;

foreach (string deletePath in deletePathList)
{
    try
    {
        string mp3Path = deletePath.Replace("/Sources/Mp3/Delete/", "/Sources/Mp3/Mp3en/");

        if (File.Exists(mp3Path))
        {
            File.Delete(mp3Path);
            count++;
        }
        else
        {
            errorList.Add(mp3Path);
        }
    }
    catch
    {
        errorCount++;
    }
}

Console.WriteLine(count);
Console.WriteLine(errorCount);
Console.WriteLine(errorList.Count);
File.AppendAllText(notFinishedPath, JsonHelper.SerializeObject(errorList));













// var youtube = new YoutubeClient();

// string listId = "RDIcrbM1l_BoI";
// var videoList = await youtube.Playlists.GetVideosAsync(listId);

// int errorCount = 0;

// var videos = await youtube.Channels.GetUploadsAsync("UCCSGhaYqvTLyQ4Jn-PSVT9A");

// double duration = 0;

// foreach (var video in videos)
// {
//     duration += video.Duration?.TotalHours ?? 0;
// }

// Console.WriteLine($"{duration} Hours");
// Console.WriteLine($"{duration / 24} Days");













// DateTimeHelper.StartPoint(idList.Count);
// foreach (var id in idList)
// {
//     try
//     {
//         var video = await youtube.Videos.GetAsync(id);
//         DateTimeHelper.CalculatePointInLoop();
//         Console.WriteLine($"Downloading: {video.Title} || {video.Duration}");
//         Console.WriteLine($"Error => {errorCount}");
//         string filePath = PathHelper.Combine(mp3Directory, $"{video.Title}.mp3");

//         var streams = await youtube.Videos.Streams.GetManifestAsync(video.Id);

//         var stream = streams.GetAudioOnlyStreams().OrderByDescending(s =>
//         s.Bitrate.BitsPerSecond).FirstOrDefault();

//         ConversionRequest conversionRequest = new(
//             "/opt/homebrew/bin/ffmpeg",
//             filePath,
//             Container.Mp3,
//             ConversionPreset.VerySlow
//         );

//         await youtube.Videos.DownloadAsync([stream], conversionRequest);
//     }
//     catch (Exception ex)
//     {
//         errorCount++;
//         FileHelper.AppendLine(errorLogPath, $"In Video ${id} => {ex.Message}");
//     }
// }




// await VideoHelper.StartDownloading(directoryPath);

