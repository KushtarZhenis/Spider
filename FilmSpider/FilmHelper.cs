using System.Net;
using COMMON;
using HtmlAgilityPack;
using MODEL;
using MODEL.Film;
using MODEL.FilmInfo;

namespace FilmSpider;
public class FilmHelper
{
    public static void WriteErrorMsg(string msg)
    {
        File.AppendAllText("/Users/kushtar/Desktop/SpiderSources/Film/Errors.txt", msg + Environment.NewLine);
    }

    public static async Task<string> GetPageHtml(string url)
    {
        try
        {
            HtmlDocument dom = HtmlHelper.GetHtmlDom(url);
            var node = dom.DocumentNode.SelectSingleNode("//script");

            string[] strArr = node.InnerHtml[18..].Split('=');
            string cookieName = strArr[0][2..];
            string cookieValue = strArr[1][..strArr[1].IndexOf(';')];

            var cookieContainer = new CookieContainer();
            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler);

            cookieContainer.Add(new Uri(url), new Cookie(cookieName, cookieValue));
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                return res;
            }
            else return "";
        }
        catch (Exception ex)
        {
            WriteErrorMsg(ex.Message);
            return "";
        }
    }

    public static Film InsertFilm(PartialFilm pFilm, int id)
    {
        var rate = new Rate()
        {
            Id = id,
            Douban = pFilm.Rate.Douban,
            Imdb = pFilm.Rate.IMDB
        };
        var film = new Film()
        {
            Id = id,
            Title = pFilm.Title,
            ReleaseYear = pFilm.ReleaseYear,
            RateId = rate.Id,
            Duration = pFilm.Duration,
            ThumbnailUrl = pFilm.Duration,
            Description = pFilm.Description
        };
        foreach (string pAlias in pFilm.Alias)
        {
            var alias = new Filmaliasmap()
            {
                FilmId = film.Id,
                Alias = pAlias
            };
        }
        foreach (string pLanguage in pFilm.Language)
        {
            var language = new Filmlanguagemap()
            {
                FilmId = film.Id,
                FilmLanguage = pLanguage
            };
        }
        foreach (string pRegion in pFilm.RegionsOfRelease)
        {
            var language = new Filmregionmap()
            {
                FilmId = film.Id,
                RegionOfRelease = pRegion
            };
        }
        foreach (string pReleaseDate in pFilm.ReleaseDate)
        {
            var releaseDate = new Filmreleasedatemap()
            {
                FilmId = film.Id,
                Releasedate = pReleaseDate
            };
        }
        foreach (string pTag in pFilm.Tags)
        {
            var tag = new Filmtagmap()
            {
                FilmId = film.Id,
                TagTitle = pTag
            };
        }
        foreach (var pLink in pFilm.Links)
        {
            var link = new MODEL.FilmInfo.Link()
            {
                Type = pLink.Type
            };

            foreach (var pLinkField in pLink.Fields)
            {
                var linkField = new Linkfield()
                {
                    Id = film.Id,
                    Title = pLinkField.Title,
                    Size = pLinkField.Size,
                    Magnet = pLinkField.Magnet,
                    Torrent = pLinkField.Torrent
                };
            }

        }





    }

}