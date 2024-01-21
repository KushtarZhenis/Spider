using COMMON;

string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Mary/Videos");

await VideoHelper.StartDownloading(directoryPath);