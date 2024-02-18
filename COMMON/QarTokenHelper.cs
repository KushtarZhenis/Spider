using System.Net;
using MODEL.FormatModels;

namespace COMMON;
public class QarTokenHelper
{
    private static readonly string password = "QarTokenEncryptPass";
    public static string EncryptKey(TokenInfoModel item)
    {
        string tokenInfoStr = JsonHelper.SerializeObject(item);
        string encryptedKey = AESHelper.EncryptText(tokenInfoStr, password);
        encryptedKey = AESHelper.Base64Encode(encryptedKey);
        return WebUtility.UrlEncode(encryptedKey);
    }

    public static TokenInfoModel DecryptKey(string encryptedKey)
    {
        try
        {
            encryptedKey = WebUtility.UrlDecode(encryptedKey);
            encryptedKey = AESHelper.Base64Decode(encryptedKey);
            string decryptedKey = AESHelper.DecryptText(encryptedKey, password);
            return JsonHelper.DeSerializeObject<TokenInfoModel>(decryptedKey);
        }
        catch
        {
            return null;
        }
    }
}