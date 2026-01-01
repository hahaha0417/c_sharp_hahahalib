using Emgu.CV.Ocl;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using System;
using System.Windows.Forms;

namespace hahahalib
{
    public class hahaha_log
    {
        ILoggerFactory? Factory_ = null;

        public hahaha_log()
        { 
        
        }

        public int Create(string file_name)
        {
            // 載入 NLog 設定 (nlog.config 必須複製至輸出目錄)
            var nlog_config_ = new NLog.Config.XmlLoggingConfiguration(file_name);
            NLog.LogManager.Configuration = nlog_config_;

            // 建立 Microsoft logger factory 並把 NLog 加入
            Factory_ = LoggerFactory.Create(builder =>
            {
                builder
                    .ClearProviders() // 避免重複輸出
                    .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                    //.AddConsole() // 輸出到 Console
                    //.AddDebug()   // 輸出到 VS 偵錯輸出視窗 (Debug.WriteLine)
                    .AddNLog(); // 需 NLog.Extensions.Logging
            });


            return 0;
        }

        public int Close()
        {
            if (Factory_ != null)
            {
                Factory_.Dispose();
            }
            

            return 0;
        }

        // 方法樣板
        public Microsoft.Extensions.Logging.ILogger Create_Log(string name)
        {
            return Factory_!.CreateLogger(name);

        }

        public Microsoft.Extensions.Logging.ILogger Create_Log<T>()
        {
            return Factory_!.CreateLogger<T>();

        }


    }
}
