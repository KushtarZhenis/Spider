namespace COMMON;

public class Loginlog
{
    public uint Id { get; set; }
    public uint PersonId { get; set; }
    public string Email { get; set; }
    public string IP { get; set; }
    public uint LoginTime { get; set; }
    public byte QStatus { get; set; }
}