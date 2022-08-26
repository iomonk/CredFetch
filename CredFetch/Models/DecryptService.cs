using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace CredFetch.Models;

public static class DecryptService
{
    public static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        try
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            using var aesAlg = new AesManaged {Key = key, IV = iv};
            var decrypt = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decrypt, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            var plaintext = srDecrypt.ReadToEnd();
            return plaintext;
        }
        catch
        {
            MessageBox.Show(Constants.WrongPass);
            Environment.Exit(0);
        }

        return string.Empty;
    }
}