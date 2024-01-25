using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class Linkfield()
{
    public int Id { get; set; }
    public string Magnet { get; set; }
    public string Size { get; set; }
    public string Title { get; set; }
    public string Torrent { get; set; }
    public string Type { get; set; }
    public int FilmId { get; set; }
}