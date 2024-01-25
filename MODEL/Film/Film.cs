using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class Film()
{
    public string Description { get; set; }
    public string Duration { get; set; }
    public int Id { get; set; }
    public int RateId { get; set; }
    public string ReleaseYear { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Title { get; set; }
}