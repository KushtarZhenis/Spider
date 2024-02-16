using COMMON;
using YoutubeExplode;
using System.Threading.Tasks;
using YoutubeExplode.Converter;
using System.Text.RegularExpressions;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Common;
using AngleSharp.Io.Dom;

// string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sources/Videos");

// await VideoHelper.StartDownloading(directoryPath);

string filePath = "/Users/kushtar/Desktop/Github/CSharp/Spider/VideoSpider/mp3.json";
string content = File.ReadAllText(filePath).Replace("https://www.youtube.com/watch?v=", "");
List<string> idList = JsonHelper.DeSerializeObject<List<string>>(content);

var youtube = new YoutubeClient();

// string videoId = "R3GfuzLMPkA"; // Replace with the actual video ID
// string videoId = "EJr3uAQwGek"; // Replace with the actual video ID

foreach (string videoId in idList)
{
    var streams = await youtube.Videos.Streams.GetManifestAsync(videoId);
    var muxedStreams = streams.GetAudioStreams();

    

}

