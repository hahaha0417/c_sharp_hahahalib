using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace hahahalib
{
    /// <summary>
    /// 用於次毫秒等待的混合式休眠工具。
    /// 以增加少量 CPU 使用率換取比一般 Thread.Sleep 更好的時間精度。
    /// </summary>
    public class hahaha_sleep
    {
        // 策略：
        // 1. 先用 Sleep(0) 讓出 CPU，避免整段時間都忙等。
        // 2. 透過較粗粒度的 SpinWait 接近目標時間。
        // 3. 最後用短暫忙等補齊精度。

        /// <summary>
        /// 以混合式 yield / spin 策略等待指定毫秒數。
        /// </summary>
        public void Sleep_Ms(double ms)
        {
            long freq_ = Stopwatch.Frequency;
            long ticks_ = (long)(freq_ * (ms / 1000.0)); // 毫秒轉計時刻度
            long start_ = Stopwatch.GetTimestamp();
            long target_ = start_ + ticks_;

            Thread.Sleep(0);

            long threshold_ = freq_ / 20000; // 約 50 微秒

            while (true)
            {
                long now_ = Stopwatch.GetTimestamp();
                long remaining_ = target_ - now_;

                if (remaining_ <= threshold_)
                {
                    break;
                }

                Thread.SpinWait(50);
            }

            while (Stopwatch.GetTimestamp() < target_)
            {
            }
        }

        /// <summary>
        /// 以相同混合策略等待指定微秒數。
        /// </summary>
        public void Sleep_Us(double us)
        {
            long freq_ = Stopwatch.Frequency;
            long ticks_ = (long)(freq_ * (us / 1_000_000.0)); // 微秒轉計時刻度
            long start_ = Stopwatch.GetTimestamp();
            long target_ = start_ + ticks_;

            Thread.Sleep(0);

            long threshold_ = freq_ / 20000; // 約 50 微秒

            while (true)
            {
                long now_ = Stopwatch.GetTimestamp();
                long remaining_ = target_ - now_;

                if (remaining_ <= threshold_)
                {
                    break;
                }

                Thread.SpinWait(50);
            }

            while (Stopwatch.GetTimestamp() < target_)
            {
            }
        }
    }
}
