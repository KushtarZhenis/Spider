using System.Diagnostics;

namespace COMMON;

public class FileHelper
{

    #region Download File By Url +Download(string filPath, string url)
    public static async Task Download(string filPath, string url)
    {
        using var client = new HttpClient();
        using var stream = await client.GetStreamAsync(new Uri(url));
        using var fs = new FileStream(filPath, FileMode.OpenOrCreate, FileAccess.Write);
        await stream.CopyToAsync(fs);
    }
    #endregion

    #region Download File By Url +DownloadFile(string savePath, string fileUrl)
    public static async Task DownloadFile(string savePath, string fileUrl)
    {
        using var client = new HttpClient();

        var response = await client.GetAsync(fileUrl);
        response.EnsureSuccessStatusCode();
        byte[] Bytes = await response.Content.ReadAsByteArrayAsync();

        File.WriteAllBytes(savePath, Bytes);
    }
    #endregion

    #region Append A Line To File +AppendLine(string filePath, string content)
    public static void AppendLine(string filePath, string content)
    {
        File.AppendAllText(filePath, content + Environment.NewLine);
    }
    #endregion

    #region Get All File Path In A Directory +GetAllFilePath(string pDirectoryPath)
    public static List<string> GetAllFilePath(string pDirectoryPath)
    {
        List<string> res = [];

        foreach (string filePath in Directory.GetFiles(pDirectoryPath))
        {
            res.Add(filePath);
        }
        foreach (string directoryPath in Directory.GetDirectories(pDirectoryPath))
        {
            res.AddRange(GetAllFilePath(directoryPath));
        }

        return res;
    }
    #endregion

    #region Convert Audio From Mp4 File To Mp3 File +ConvertMp4ToMp3(string sourcePath, string destinationPath, out string output, out string error)
    public static void ConvertMp4ToMp3(string sourcePath, string destinationPath, out string output, out string error)
    {
        var ffmpegProcess = new Process();
        ffmpegProcess.StartInfo.FileName = "ffmpeg";
        ffmpegProcess.StartInfo.Arguments = $"-i \"{sourcePath}\" -vn -acodec libmp3lame -q:a 2 \"{destinationPath}\"";
        ffmpegProcess.StartInfo.RedirectStandardOutput = true;
        ffmpegProcess.StartInfo.RedirectStandardError = true;
        ffmpegProcess.StartInfo.UseShellExecute = false;
        ffmpegProcess.StartInfo.CreateNoWindow = true;
        ffmpegProcess.Start();

        output = ffmpegProcess.StandardOutput.ReadToEnd();
        error = ffmpegProcess.StandardError.ReadToEnd();
        ffmpegProcess.WaitForExit();
        if (!ffmpegProcess.HasExited)
        {
            ffmpegProcess.Kill();
        }
    }
    #endregion

}