using COMMON;
using YoutubeExplode;
using YoutubeExplode.Converter;

namespace Spider;
public class VideoHelper
{
    private static YoutubeClient Youtube { get; } = new YoutubeClient();

    #region Download Video On Youtube +DownloadVideo(string saveDirectory, string videoUrl)
    public static async Task DownloadVideo(string saveDirectory, string videoUrl)
    {
        var youtube = new YoutubeClient();
        var video = await youtube.Videos.GetAsync(videoUrl);

        var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(video.Id);
        var availableStreams = streamInfoSet.GetMuxedStreams().OrderByDescending(s => s.VideoQuality);
        var selectedStream = availableStreams.FirstOrDefault();

        if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

        string savePath = $"{saveDirectory}/{video.Title}.{selectedStream.Container}";
        if (File.Exists(savePath)) return;
        Console.WriteLine($"Dowloading: {video.Title}");
        await youtube.Videos.DownloadAsync(videoUrl, savePath);
    }
    #endregion

    #region Download Videos On PlayList +DownloadPlayList(string saveDirectory, string playlistUrl)
    public static async Task DownloadPlayList(string saveDirectory, string playlistUrl)
    {
        IAsyncEnumerable<YoutubeExplode.Common.Batch<YoutubeExplode.Playlists.PlaylistVideo>> videos = Youtube.Playlists.GetVideoBatchesAsync(playlistUrl);

        List<string> urlList = [];

        await foreach (var batch in videos)
        {
            foreach (var video in batch.Items)
            {
                urlList.Add(video.Url);
            }
        }

        DateTimeHelper.StartPoint(urlList.Count);

        foreach (string url in urlList)
        {
            Console.Clear();
            DateTimeHelper.CalculatePointInLoop();
            await DownloadVideo(saveDirectory, url);
        }
    }
    #endregion

    public static async Task StartDownloading(string saveDirectory)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;

        while (true)
        {
            Console.WriteLine("Enter a Valid Video Or Playlist Url: " + Environment.NewLine);
            string url = Console.ReadLine();
            byte urlType = 0;
            bool isValidUrl = false;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    isValidUrl = response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException)
            {
                isValidUrl = false;
            }

            if (isValidUrl && url.Contains("https://www.youtube.com/watch?v="))
            {

                urlType = 1;
                Console.WriteLine("This Url Is Video Url!");
                Console.WriteLine("Start, Another Url Or Exit (Start/AnotherUrl/AnyKey): ");
            }
            else if (isValidUrl && url.Contains("https://www.youtube.com/playlist?list="))
            {
                urlType = 2;
                Console.WriteLine("This Url Is Playlist Url!");
                Console.WriteLine("Start, Another Url Or Exit (Start/AnotherUrl/AnyKey): ");
            }
            else
            {
                Console.WriteLine("This Url Is Invalid!");
                Console.WriteLine("Another Url Or Exit (AnotherUrl/AnyKey): ");
            }

            Console.WriteLine();
            string key = Console.ReadLine();

            if (urlType == 0)
            {
                if (string.Equals(key, "AnotherUrl", StringComparison.OrdinalIgnoreCase)) continue;
                else break;
            }
            else
            {
                if (string.Equals(key, "Start", StringComparison.OrdinalIgnoreCase))
                {
                    if (urlType == 1) await DownloadVideo(saveDirectory, url);
                    else if (urlType == 2) await DownloadPlayList(saveDirectory, url);
                    else Console.WriteLine("Somthing Went Wrong!");
                }
                else if (string.Equals(key, "AnotherUrl", StringComparison.OrdinalIgnoreCase)) continue;
                else break;
            }
        }
    }
}