namespace MODEL.Film;

public class Person
{
    public uint Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public byte EmailConfirm { get; set; }
    public string Password { get; set; }
    public uint AddTime { get; set; }
    public uint UpdateTime { get; set; }
    public byte QStatus { get; set; }
}
