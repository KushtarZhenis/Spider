using HtmlAgilityPack;

namespace COMMON;
public class HtmlHelper
{
    public static HtmlDocument GetHtmlDom(string url)
    {

        HtmlWeb web = new HtmlWeb();
        HtmlDocument document = web.Load(url);

        return document;
    }

    public static string GetHtmlDomAsString(string url)
    {

        HtmlWeb web = new HtmlWeb();
        string document = web.Load(url).DocumentNode.OuterHtml;

        return document;
    }

    public static async Task DownloadFile(string savePath, string imageUrl)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(imageUrl);
            response.EnsureSuccessStatusCode();

            byte[] Bytes = await response.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes(savePath, Bytes);
        }
    }

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
            await File.WriteAllTextAsync(filePath, dom.DocumentNode.OuterHtml);
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
            await File.WriteAllTextAsync(filePath, dom.DocumentNode.OuterHtml);
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
            await File.WriteAllTextAsync(filePath, dom.DocumentNode.OuterHtml);
        }
    }

}