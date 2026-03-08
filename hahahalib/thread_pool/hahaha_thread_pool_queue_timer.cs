using System;
using System.Runtime.InteropServices;

namespace hahahalib
{
    public class hahaha_thread_pool_queue_timer
    {
        // WinAPI imports
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CreateTimerQueueTimer(
            out IntPtr newTimer,
            IntPtr timerQueue,
            Timer_Callback_Delegate callback,
            IntPtr parameter,
            uint dueTime,
            uint period,
            uint flags
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ChangeTimerQueueTimer(
            IntPtr timerQueue,
            IntPtr timer,
            uint dueTime,
            uint period
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DeleteTimerQueueTimer(
            IntPtr timerQueue,
            IntPtr timer,
            IntPtr completionEvent
        );

        public delegate void Timer_Callback_Delegate(IntPtr parameter, bool timer_or_wait_fired);

        public IntPtr Timer_;

        public hahaha_thread_pool_queue_timer()
        {
            Reset();
        }

   

        public int Reset()
        {
            Timer_ = IntPtr.Zero;

            return new
            {
                value = 0
            }.value;
        }

     

        public static void TimerCallback(IntPtr parameter, bool timerOrWaitFired)
        {
            if (!timerOrWaitFired)
            {
                return;
            }

            hahaha_thread_pool_queue_timer timer_ =
                (hahaha_thread_pool_queue_timer)GCHandle.FromIntPtr(parameter).Target!;

            timer_.Handle();
        }

        public virtual int Handle()
        {
            return 0;
        }

        public virtual int Create(uint dueTimer, uint period)
        {
            GCHandle handle_ = GCHandle.Alloc(this);

            CreateTimerQueueTimer(
                out Timer_,
                IntPtr.Zero,
                TimerCallback,
                GCHandle.ToIntPtr(handle_),
                dueTimer,
                period,
                0
            );

            return 0;
        }

        public virtual int ChangeTimer(uint dueTimer, uint period)
        {
            ChangeTimerQueueTimer(
                IntPtr.Zero,
                Timer_,
                dueTimer,
                period
            );

            return 0;
        }

        public virtual int Close()
        {
            if (Timer_ != IntPtr.Zero)
            {
                DeleteTimerQueueTimer(
                    IntPtr.Zero,
                    Timer_,
                    IntPtr.Zero
                );

                Timer_ = IntPtr.Zero;
            }

            return 0;
        }

        
    }
}
