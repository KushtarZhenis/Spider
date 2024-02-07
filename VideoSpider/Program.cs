using COMMON;
using YoutubeExplode;
using System.Threading.Tasks;
using YoutubeExplode.Converter;
using System.Text.RegularExpressions;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Common;

// string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sources/Videos");

// await VideoHelper.StartDownloading(directoryPath);



var youtube = new YoutubeClient();

// string videoId = "R3GfuzLMPkA"; // Replace with the actual video ID
string videoId = "EJr3uAQwGek"; // Replace with the actual video ID

var streams = await youtube.Videos.Streams.GetManifestAsync(videoId);
var video = await youtube.Videos.GetAsync(videoId);

Console.WriteLine(streams.Streams[2].Url);


var thumbnailUrls = video.Thumbnails;

string thumbnailUrl = thumbnailUrls.GetWithHighestResolution().Url;


var muxedStreams = streams.GetMuxedStreams();

var streamInfo1 = muxedStreams.Where(s => s.Container == Container.Mp4);

var streamInfo2 = streamInfo1.OrderByDescending(s => s.VideoQuality);

var streamInfo = streamInfo2.FirstOrDefault();



if (streamInfo != null)
{
    string mp4Url = streamInfo.Url;
    Console.WriteLine("MP4 URL: " + mp4Url);
}
else
{
    Console.WriteLine("No .mp4 stream found.");
}