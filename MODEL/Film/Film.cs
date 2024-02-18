using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class Film()
{
    public string Description { get; set; }
    public string Duration { get; set; }
    public uint Id { get; set; }
    public string Url { get; set; }
    public uint RateId { get; set; }
    public string ReleaseYear { get; set; }
    public uint AddTime { get; set; }
    public uint UpdateTime { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Title { get; set; }
    public byte QStatus { get; set;}
}