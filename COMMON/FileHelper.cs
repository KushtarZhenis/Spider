namespace COMMON;

public class FileHelper
{

    #region Download File From Url +DownloadFile(string savePath, string fileUrl)
    public static async Task DownloadFile(string savePath, string fileUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(fileUrl);
            response.EnsureSuccessStatusCode();

            byte[] Bytes = await response.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes(savePath, Bytes);
        }
    }
    #endregion

    #region Append A Line To File +AppendLine(string filePath, string content)
    public static async Task AppendLine(string filePath, string content)
    {
        await File.AppendAllTextAsync(filePath, content + Environment.NewLine);
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

}