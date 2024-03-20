using System.Diagnostics;
using COMMON;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;


string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string videoDirectory = PathHelper.Combine(desktopPath, "Sources/Videos/");
string errorLog = PathHelper.Combine(desktopPath, "Logs/Mp3Downloads");

if (!Directory.Exists(videoDirectory)) Directory.CreateDirectory(videoDirectory);
if (!Directory.Exists(errorLog)) Directory.CreateDirectory(errorLog);

await VideoHelper.DownloadVideo(videoDirectory, "https://youtube.com");













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

