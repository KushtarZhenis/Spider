using System;
using System.Collections.Generic;

namespace MODEL.FilmInfo;
public partial class Filmregionmap()
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public string RegionOfRelease { get; set; }
}