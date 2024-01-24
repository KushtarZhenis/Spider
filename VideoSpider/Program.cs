using COMMON;

string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Sources/Videos");

await VideoHelper.StartDownloading(directoryPath);