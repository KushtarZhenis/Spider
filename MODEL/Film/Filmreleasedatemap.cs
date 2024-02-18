using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class FilmReleaseDateMap()
{
    public uint FilmId { get; set; }
    public uint Id { get; set; }
    public string Releasedate { get; set; }
    public uint AddTime { get; set; }
    public uint UpdateTime { get; set; }
    public byte QStatus { get; set; }
}