using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class FilmAliasMap()
{
    public string Alias { get; set; }
    public uint FilmId { get; set; }
    public uint Id { get; set; }
    public uint AddTime { get; set; }
    public uint UpdateTime { get; set; }
}