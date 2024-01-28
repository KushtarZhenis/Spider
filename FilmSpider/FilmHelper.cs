using System.Net;
using COMMON;
using Dapper;
using DBHelper;
using HtmlAgilityPack;
using MODEL;
using MODEL.Film;
using MODEL.JsonFilm;

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

    public static void InsertFilm(PartialFilm pFilm, int id, MySql.Data.MySqlClient.MySqlConnection _connection)
    {
        // var rate = new Rate()
        // {
        //     Douban = pFilm.Rate.Douban,
        //     Imdb = pFilm.Rate.IMDB
        // };
        // _connection.Insert(rate);
        // int rateId = _connection.Query<int>("SELECT id FROM rate WHERE id = LAST_INSERT_ID();").FirstOrDefault();

        // var film = new Film()
        // {
        //     Title = pFilm.Title,
        //     ReleaseYear = pFilm.ReleaseYear,
        //     RateId = rateId,
        //     Duration = pFilm.Duration,
        //     ThumbnailUrl = pFilm.ThumbnailUrl,
        //     Description = pFilm.Description
        // };
        // _connection.Insert(film);
        // int filmId = _connection.Query<int>("SELECT id FROM rate WHERE id = LAST_INSERT_ID();").FirstOrDefault();

        // foreach (string pAlias in pFilm.Alias)
        // {
        //     var alias = new Filmaliasmap()
        //     {
        //         FilmId = filmId,
        //         Alias = pAlias
        //     };
        //     _connection.Insert(alias);
        // }

        // foreach (string pLanguage in pFilm.Language)
        // {
        //     var language = new Filmlanguagemap()
        //     {
        //         FilmId = filmId,
        //         FilmLanguage = pLanguage
        //     };
        //     _connection.Insert(language);
        // }

        // foreach (string pRegion in pFilm.RegionsOfRelease)
        // {
        //     var regions = new Filmregionmap()
        //     {
        //         FilmId = filmId,
        //         RegionOfRelease = pRegion
        //     };
        //     _connection.Insert(regions);
        // }

        // foreach (string pReleaseDate in pFilm.ReleaseDate)
        // {
        //     var releaseDate = new Filmreleasedatemap()
        //     {
        //         FilmId = filmId,
        //         Releasedate = pReleaseDate
        //     };
        //     _connection.Insert(releaseDate);
        // }

        // foreach (string pTag in pFilm.Tags)
        // {
        //     var tag = new Filmtagmap()
        //     {
        //         FilmId = filmId,
        //         TagTitle = pTag
        //     };
        //     _connection.Insert(tag);
        // }

        int filmId = _connection.Query<int>("select id from film where title = @Title and description = @Description", new { Title = pFilm.Title, Description = pFilm.Description }).FirstOrDefault();

        foreach (var pLink in pFilm.Links)
        {
            foreach (var pLinkField in pLink.Fields)
            {
                var linkField = new Linkfield()
                {
                    Title = pLinkField.Title,
                    Size = pLinkField.Size,
                    Magnet = pLinkField.Magnet,
                    Torrent = pLinkField.Torrent,
                    Type = pLink.Type,
                    FilmId = filmId
                };
                _connection.Insert(linkField);
                // int fieldId = _connection.Query<int>("SELECT id FROM linkfieldmap WHERE id = LAST_INSERT_ID();").FirstOrDefault();
            }
        }
    }
}