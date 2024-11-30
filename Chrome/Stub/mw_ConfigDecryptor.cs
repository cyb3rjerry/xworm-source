using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Stub
{
	// Token: 0x02000011 RID: 17
	public class mw_ConfigDecryptor
	{
		// Token: 0x060000AF RID: 175 RVA: 0x0000633C File Offset: 0x0000453C
		public static object mw_decryptconfigValueFromAES([In] string value)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = new byte[32];
			byte[] sourceArray = md5CryptoServiceProvider.ComputeHash(mw_Handler.mw_stringToBytes(mw_config.mw_mutexName));
			Array.Copy(sourceArray, 0, array, 0, 16);
			Array.Copy(sourceArray, 0, array, 15, 16);
			rijndaelManaged.Key = array;
			rijndaelManaged.Mode = CipherMode.ECB;
			ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
			byte[] array2 = Convert.FromBase64String(value);
			return mw_Handler.mw_convertByteArrayToString(cryptoTransform.TransformFinalBlock(array2, 0, array2.Length));
		}
	}
}
