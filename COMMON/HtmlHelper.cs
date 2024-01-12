using HtmlAgilityPack;

namespace COMMON;
public class HtmlHelper
{
    #region Get Html Document From Url +GetHtmlDom(string url)
    public static HtmlDocument GetHtmlDom(string url)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load(url);
        return document;
    }
    #endregion

    #region Get Html Document As String From Url +GetHtmlDomAsString(string url)
    public static string GetHtmlDomAsString(string url)
    {

        HtmlWeb web = new HtmlWeb();
        string document = web.Load(url).DocumentNode.OuterHtml;

        return document;
    }
    #endregion

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

    #region Edit All Media In A File +EditAllMedia(string filePath, string siteUrl)
    public static async Task EditAllMedia(string filePath, string siteUrl)
    {
        string html = File.ReadAllText(filePath);
        HtmlDocument dom = new HtmlDocument();
        dom.LoadHtml(html);
        HtmlNodeCollection imgs = dom.DocumentNode.SelectNodes("//img");
        HtmlNodeCollection audios = dom.DocumentNode.SelectNodes("//audio");
        HtmlNodeCollection videos = dom.DocumentNode.SelectNodes("//video");
        foreach (HtmlNode img in imgs)
        {
            string imgUrl = img.Attributes["src"]?.Value;
            if (!imgUrl.StartsWith("http")) imgUrl = siteUrl + (imgUrl.StartsWith('/') ? imgUrl : '/' + imgUrl);
            string extension = Path.GetExtension(imgUrl);
            string directoryPath = Path.GetDirectoryName(filePath);
            directoryPath = Path.GetDirectoryName(directoryPath);
            string partialPath = Path.Combine("images", DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension);
            string fullPath = Path.Combine(directoryPath, partialPath);
            await DownloadFile(fullPath, imgUrl);
            img.SetAttributeValue("src", "../" + partialPath);

            string newFilePath = filePath.Replace("_wait", "");
            await File.WriteAllTextAsync(newFilePath, dom.DocumentNode.OuterHtml);
            if (File.Exists(filePath)) File.Delete(filePath);
        }
        foreach (HtmlNode audio in audios)
        {
            string imgUrl = audio.Attributes["src"]?.Value;
            if (!imgUrl.StartsWith("http"))
            {
                imgUrl = siteUrl + (imgUrl.StartsWith('/') ? imgUrl : '/' + imgUrl);
            }
            string extension = Path.GetExtension(imgUrl);
            string directoryPath = Path.GetDirectoryName(filePath);
            string partialPath = Path.Combine("audios", DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension);
            string fullPath = Path.Combine(directoryPath, partialPath);
            await DownloadFile(fullPath, imgUrl);
            audio.SetAttributeValue("src", "../" + partialPath);

            string newFilePath = filePath.Replace("_wait", "");
            await File.WriteAllTextAsync(newFilePath, dom.DocumentNode.OuterHtml);
            if (File.Exists(filePath)) File.Delete(filePath);
        }
        foreach (HtmlNode video in videos)
        {
            string imgUrl = video.Attributes["src"]?.Value;
            if (!imgUrl.StartsWith("http"))
            {
                imgUrl = siteUrl + (imgUrl.StartsWith('/') ? imgUrl : '/' + imgUrl);
            }
            string extension = Path.GetExtension(imgUrl);
            string directoryPath = Path.GetDirectoryName(filePath);
            string partialPath = Path.Combine("videos", DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension);
            string fullPath = Path.Combine(directoryPath, partialPath);
            await DownloadFile(fullPath, imgUrl);
            video.SetAttributeValue("src", "../" + partialPath);

            string newFilePath = filePath.Replace("_wait", "");
            await File.WriteAllTextAsync(newFilePath, dom.DocumentNode.OuterHtml);
            if (File.Exists(filePath)) File.Delete(filePath);
        }
    }
    #endregion

}