using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime;
using System.Text.Json;

namespace hahahalib
{
    /// <summary>
    /// 簡單的 JSON 載入/儲存工具類別，適合一般 POCO 物件持久化。
    /// 為了維持本函式庫一致風格，這裡使用狀態碼而不是直接拋出例外。
    /// </summary>
    public class hahaha_json
    {
        /// <summary>
        /// 建立 JSON 工具實例。
        /// </summary>
        public hahaha_json()
        {
        }

        /// <summary>
        /// 將 JSON 檔案內容載入到 <paramref name="obj"/>。
        /// <paramref name="section"/> 目前未使用，保留是為了相容既有呼叫方式。
        /// </summary>
        /// <returns>成功回傳 0，找不到檔案回傳 -1。</returns>
        public int Load<T>(string file_name, ref T obj, string section = "")
        {
            if (!File.Exists(file_name))
            {
                return -1;
            }

            string json = File.ReadAllText(file_name);
            var result = JsonSerializer.Deserialize<T>(json);

            if (result != null)
                obj = result;

            return 0;
        }

        /// <summary>
        /// 將 <paramref name="obj"/> 序列化為格式化 JSON 並寫入磁碟。
        /// 若目標資料夾不存在，會自動建立。
        /// </summary>
        /// <returns>成功回傳 0，序列化或檔案寫入失敗回傳 -1。</returns>
        public int Save<T>(string file_name, T obj)
        {
            try
            {
                // 允許呼叫端直接傳入多層路徑，不必先自行建立資料夾。
                var directory_ = Path.GetDirectoryName(file_name);
                if (!string.IsNullOrEmpty(directory_) && !Directory.Exists(directory_))
                {
                    Directory.CreateDirectory(directory_);
                }

                var option_ = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(obj, option_);
                File.WriteAllText(file_name, json);

                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
