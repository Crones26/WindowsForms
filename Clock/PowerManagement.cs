using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Clock
{
    public static class PowerManagement
    {
        // Импорт функций из kernel32.dll
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

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        public static IntPtr SetWakeTimer(DateTime alarmTime)
        {
            // Преобразуем время в UTC
            DateTime utcTime = alarmTime.ToUniversalTime();
            long dueTime = utcTime.ToFileTime();

            // Создаем таймер
            IntPtr timerHandle = CreateWaitableTimer(IntPtr.Zero, true, "ClockWakeTimer");

            if (timerHandle == IntPtr.Zero)
            {
                throw new Exception("Не удалось создать таймер пробуждения.");
            }

            // Устанавливаем таймер пробуждения
            if (!SetWaitableTimer(timerHandle, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, true))
            {
                CloseHandle(timerHandle);
                throw new Exception("Не удалось установить таймер пробуждения.");
            }

            return timerHandle;
        }

        public static void CancelWakeTimer(IntPtr timerHandle)
        {
            if (timerHandle != IntPtr.Zero)
            {
                CancelWaitableTimer(timerHandle);
                CloseHandle(timerHandle);
            }
        }
    }
}