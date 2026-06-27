using System;
using System.Runtime.InteropServices;

namespace hahahalib
{
    /// <summary>
    /// Windows 計時器佇列 API 的薄封裝。
    /// 子類別通常覆寫 <see cref="Handle"/> 來接收計時器觸發事件。
    /// </summary>
    public class hahaha_thread_pool_queue_timer
    {
        // Win32 計時器佇列相關 API。
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

        /// <summary>
        /// 由 CreateTimerQueueTimer 回傳的原生計時器控制代碼。
        /// </summary>
        public IntPtr Timer_;

        public hahaha_thread_pool_queue_timer()
        {
            Reset();
        }
        /// <summary>
        /// 重設包裝類別狀態，但不主動釋放原生計時器。
        /// 若已建立計時器，應先呼叫 <see cref="Close"/>。
        /// </summary>
        public int Reset()
        {
            Timer_ = IntPtr.Zero;

            return new
            {
                value = 0
            }.value;
        }

        /// <summary>
        /// 供原生計時器佇列回呼轉接到實例方法的靜態橋接函式。
        /// </summary>
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

        /// <summary>
        /// 計時器觸發時呼叫，應由子類別覆寫。
        /// </summary>
        public virtual int Handle()
        {
            return 0;
        }

        /// <summary>
        /// 建立計時器佇列計時器。
        /// <paramref name="dueTimer"/> 為首次觸發延遲，<paramref name="period"/> 為週期，單位皆為毫秒。
        /// </summary>
        public virtual int Create(uint dueTimer, uint period)
        {
            // 只要原生端還可能回呼，就要保證 managed 物件不被 GC 回收。
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

        /// <summary>
        /// 更新既有計時器的下一次觸發時間與週期。
        /// </summary>
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

        /// <summary>
        /// 若計時器存在，刪除其原生控制代碼。
        /// </summary>
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
