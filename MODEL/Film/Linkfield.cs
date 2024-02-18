using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class LinkField()
{
    public uint Id { get; set; }
    public uint FilmId { get; set; }
    public string Title { get; set; }
    public string Size { get; set; }
    public string Type { get; set; }
    public string Magnet { get; set; }
    public string Torrent { get; set; }
    public uint AddTime { get; set; }
    public uint UpdateTime { get; set; }
    public byte QStatus { get; set; }
}