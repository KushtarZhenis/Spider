using System.Diagnostics;
using COMMON;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;


string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string mp3Directory = PathHelper.Combine(desktopPath, "Sources/Mp3");
//  string videosDirectory = PathHelper.Combine(desktopPath, "Sources/Mp3Videos"); 
string errorLogPath = PathHelper.Combine(desktopPath, "Logs/Mp3Downloads/Mp3Downloads.txt");
//  if (!Directory.Exists(videosDirectory)) Directory.CreateDirectory(videosDirectory); 
if (!Directory.Exists(mp3Directory)) Directory.CreateDirectory(mp3Directory);


// string sourcePath = PathHelper.Combine(mp3Directory, "Wide Awake feat Kianna - Giulio Cercato __Lyrics__.mp4");
// string destinationPath = sourcePath[..^4] + ".mp3";

// string listId = "RDIcrbM1l_BoI";
// var videoList = await youtube.Playlists.GetVideosAsync(listId);

// int errorCount = 0;

var youtube = new YoutubeClient();

var videos = await youtube.Channels.GetUploadsAsync("UCCSGhaYqvTLyQ4Jn-PSVT9A");

double duration = 0;

foreach (var video in videos)
{
    duration += video.Duration?.TotalHours ?? 0;
}

Console.WriteLine($"{duration} Hours");
Console.WriteLine($"{duration / 24} Days");










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

