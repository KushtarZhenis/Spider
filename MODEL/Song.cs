namespace MODEL;

public class Song
{
    public string Id { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
    public string backUrl { get; set; }
    public Data Data { get; set; }

}
public class Data
{
    public string Url { get; set; }
    public string Lyric { get; set; }
}