#r "System.Security.Cryptography"
#r "System.Text.Encoding"

using System;
using System.Security.Cryptography;
using System.Text;

string mw_mutexName = "BM0KFI37PL8w3qNr"; // Required for decryption key

string mw_IPAddress = "ZUJaQY41qR4AqdHYyAP+cA==";
string mw_socketPort = "FWd2q7oO16NdAEuty6JKYw==";
string mw_AESKey = "tmWHapH+yoFkBndjZ/Kolw==";
string mw_XwormTag = "z8aZlTt4+rgWWWZD6By+1w==";
string mw_malwareName = "eY4sJECpzuvmd2ufLAP2ug==";
string mw_driveInfectionFilename = "2fhTCLHIb4RZIBqb+s7wdQ==";
string mw_InstallPath = "WK+jcFdb8XTpo7pyCZgMgg==";
string mw_localFilename = "w4/hGV4jqpUOohi9FExK/w==";

Console.WriteLine("Decrypted Configuration:");

try
{
    // Decrypt and print each value
    PrintDecryptedValue("mw_IPAddress", mw_IPAddress, mw_mutexName);
    PrintDecryptedValue("mw_socketPort", mw_socketPort, mw_mutexName);
    PrintDecryptedValue("mw_AESKey", mw_AESKey, mw_mutexName);
    PrintDecryptedValue("mw_XwormTag", mw_XwormTag, mw_mutexName);
    PrintDecryptedValue("mw_malwareName", mw_malwareName, mw_mutexName);
    PrintDecryptedValue("mw_driveInfectionFilename", mw_driveInfectionFilename, mw_mutexName);
    PrintDecryptedValue("mw_InstallPath", mw_InstallPath, mw_mutexName);
    PrintDecryptedValue("mw_localFilename", mw_localFilename, mw_mutexName);
    Console.WriteLine($"mw_mutexName:{mw_mutexName}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error during decryption: {ex.Message}");
}

void PrintDecryptedValue(string key, string encryptedValue, string mutexName)
{
    if (!string.IsNullOrEmpty(encryptedValue))
    {
        string decryptedValue = DecryptValue(encryptedValue, mutexName);
        Console.WriteLine($"{key}:{decryptedValue}");
    }
    else
    {
        Console.WriteLine($"{key}: (No Value)");
    }
}

string DecryptValue(string encryptedValue, string mutexName)
{
    using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
    using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
    {
        byte[] keyArray = new byte[32];
        byte[] hash = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(mutexName));
        Array.Copy(hash, 0, keyArray, 0, 16);
        Array.Copy(hash, 0, keyArray, 15, 16);

        rijndaelManaged.Key = keyArray;
        rijndaelManaged.Mode = CipherMode.ECB;

        ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor();
        byte[] encryptedBytes = Convert.FromBase64String(encryptedValue);
        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}
