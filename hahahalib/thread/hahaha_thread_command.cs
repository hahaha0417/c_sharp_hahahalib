using System;
using System.Collections.Generic;
using System.Threading;

namespace hahahalib
{
    /// <summary>
    /// C# 版：hahaha_thread_command
    /// - Create(): 建立背景執行緒與事件
    /// - AddCommand(): 丟入一個命令並喚醒執行緒
    /// - Wait(): 等到當前批次命令處理完成（或被要求退出）
    /// - Close(): 通知退出並釋放資源
    /// - Terminate(): 安全版的通知退出（不強殺）
    /// - Handle(): 覆寫以處理單一命令
    /// </summary>
    public class hahaha_thread_command
    {



        // === 狀態/同步物件 ===
        public Thread? Thread_;
        public ManualResetEvent? Event_Run_;   // 對應 Event_Run_ (manual reset)
        public AutoResetEvent? Event_Wait_;   // 對應 Event_Wait_ (auto reset)
        public ManualResetEvent? Event_Exit_;  // 對應 Event_Exit_ (manual reset)

        public Queue<hahaha_thread_command_command> Queue_ = new Queue<hahaha_thread_command_command>();
        public Lock Lock_ = new Lock();

        public bool Is_Close_ = true;

        // === 建構/重設 ===
        public hahaha_thread_command()
        {
            Reset();
        }

        public virtual int Reset()
        {
            Thread_ = null;
            Event_Run_ = null;
            Event_Wait_ = null;
            Event_Exit_ = null;
            Is_Close_ = true;

            lock (Lock_)
            {
                Queue_.Clear();
            }
            return 0;
        }

        // === 建立/關閉 ===
        public virtual int Create()
        {
            Close(); // 確保乾淨狀態

            Event_Run_ = new ManualResetEvent(false);
            Event_Wait_ = new AutoResetEvent(false);
            Event_Exit_ = new ManualResetEvent(false);
            Is_Close_ = false;

            Thread_ = new Thread(Thread_Proc)
            {
                IsBackground = true,
                Name = "ThreadCommandWorker"
            };
            Thread_.Start();

            return 0;
        }

        public virtual int Close()
        {
            // 通知退出
            Is_Close_ = true;
            Event_Exit_?.Set();

            // 等待執行緒結束
            if (Thread_ != null)
            {
                Thread_.Join();
                Thread_ = null;
            }

            // 釋放事件
            Event_Run_?.Dispose(); Event_Run_ = null;
            Event_Wait_?.Dispose(); Event_Wait_ = null;
            Event_Exit_?.Dispose(); Event_Exit_ = null;

            return 0;
        }

        /// <summary>
        /// 安全終止：只做「通知退出＋等待」，不使用 Thread.Abort()
        /// </summary>
        public virtual int Terminate()
        {
            Is_Close_ = true;
            Event_Exit_?.Set();

            if (Thread_ != null)
            {
                Thread_.Join();
                Thread_ = null;
            }
            return 0;
        }

        // === 等待目前批次處理完成 ===
        public virtual int Wait()
        {
            if (Event_Wait_ == null || Event_Exit_ == null) return -1;

            int idx = WaitHandle.WaitAny(new WaitHandle[] { Event_Wait_, Event_Exit_ });
            return idx == 0 ? 0 : -1;
        }

        // === 丟入命令 ===
        public virtual int Add_Command(string code, object parameter = null)
        {
            if (Event_Run_ == null) return -1;

            lock (Lock_)
            {
                Queue_.Enqueue(new hahaha_thread_command_command { Code_ = code, Parameter_ = parameter });
                // 有命令就喚醒
                Event_Run_.Set();
            }
            return 0;
        }

        /// <summary>
        /// 若你想複寫「命令建立流程」可覆寫本方法；預設改走 AddCommand。
        /// </summary>
        public virtual int On_Command()
        {
            return 0;
        }

        // === 執行緒主迴圈 ===
        public void Thread_Proc()
        {
            var handles = new WaitHandle[] { Event_Run_, Event_Exit_ };

            while (true)
            {
                int signaled = WaitHandle.WaitAny(handles);
                if (signaled == 1) // Exit
                {
                    break;
                }

                // 被 Run 事件喚醒：把目前佇列中所有命令處理完
                while (!Is_Close_ && Queue_.Count > 0)
                {
                    hahaha_thread_command_command? cmd_ = null;
                    lock (Lock_)
                    {
                        cmd_ = Queue_.Dequeue();

                    }

              

                    //try
                    {
                        Handle(cmd_);
                    }
                    //catch (Exception ex)
                    //{
                    //    OnHandleException(cmd_, ex);
                    //}
                }

                // 批次結束：清除 Run，並通知 Wait()
                Event_Run_.Reset();
                Event_Wait_.Set();
            }
        }

        /// <summary>
        /// 處理單一命令：請在子類別覆寫
        /// </summary>
        public virtual int Handle(hahaha_thread_command_command cmd)
        {
            // 預設不做事，回 0
            return 0;
        }

        /// <summary>
        /// 可覆寫：處理 Handle() 內部拋出的例外
        /// </summary>
        public virtual void OnHandleException(hahaha_thread_command_command cmd, Exception ex)
        {
            // 預設吞例外；可改寫成記錄或回報
        }
    }
}
