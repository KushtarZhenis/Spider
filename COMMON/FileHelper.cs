namespace COMMON;

public class FileHelper
{
    public static async Task AppendLine(string filePath, string content)
    {
        await File.AppendAllTextAsync(filePath, content + Environment.NewLine);
    }

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
}