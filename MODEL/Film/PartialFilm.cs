namespace MODEL.Film;

public class PartialFilm
{
    public string Title { get; set; }
    public string ReleaseYear { get; set; }
    public Rates Rate { get; set; }
    public string[] Alias { get; set; }
    public string[] ReleaseDate { get; set; }
    public string[] Language { get; set; }
    public string Duration { get; set; }
    public string[] RegionsOfRelease { get; set; }
    public string ThumbnailUrl { get; set; }
    public string[] Tags { get; set; }
    public string Description { get; set; }
    public Link[] Links { get; set; }
    public void Display()
    {
        Console.WriteLine($"Title        => {Title}");
        Console.WriteLine($"ReleaseYear  => {ReleaseYear}");
        Console.WriteLine($"Duration     => {Duration}");
        Console.WriteLine($"ThumbnailUrl => {ThumbnailUrl}");
        Console.WriteLine($"Description  => {Description}");
    }
}