# hahahalib 手冊

歡迎使用 `hahahalib` 手冊。

本手冊目標：

- 整理 `hahahalib` 的公開 API 與主要類別用途
- 提供維護時可快速查閱的類別說明
- 作為後續補充 C# XML 註解與使用範例的基礎

## 專案定位

`hahahalib` 是一個偏實務、偏輕量的 C# 類別庫，目前內容以這幾類為主：

- 執行緒與命令佇列控制
- 計時與高精度等待
- WinForms UI 基底元件
- GNSS / NMEA 解析
- JSON 讀寫工具
- 影像與 Bitmap / Mat 共用封裝

## 建議閱讀順序

如果你第一次接觸這個專案，建議先看：

1. `hahaha_thread_command`
2. `hahaha_thread_pause`
3. `hahaha_form_base_titlebar`
4. `hahaha_json`
5. `hahaha_gps_gnss`

## 文件產生方式

本專案已提供 `Doxyfile`。

安裝 Doxygen 後，在專案根目錄執行：

```powershell
doxygen Doxyfile
```

產生完成後，首頁通常會在：

```text
docs/output/doxygen/html/index.html
```

## 註解撰寫原則

為了讓手冊品質穩定，後續建議優先補這些內容：

- 類別用途
- 公開方法的輸入與輸出
- 執行緒生命週期
- 資源釋放責任
- UI / Win32 行為限制
