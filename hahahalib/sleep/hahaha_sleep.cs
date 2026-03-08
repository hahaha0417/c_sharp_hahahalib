using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace hahahalib
{
    public class hahaha_sleep
    {
        //1. Sleep(0) 讓出 CPU	0%～5%
        //2. 粗 SpinWait（SpinWait(50)）	間歇忙等	10%～40%
        //3. 最後 50µs 純忙等  精準 busy-wait	100%（僅 50µs）
        
        // Sleep_Ms(1) 1ms
        public void Sleep_Ms(double ms)
        {
            long freq_ = Stopwatch.Frequency;
            long ticks_ = (long)(freq_ * (ms / 1000.0)); // ms → ticks
            long start_ = Stopwatch.GetTimestamp();
            long target_ = start_ + ticks_;

            // 第一階段：讓出 CPU（避免 100% 忙等）
            Thread.Sleep(0);

            // 第二階段：粗忙等直到剩下最後 ~50µs
            long threshold_ = freq_ / 20000; // 50µs

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

            // 第三階段：最後 50µs 用純忙等達到微秒級精度
            while (Stopwatch.GetTimestamp() < target_)
            {
                // busy wait
            }
        }

        // Sleep_Us(1000) 1ms
        public void Sleep_Us(double us)
        {
            long freq_ = Stopwatch.Frequency;
            long ticks_ = (long)(freq_ * (us / 1_000_000.0)); // us → ticks
            long start_ = Stopwatch.GetTimestamp();
            long target_ = start_ + ticks_;

            Thread.Sleep(0);

            long threshold_ = freq_ / 20000; // 50µs

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
                // busy wait
            }
        }

    }

}
