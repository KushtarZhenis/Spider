using COMMON;

string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SpiderSources/Videos");
await VideoHelper.StartDownloading(directoryPath);