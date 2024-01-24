using System;
using System.Collections.Generic;

namespace MODEL.FilmInfo;
public partial class Film()
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ReleaseYear { get; set; }
    public int RateId { get; set; }
    public string Duration { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Description { get; set; }
}