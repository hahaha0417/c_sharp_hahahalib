using System;
using System.Threading;

namespace hahahalib
{
    /// <summary>
    /// 可被繼承的執行緒暫停控制類
    /// - Create() 建立執行緒與事件
    /// - Enabled() 觸發執行緒執行 Handle()
    /// - Wait() 等待 Handle() 完成
    /// - Close() 關閉執行緒與資源
    /// - Terminate() 強制終止（危險，盡量避免）
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

        public virtual void Reset()
        {
            Thread_ = null;
            Event_Run_ = null;
            Event_Wait_ = null;
            Event_Exit_ = null;
            Is_Close_ = true;
        }

        /// <summary>
        /// 建立執行緒與事件
        /// </summary>
        public virtual void Create()
        {
            Close();

            Event_Run_ = new ManualResetEvent(false);   // ManualResetEvent (相當於 CreateEventW TRUE)
            Event_Wait_ = new AutoResetEvent(false);    // AutoResetEvent (相當於 CreateEventW FALSE)
            Event_Exit_ = new ManualResetEvent(false);

            Is_Close_ = false;

            Thread_ = new Thread(Thread_Proc)
            {
                IsBackground = true
            };
            Thread_.Start();
        }

        /// <summary>
        /// 關閉執行緒與事件
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
                    Thread_.Join(100); // 等待結束
                    Thread_ = null;
                }

                Is_Close_ = true;
            }
            
        }

        /// <summary>
        /// 啟動一次 Handle()
        /// </summary>
        public virtual void Enabled()
        {
            Event_Run_?.Set();
        }

        /// <summary>
        /// Disabled
        /// </summary>
        public virtual void Disabled()
        {
            
        }

        /// <summary>
        /// 等待 Handle() 結束
        /// </summary>
        public virtual int Wait()
        {
            if (Event_Wait_ == null || Event_Exit_ == null)
                return -1;

            int idx = WaitHandle.WaitAny(new WaitHandle[] { Event_Wait_, Event_Exit_ });
            return idx == 0 ? 0 : -1;
        }

        /// <summary>
        /// (危險) 強制中止執行緒
        /// </summary>
        public virtual void Terminate()
        {
            if (Event_Exit_ != null)
            {
                Event_Exit_.Set(); // 通知 ThreadProc 跳出迴圈
            }

            if (Thread_ != null)
            {
                Thread_.Join();
                Thread_ = null;
            }
        }

        /// <summary>
        /// 執行緒函式
        /// </summary>
        public void Thread_Proc()
        {
            WaitHandle[] handles = { Event_Run_, Event_Exit_ };

            while (!Is_Close_)
            {
                int idx = WaitHandle.WaitAny(handles);
                if (idx == 0) // Run
                {
                    Handle();
                
                    Event_Run_?.Reset();   // 對應 ResetEvent
                    Event_Wait_?.Set();    // 通知 Wait() 完成
                }
                else if (idx == 1) // Exit
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 子類別覆寫，實際處理工作
        /// </summary>
        public virtual void Handle()
        {
            // 預設不做事
        }
    }
}
