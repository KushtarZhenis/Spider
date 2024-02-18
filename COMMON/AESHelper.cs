using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace COMMON;
public class AESHelper
{
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string EncryptText(string input, string password)
    {
        byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        passwordBytes = SHA256.HashData(passwordBytes);
        byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
        string result = Convert.ToBase64String(bytesEncrypted);
        return result;
    }

    public static string DecryptText(string input, string password)
    {
        byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        passwordBytes = SHA256.HashData(passwordBytes);
        byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
        string result = Encoding.UTF8.GetString(bytesDecrypted);
        return result;
    }

    private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes = null;
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        using (var ms = new MemoryStream())
        {
#pragma warning disable SYSLIB0022 // Type or member is obsolete
            using var AES = new RijndaelManaged();
#pragma warning restore SYSLIB0022 // Type or member is obsolete

            AES.KeySize = 256;
            AES.BlockSize = 128;

#pragma warning disable SYSLIB0041 // Type or member is obsolete
            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
#pragma warning restore SYSLIB0041 // Type or member is obsolete

            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            AES.Mode = CipherMode.CBC;

            using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                cs.Close();
            }
            encryptedBytes = ms.ToArray();
        }
        return encryptedBytes;
    }

    private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes = null;
        byte[] saltBytes = [1, 2, 3, 4, 5, 6, 7, 8];

        using (var ms = new MemoryStream())
        {
#pragma warning disable SYSLIB0022 // Type or member is obsolete
            using var AES = new RijndaelManaged();
#pragma warning restore SYSLIB0022 // Type or member is obsolete

            AES.KeySize = 256;
            AES.BlockSize = 128;

#pragma warning disable SYSLIB0041 // Type or member is obsolete
            var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
#pragma warning restore SYSLIB0041 // Type or member is obsolete
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            AES.Mode = CipherMode.CBC;

            using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                cs.Close();
            }
            decryptedBytes = ms.ToArray();
        }
        return decryptedBytes;
    }
}
