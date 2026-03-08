using System;
using System.Runtime.InteropServices;

namespace hahahalib
{
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

        public int Reset()
        {
            Start_ = 0;
            End_ = 0;
            Ticks_ = 0;

            QueryPerformanceFrequency(out Ticks_);

            return 0;
        }
        public int Start()
        {
            QueryPerformanceCounter(out Start_);
            return 0;
        }

        public int End()
        {
            QueryPerformanceCounter(out End_);
            return 0;
        }

        public double Time_S()
        {
            return (double)(End_ - Start_) * 1.0 / (double)Ticks_;
        }

        public double Time_Ms()
        {
            return (double)(End_ - Start_) * 1000.0 / (double)Ticks_;
        }

        public double Time_Us()
        {
            return (double)(End_ - Start_) * 1000000.0 / (double)Ticks_;
        }


    }
}
