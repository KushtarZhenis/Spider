using System;
using System.Collections.Generic;

namespace MODEL.Film;
public partial class Rate()
{
    public string Douban { get; set; }
    public uint Id { get; set; }
    public string Imdb { get; set; }
    public uint AddTime { get; set; }
    public uint UpdateTime { get; set; }
    public byte QStatus { get; set; }
}