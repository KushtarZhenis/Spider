namespace MODEL.Film;

public class EmailSentLog
{
    public uint Id { get; set; }
    public string Email { get; set; }
    public string Ip { get; set; }
    public uint SentTime { get; set; }
    public byte QStatus { get; set; }
}