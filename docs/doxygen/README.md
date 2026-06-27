# Doxygen 使用說明

## 目的

這個資料夾存放 `hahahalib` 的 Doxygen 手冊設定與首頁內容。

## 目前配置

- `../../Doxyfile`：Doxygen 主設定檔
- `mainpage.md`：手冊首頁

## 產生方式

在專案根目錄執行：

```powershell
doxygen Doxyfile
```

## 輸出位置

```text
docs/output/doxygen/html/index.html
```

## 設定重點

- 已排除 `bin/`、`obj/`
- 已排除 `*.Designer.cs` 與 `Resources.Designer.cs`
- 主要掃描手寫 `.cs` 檔與 Markdown 文件
- 輸出語言設定為繁體中文
