using System.Security.Cryptography;
using System.Text;

namespace COMMON;

public class MD5Helper
{
    private const string salt = "filmweb.md5encrypt";

    #region Decrypt By MD5 +CreateHashMD5(string s)
    public static string CreateHashMD5(string s)
    {
        byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(s));
        string md5 = string.Empty;
        for (int i = 0; i < data.Length; i++)
        {
            md5 += data[i].ToString("x2").ToUpperInvariant();
        }
        return md5;
    }
    #endregion

    #region Decrypt By MD5 +EncryptText(string password, string salt)
    public static string EncryptText(string text)
    {
        text = CreateHashMD5(text);
        text = CreateHashMD5(text + salt);
        return text;
    }
    #endregion

}