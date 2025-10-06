using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime;
using System.Text.Json;

namespace hahahalib
{
    public class hahaha_json
    {
        

        public hahaha_json()
        {
            
        }

       

        public int Load<T>(string file_name, ref T obj, string section = "")
        {
            if (!File.Exists(file_name))
            {
                return -1;
            }

            var builder_ = new ConfigurationBuilder()
                .AddJsonFile(file_name, optional: false, reloadOnChange: true);

            IConfiguration config_ = builder_.Build();

            // 綁定到類別
            config_.GetSection(section).Bind(obj);


            return 0;
        }

        public int Save<T>(string file_name, T obj)
        {
            try
            {
                // 取得檔案的資料夾路徑
                var directory_ = Path.GetDirectoryName(file_name);
                if (!string.IsNullOrEmpty(directory_) && !Directory.Exists(directory_))
                {
                    Directory.CreateDirectory(directory_); // ✅ 自動建立資料夾
                }

                var option_ = new JsonSerializerOptions
                {
                    WriteIndented = true // 格式化輸出
                };

                string json = JsonSerializer.Serialize(obj, option_);
                File.WriteAllText(file_name, json);

                return 0; // 成功
            }
            catch (Exception)
            {
                // 可以用 logger 記錄 ex
                return -1; // 失敗
            }

        }

    }
}
