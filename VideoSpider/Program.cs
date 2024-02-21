using COMMON;
using YoutubeExplode;
using System.Threading.Tasks;
using YoutubeExplode.Converter;
using System.Text.RegularExpressions;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Common;
using AngleSharp.Io.Dom;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

// await VideoHelper.StartDownloading(directoryPath);


string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string saveDirectory = PathHelper.Combine(desktopPath, "Sources/Mp3");
string videosDirectory = PathHelper.Combine(desktopPath, "Sources/Mp3Videos");
string errorLogPath = PathHelper.Combine(desktopPath, "Logs/Mp3Downloads/Mp3Downloads.txt");

// if (!Directory.Exists(videosDirectory)) Directory.CreateDirectory(videosDirectory);
// if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

// List<string> pathList = FileHelper.GetAllFilePath(videosDirectory);

// DateTimeHelper.StartPoint(pathList.Count);
// foreach (string inputFilePath in pathList)
// {
//     string outputFilePath = PathHelper.Combine(saveDirectory, Path.GetFileNameWithoutExtension(inputFilePath) + ".mp3");

//     try
//     {
//         var ffmpegProcess = new Process();
//         ffmpegProcess.StartInfo.FileName = "ffmpeg";
//         ffmpegProcess.StartInfo.Arguments = $"-i \"{inputFilePath}\" -vn -acodec libmp3lame -q:a 2 \"{outputFilePath}\"";
//         ffmpegProcess.StartInfo.RedirectStandardOutput = true;
//         ffmpegProcess.StartInfo.RedirectStandardError = true;
//         ffmpegProcess.StartInfo.UseShellExecute = false;
//         ffmpegProcess.StartInfo.CreateNoWindow = true;
//         ffmpegProcess.Start();

//         string output = ffmpegProcess.StandardOutput.ReadToEnd();
//         string error = ffmpegProcess.StandardError.ReadToEnd();

//         ffmpegProcess.WaitForExit();

//         if (!ffmpegProcess.HasExited)
//         {
//             ffmpegProcess.Kill();
//         }

//         DateTimeHelper.CalculatePointInLoop();
//         Console.WriteLine("Output: " + output);
//         Console.WriteLine("Error: " + error);
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"An error occurred: {ex.Message}");
//     }

//     // if (File.Exists(path)) File.Delete(path);
// }





// string playListId = "4PxW-GRpDhY";//RDyMBYDHDi-ak
string playListId = "RDyMBYDHDi-ak";

var youtube = new YoutubeClient();

var videoList = await youtube.Playlists.GetVideosAsync(playListId);
// DateTimeHelper.StartPoint(videoList.Count);

string filPath = "/Users/kushtar/Desktop/Github/Nodejs/Test/id.json";
List<string> idList = [];
foreach (var video in videoList)
{
    idList.Add(video.Id.ToString());
}
string content = JsonHelper.SerializeObject(idList);
File.WriteAllText(filPath, content);
Console.WriteLine("Finished");


// foreach (var video in videoList)
// {
//     try
//     {
//         var streams = await youtube.Videos.Streams.GetManifestAsync(video.Id);
//         var audioStreamInfo = streams.GetAudioOnlyStreams().Where(s => s.Container == Container.Mp4).GetWithHighestBitrate();

//         if (audioStreamInfo == null)
//         {
//             File.WriteAllText(errorLogPath, $"{video.Title} Audio Streams Not Found\n\n");
//         }
//         else
//         {
//             string savePath = PathHelper.Combine(videosDirectory, video.Title + '.' + Container.Mp4.Name);
//             Console.Clear();
//             DateTimeHelper.CalculatePointInLoop();
//             Console.WriteLine($"Downloading  =>  {video.Title}  {video.Duration}");
//             await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, savePath);
//         }
//     }
//     catch (Exception ex)
//     {
//         File.WriteAllText(errorLogPath, $"Error When Downloading {video.Title}  :  {ex.Message}\n\n");
//     }
// }

