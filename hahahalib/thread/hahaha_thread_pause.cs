using System;
using System.Threading;

namespace hahahalib
{
    /// <summary>
    /// 可重複使用的單工作執行緒包裝類別。
    /// 每次呼叫 <see cref="Enabled"/> 都會喚醒執行緒一次，並執行 <see cref="Handle"/>。
    /// </summary>
    public class hahaha_thread_pause
    {
        public Thread? Thread_;
        public ManualResetEvent? Event_Run_;   // 對應 Event_Run_
        public AutoResetEvent? Event_Wait_;    // 對應 Event_Wait_
        public ManualResetEvent? Event_Exit_;  // 對應 Event_Exit_
        public bool Is_Close_ = true;

        public hahaha_thread_pause()
        {
            Reset();
        }

        /// <summary>
        /// 重設執行緒參考與事件狀態，並標記為已關閉。
        /// </summary>
        public virtual void Reset()
        {
            Thread_ = null;
            Event_Run_ = null;
            Event_Wait_ = null;
            Event_Exit_ = null;
            Is_Close_ = true;
        }

        /// <summary>
        /// 建立背景執行緒與等待事件。
        /// </summary>
        public virtual void Create()
        {
            Close();

            Event_Run_ = new ManualResetEvent(false);
            Event_Wait_ = new AutoResetEvent(false);
            Event_Exit_ = new ManualResetEvent(false);

            Is_Close_ = false;

            Thread_ = new Thread(Thread_Proc)
            {
                IsBackground = true
            };

            Thread_!.Start();
        }



        /// <summary>
        /// 通知執行緒結束並釋放相關等待事件。
        /// </summary>
        public virtual void Close()
        {
            if (!Is_Close_)
            {
                
                if (Event_Exit_ != null)
                {
                    Event_Exit_.Set();
                    Event_Exit_.Dispose();
                    Event_Exit_ = null;
                }
                Event_Run_?.Dispose();
                Event_Run_ = null;

                Event_Wait_?.Dispose();
                Event_Wait_ = null;

                if (Thread_ != null)
                {
                    Thread_.Join(100);
                    Thread_ = null;
                }

                Is_Close_ = true;
            }
            
        }

        /// <summary>
        /// 觸發一次 <see cref="Handle"/> 執行。
        /// </summary>
        public virtual void Enabled()
        {
            Event_Run_?.Set();
        }

        /// <summary>
        /// 保留給子類別自行實作停用行為的擴充點。
        /// </summary>
        public virtual void Disabled()
        {
            
        }

        /// <summary>
        /// 等待目前工作完成，或等待執行緒結束。
        /// </summary>
        public virtual int Wait()
        {
            if (Event_Wait_ == null || Event_Exit_ == null)
                return -1;

            int idx = WaitHandle.WaitAny(new WaitHandle[] { Event_Wait_, Event_Exit_ });
            return idx == 0 ? 0 : -1;
        }

        /// <summary>
        /// 安全地終止背景工作執行緒。
        /// </summary>
        public virtual void Terminate()
        {
            if (Event_Exit_ != null)
            {
                Event_Exit_.Set();
            }

            if (Thread_ != null)
            {
                Thread_.Join();
                Thread_ = null;
            }
        }

        /// <summary>
        /// 執行緒主程序。
        /// </summary>
        public void Thread_Proc()
        {
            WaitHandle[] handles = { Event_Run_, Event_Exit_ };

            while (!Is_Close_)
            {
                int idx = WaitHandle.WaitAny(handles);
                if (idx == 0)
                {
                    Handle();
                
                    Event_Run_?.Reset();
                    Event_Wait_?.Set();
                }
                else if (idx == 1)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 由子類別覆寫，實作實際工作內容。
        /// </summary>
        public virtual void Handle()
        {
        }
    }
}
