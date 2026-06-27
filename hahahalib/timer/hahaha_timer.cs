using System;
using System.Runtime.InteropServices;

namespace hahahalib
{
    /// <summary>
    /// 以 QueryPerformanceCounter 包裝的高精度計時器。
    /// 適合量測短時間程式區段的執行時間。
    /// </summary>
    public class Hahaha_Timer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long Ticks_;
        private long Start_;
        private long End_;

        public Hahaha_Timer()
        {
            Reset();
        }

        /// <summary>
        /// 重新讀取計數器頻率，並清除起始與結束時間。
        /// </summary>
        public int Reset()
        {
            Start_ = 0;
            End_ = 0;
            Ticks_ = 0;

            QueryPerformanceFrequency(out Ticks_);

            return 0;
        }

        /// <summary>
        /// 記錄起始時間戳。
        /// </summary>
        public int Start()
        {
            QueryPerformanceCounter(out Start_);
            return 0;
        }

        /// <summary>
        /// 記錄結束時間戳。
        /// </summary>
        public int End()
        {
            QueryPerformanceCounter(out End_);
            return 0;
        }

        /// <summary>
        /// 取得量測結果，單位為秒。
        /// </summary>
        public double Time_S()
        {
            return (double)(End_ - Start_) * 1.0 / (double)Ticks_;
        }

        /// <summary>
        /// 取得量測結果，單位為毫秒。
        /// </summary>
        public double Time_Ms()
        {
            return (double)(End_ - Start_) * 1000.0 / (double)Ticks_;
        }

        /// <summary>
        /// 取得量測結果，單位為微秒。
        /// </summary>
        public double Time_Us()
        {
            return (double)(End_ - Start_) * 1000000.0 / (double)Ticks_;
        }
    }
}
