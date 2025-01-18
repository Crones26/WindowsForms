using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Clock
{
	public class PowerManagement
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateWaitableTimer(IntPtr lpTimerAttributes, bool bManualReset, string lpTimerName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool SetWaitableTimer(
			IntPtr hTimer,
			[In] ref long lpDueTime,
			int lPeriod,
			IntPtr pfnCompletionRoutine,
			IntPtr lpArgToCompletionRoutine,
			bool fResume);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CancelWaitableTimer(IntPtr hTimer);

		public static void SetWakeTimer(DateTime alarmTime)
		{
			try
			{
				DateTime wakeTime = alarmTime.AddSeconds(-30);
				long dueTime = wakeTime.ToFileTime();

				IntPtr timer = CreateWaitableTimer(IntPtr.Zero, true, "WakeTimer");
				if (timer == IntPtr.Zero)
				{
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}

				if (!SetWaitableTimer(timer, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, true))
				{
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
				}

				Console.WriteLine($"Wake timer set for: {wakeTime}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error setting wake timer: {ex.Message}");
			}
		}


	}
}