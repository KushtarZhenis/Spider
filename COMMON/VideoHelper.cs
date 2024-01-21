using YoutubeExplode;
using YoutubeExplode.Converter;

namespace COMMON;
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
        if (File.Exists(savePath))
        {
            DateTimeHelper.ResetPoint();
            return;
        }
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

    #region Download Main Task +StartDownloading(string saveDirectory) 
    public static async Task StartDownloading(string saveDirectory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter a Valid Video Or Playlist Url: " + Environment.NewLine);
            string url = Console.ReadLine();
            byte urlType = 0;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    bool isValidUrl = response.IsSuccessStatusCode;
                    if (isValidUrl) urlType = 1;
                }
            }
            catch (HttpRequestException) { urlType = 0; }

            if (urlType != 0 && url.Contains("playlist?list="))
            {
                urlType = 2;
                Console.WriteLine("This Url Is a Playlist Url!");
                Console.WriteLine("Start, Another Url Or Exit (Start/AnotherUrl/AnyKey): ");
            }
            else if (urlType != 0)
            {
                Console.WriteLine("This Url Is a Video Url!");
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
    #endregion

}