using System;
using System.Collections.Generic;
using System.Threading;

namespace hahahalib
{
    /// <summary>
    /// 以獨立背景執行緒處理命令佇列的工作類別。
    /// 子類別通常覆寫 <see cref="Handle(hahaha_thread_command_command)"/> 來實作實際命令處理。
    /// </summary>
    public class hahaha_thread_command
    {

        // 執行緒生命週期與同步物件。
        public Thread? Thread_;
        public ManualResetEvent? Event_Run_;
        public AutoResetEvent? Event_Wait_;
        public ManualResetEvent? Event_Exit_;

        public Queue<hahaha_thread_command_command> Queue_ = new Queue<hahaha_thread_command_command>();
        public Lock Lock_ = new Lock();

        public bool Is_Close_ = true;

        public hahaha_thread_command()
        {
            Reset();
        }

        /// <summary>
        /// 將物件重設為關閉狀態，並清空尚未處理的命令佇列。
        /// </summary>
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

        /// <summary>
        /// 建立背景工作執行緒與相關等待事件。
        /// </summary>
        public virtual int Create()
        {
            Close();

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

        /// <summary>
        /// 通知工作執行緒停止、等待它結束，並釋放同步物件。
        /// </summary>
        public virtual int Close()
        {
            Is_Close_ = true;
            Event_Exit_?.Set();

            if (Thread_ != null)
            {
                Thread_.Join();
                Thread_ = null;
            }

            Event_Run_?.Dispose(); Event_Run_ = null;
            Event_Wait_?.Dispose(); Event_Wait_ = null;
            Event_Exit_?.Dispose(); Event_Exit_ = null;

            return 0;
        }

        /// <summary>
        /// 安全終止輔助方法，只通知結束並等待，不做強制中止。
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

        /// <summary>
        /// 等待目前批次命令處理完成，或等待工作執行緒結束。
        /// </summary>
        public virtual int Wait()
        {
            if (Event_Wait_ == null || Event_Exit_ == null) return -1;

            int idx = WaitHandle.WaitAny(new WaitHandle[] { Event_Wait_, Event_Exit_ });
            return idx == 0 ? 0 : -1;
        }

        /// <summary>
        /// 將命令放入佇列，並喚醒背景執行緒。
        /// </summary>
        public virtual int Add_Command(string code, object parameter = null)
        {
            if (Event_Run_ == null) return -1;

            lock (Lock_)
            {
                Queue_.Enqueue(new hahaha_thread_command_command { Code_ = code, Parameter_ = parameter });
                Event_Run_.Set();
            }
            return 0;
        }

        /// <summary>
        /// 保留給子類別自訂命令建立流程的擴充點。
        /// 預設實作不做任何事。
        /// </summary>
        public virtual int On_Command()
        {
            return 0;
        }

        /// <summary>
        /// 執行緒主迴圈。
        /// 每次被喚醒後會盡量把目前佇列清空，再通知 <see cref="Wait"/> 可返回。
        /// </summary>
        public void Thread_Proc()
        {
            var handles = new WaitHandle[] { Event_Run_, Event_Exit_ };

            while (true)
            {
                int signaled = WaitHandle.WaitAny(handles);
                if (signaled == 1)
                {
                    break;
                }

                // 一次清掉目前批次，讓單次 Wait() 可以觀察到批次完成。
                while (!Is_Close_ && Queue_.Count > 0)
                {
                    hahaha_thread_command_command? cmd_ = null;
                    lock (Lock_)
                    {
                        cmd_ = Queue_.Dequeue();

                    }
                    {
                        Handle(cmd_);
                    }
                }

                Event_Run_.Reset();
                Event_Wait_.Set();
            }
        }

        /// <summary>
        /// 處理單一命令，應由子類別覆寫。
        /// </summary>
        public virtual int Handle(hahaha_thread_command_command cmd)
        {
            return 0;
        }

        /// <summary>
        /// 提供子類別集中處理 <see cref="Handle"/> 例外的擴充點。
        /// </summary>
        public virtual void OnHandleException(hahaha_thread_command_command cmd, Exception ex)
        {
        }
    }
}
