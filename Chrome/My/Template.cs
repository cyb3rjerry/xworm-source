using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace My
{
	// Token: 0x02000004 RID: 4
	[StandardModule]
	[HideModuleName]
	[GeneratedCode("MyTemplate", "14.0.0.0")]
	internal sealed class Template
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000024C8 File Offset: 0x000006C8
		[HelpKeyword("My.Computer")]
		internal static Computer Computer
		{
			[DebuggerHidden]
			get
			{
				return Template.Computer.GetInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000024E4 File Offset: 0x000006E4
		[HelpKeyword("My.Application")]
		internal static Application Application
		{
			[DebuggerHidden]
			get
			{
				return Template.Application.GetInstance;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002500 File Offset: 0x00000700
		[HelpKeyword("My.User")]
		internal static User User
		{
			[DebuggerHidden]
			get
			{
				return Template.User.GetInstance;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000251C File Offset: 0x0000071C
		[HelpKeyword("My.WebServices")]
		internal static Template.MyWebServices WebServices
		{
			[DebuggerHidden]
			get
			{
				return Template.WebService.GetInstance;
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly Template.ThreadSafeObjectProvider<Computer> Computer = new Template.ThreadSafeObjectProvider<Computer>();

		// Token: 0x04000002 RID: 2
		private static readonly Template.ThreadSafeObjectProvider<Application> Application = new Template.ThreadSafeObjectProvider<Application>();

		// Token: 0x04000003 RID: 3
		private static readonly Template.ThreadSafeObjectProvider<User> User = new Template.ThreadSafeObjectProvider<User>();

		// Token: 0x04000004 RID: 4
		private static readonly Template.ThreadSafeObjectProvider<Template.MyWebServices> WebService = new Template.ThreadSafeObjectProvider<Template.MyWebServices>();

		// Token: 0x02000005 RID: 5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
		internal sealed class MyWebServices
		{
			// Token: 0x06000016 RID: 22 RVA: 0x00002538 File Offset: 0x00000738
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002558 File Offset: 0x00000758
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002570 File Offset: 0x00000770
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			internal new Type GetType()
			{
				return typeof(Template.MyWebServices);
			}

			// Token: 0x06000019 RID: 25 RVA: 0x0000258C File Offset: 0x0000078C
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x0600001A RID: 26 RVA: 0x000025A4 File Offset: 0x000007A4
			[DebuggerHidden]
			private static T Create__Instance__<T>(T instance) where T : new()
			{
				T result;
				if (instance == null)
				{
					result = Activator.CreateInstance<T>();
				}
				else
				{
					result = instance;
				}
				return result;
			}

			// Token: 0x0600001B RID: 27 RVA: 0x000025C8 File Offset: 0x000007C8
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance)
			{
				instance = default(T);
			}

			// Token: 0x0600001C RID: 28 RVA: 0x000020EC File Offset: 0x000002EC
			[EditorBrowsable(EditorBrowsableState.Never)]
			[DebuggerHidden]
			public MyWebServices()
			{
			}
		}

		// Token: 0x02000006 RID: 6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ComVisible(false)]
		internal sealed class ThreadSafeObjectProvider<T> where T : new()
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001D RID: 29 RVA: 0x000025E4 File Offset: 0x000007E4
			internal T GetInstance
			{
				[DebuggerHidden]
				get
				{
					if (Template.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
					{
						Template.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = Activator.CreateInstance<T>();
					}
					return Template.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
				}
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000020EC File Offset: 0x000002EC
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public ThreadSafeObjectProvider()
			{
			}

			// Token: 0x04000005 RID: 5
			[CompilerGenerated]
			[ThreadStatic]
			private static T m_ThreadStaticValue;
		}
	}
}
