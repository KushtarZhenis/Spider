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

List<string> pathList = FileHelper.GetAllFilePath(videosDirectory);
foreach (string path in pathList)
{
    string ffmpegPath = "ffmpeg";
    if (path.EndsWith(".mp4"))
    {
        
    }
    string outputFilePath = ;

    string arguments = $"-i \"{path}\" -vn -acodec libmp3lame -q:a 2 \"{outputFilePath}\"";
    ProcessStartInfo processInfo = new ProcessStartInfo(ffmpegPath, arguments)
    {
        CreateNoWindow = true,
        UseShellExecute = false
    };
    using (Process process = Process.Start(processInfo))
    {
        process.WaitForExit();
    }

    Console.WriteLine("Conversion complete.");
}





// string playListId = "RDyMBYDHDi-ak";


// var youtube = new YoutubeClient();

// var videoList = await youtube.Playlists.GetVideosAsync(playListId);
// DateTimeHelper.StartPoint(videoList.Count);

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
//             string savePath = PathHelper.Combine(saveDirectory, video.Title + '.' + Container.Mp4.Name);
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

