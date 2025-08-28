using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace hahahalib.ui
{
    public partial class hahaha_form_base_titlebar : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")] static extern bool ReleaseCapture();
        [DllImport("user32.dll")] static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")] static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        // 讓框架知道我們換了邊框策略，但不觸發系統回畫標題列
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);
        const uint SWP_NOSIZE = 0x0001, SWP_NOMOVE = 0x0002, SWP_NOZORDER = 0x0004, SWP_FRAMECHANGED = 0x0020;
        

        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        // ===== DWM 關閉非用戶區繪製 + 關邊框顏色（Win11）=====
        const int DWMWA_NCRENDERING_POLICY = 2;
        const int DWMWA_BORDER_COLOR = 34;      // Win11 專用
        const int DWMNCRP_DISABLED = 1;         // 0=UseWindowStyle, 1=Disabled, 2=Enabled


        const int GWL_STYLE = -16;
        const int WS_CAPTION = 0x00C00000;
        const int WS_THICKFRAME = 0x00040000; // 也叫 WS_SIZEBOX

        // ===== Win32 =====
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int WM_NCHITTEST = 0x84;
        const int WM_SYSCOMMAND = 0x112;

        // ===== 需要攔的訊息常數 =====
        const int WM_NCCALCSIZE = 0x0083;
        const int WM_NCPAINT = 0x0085;
        const int WM_NCACTIVATE = 0x0086;
        const int WM_SETTEXT = 0x000C;
        const int WM_SETICON = 0x0080;
        const int WM_THEMECHANGED = 0x031A;

        // UXTheme 內部會下發的未文件化訊息（標題列/框線重畫）：
        const int WM_NCUAHDRAWCAPTION = 0x00AE;
        const int WM_NCUAHDRAWFRAME = 0x00AF;

        const int HTCAPTION = 0x2;
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 16;
        const int HTBOTTOMRIGHT = 17;

        const uint TPM_RETURNCMD = 0x0100;

        public Color Color_Form_ = Color.FromArgb(255, 30, 30, 30);
        public Color Color_Body_ = Color.FromArgb(255, 190, 190, 190);
        public Color Color_System_ = Color.FromArgb(255, 90, 120, 90);

        public Color Color_Title_Base_ = Color.FromArgb(255, 190, 255, 190);
        public Color Color_Title_Move_ = Color.FromArgb(255, 210, 255, 210);
        public Color Color_Title_Down_ = Color.FromArgb(255, 230, 255, 230);

        public Color Color_Button_Base_ = Color.FromArgb(255, 30, 30, 30);
        public Color Color_Button_Move_ = Color.FromArgb(255, 60, 60, 60);
        public Color Color_Button_Down_ = Color.FromArgb(255, 90, 90, 90);

        public Color Color_Logo_Base_ = Color.FromArgb(255, 60, 60, 60);
        public Color Color_Logo_Move_ = Color.FromArgb(255, 90, 90, 90);
        public Color Color_Logo_Down_ = Color.FromArgb(255, 120, 120, 120);

     

        public bool Show_Maximize_ = true;
        public int Form_Border_Width_ = 10;

        public hahaha_form_base_titlebar()
        {
            InitializeComponent();

           
            FormBorderStyle = FormBorderStyle.Sizable;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(480, 320);

            BackColor = Color_Form_;
            panel_body.BackColor = Color_Body_;
           
            panel_title.BackColor = Color_Title_Base_;
            panel_title.ForeColor = Color.Black;
            panel_logo.BackColor = Color_Logo_Base_;

           

            button_min.BackColor = Color_Button_Base_;
            button_max.BackColor = Color_Button_Base_;
            button_close.BackColor = Color_Button_Base_;

           

            button_min.ForeColor = Color.White; 
            button_max.ForeColor = Color.White;
            button_close.ForeColor = Color.White;  

            DoubleBuffered = true;


     
             
            
        }

        void SuppressNonClientRepaint()
        {
            SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
        }

        void ApplyNoNonClientDrawing()
        {
            try
            {
                int ncrDisabled = DWMNCRP_DISABLED;
                DwmSetWindowAttribute(Handle, DWMWA_NCRENDERING_POLICY, ref ncrDisabled, sizeof(int));

                // Win11 上把邊框色設 0，避免白框
                int zero = 0;
                DwmSetWindowAttribute(Handle, DWMWA_BORDER_COLOR, ref zero, sizeof(int));
            }
            catch { /* 老系統沒有就忽略 */ }
        }

        // 邊緣拉伸
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // 告訴系統：非用戶區我自己處理，取消預設邊框/標題列尺寸
                case WM_NCCALCSIZE:
                    if (m.WParam != IntPtr.Zero) { m.Result = IntPtr.Zero; return; }
                    break;

                // 阻止系統在非用戶區重繪（避免白邊）
                case WM_NCPAINT:
                    m.Result = IntPtr.Zero;
                    return;

                // 焦點切換時，阻止系統非用戶區重畫
                case WM_NCACTIVATE:
                    // 直接回 1（視為已處理且保持外觀不變）
                    m.Result = (IntPtr)1;
                    return;

                //// 改標題文字、改 Icon 都會觸發非用戶區重繪，一併吃掉重畫
                //case WM_SETTEXT:
                //case WM_SETICON:
                //    base.WndProc(ref m);   // 讓文字/圖示真的被更新
                //                           // 然後阻止隨後的系統非用戶區重畫
                //    SuppressNonClientRepaint();
                //    return;

                //// 主題切換時，避免系統回畫標題列；同時重套一次 DWM 設定
                //case WM_THEMECHANGED:
                //    ApplyNoNonClientDrawing();
                //    m.Result = IntPtr.Zero;
                //    return;

                //// 這兩個是 UXTheme 用來畫標題列/框線的「內部訊息」，直接吃掉
                //case WM_NCUAHDRAWCAPTION:
                //case WM_NCUAHDRAWFRAME:
                //    m.Result = IntPtr.Zero;
                //    return;
                case WM_NCHITTEST:
                    base.WndProc(ref m);

                    Point p = PointToClient(new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16));
                    bool top = p.Y < Form_Border_Width_;
                    bool left = p.X < Form_Border_Width_;
                    bool right = p.X > Width - Form_Border_Width_;
                    bool bottom = p.Y > Height - Form_Border_Width_;

                    if (top && left) { m.Result = (IntPtr)HTTOPLEFT; return; }
                    if (top && right) { m.Result = (IntPtr)HTTOPRIGHT; return; }
                    if (bottom && left) { m.Result = (IntPtr)HTBOTTOMLEFT; return; }
                    if (bottom && right) { m.Result = (IntPtr)HTBOTTOMRIGHT; return; }
                    if (top) { m.Result = (IntPtr)HTTOP; return; }
                    if (left) { m.Result = (IntPtr)HTLEFT; return; }
                    if (right) { m.Result = (IntPtr)HTRIGHT; return; }
                    if (bottom) { m.Result = (IntPtr)HTBOTTOM; return; }
                    return;
            }
     
            base.WndProc(ref m);
        }

        public void hahaha_form_base_titlebar_Shown(object sender, EventArgs e)
        {
            base.OnHandleCreated(e);
            int style = GetWindowLong(this.Handle, GWL_STYLE);
            style &= ~WS_CAPTION;
            //style &= ~WS_THICKFRAME;                 // 去掉系統可縮放邊框
            SetWindowLong(this.Handle, GWL_STYLE, style);
            
            
            
        }

   

        public void panel_title_MouseDown(object sender, MouseEventArgs e)
        {
            panel_title.BackColor = Color_Title_Down_;
            DragWindow(sender, e);
        }

        public void panel_title_MouseEnter(object sender, EventArgs e)
        {
            panel_title.BackColor = Color_Title_Move_;
        }

        public void panel_title_MouseLeave(object sender, EventArgs e)
        {
            panel_title.BackColor = Color_Title_Base_;
        }

        public void panel_title_MouseMove(object sender, MouseEventArgs e)
        {
            panel_title.BackColor = Color_Title_Move_;
        }

        public void panel_title_MouseUp(object sender, MouseEventArgs e)
        {
            panel_title.BackColor = Color_Title_Move_;
        }

        public void button_system_MouseDown(object sender, MouseEventArgs e)
        {
            Button button_ = (Button)sender;
            button_.BackColor = Color_Button_Down_;
        }

        public void button_system_MouseEnter(object sender, EventArgs e)
        {
            Button button_ = (Button)sender;
            button_.BackColor = Color_Button_Move_;
        }

        public void button_system_MouseLeave(object sender, EventArgs e)
        {
            Button button_ = (Button)sender;
            button_.BackColor = Color_Button_Base_;
        }

        public void button_system_MouseMove(object sender, MouseEventArgs e)
        {
            Button button_ = (Button)sender;
            button_.BackColor = Color_Button_Move_;
        }

        public void button_system_MouseUp(object sender, MouseEventArgs e)
        {
            Button button_ = (Button)sender;
            button_.BackColor = Color_Button_Move_;
        }

        public void label_system_system_MouseDown(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)((Label)sender).Parent;
            panel_.BackColor = Color_Button_Down_;
        }

        public void label_system_system_MouseEnter(object sender, EventArgs e)
        {
            Panel panel_ = (Panel)((Label)sender).Parent;
            panel_.BackColor = Color_Button_Move_;

        }

        public void label_system_system_MouseLeave(object sender, EventArgs e)
        {
            Panel panel_ = (Panel)((Label)sender).Parent;
            panel_.BackColor = Color_Button_Base_;
        }

        public void label_system_system_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)((Label)sender).Parent;
            panel_.BackColor = Color_Button_Move_;
        }

        public void label_system_system_MouseUp(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)((Label)sender).Parent;
            if (panel_ != null)
            {
                panel_.BackColor = Color_Button_Move_;
            }

        }

        public void panel_system_system_MouseDown(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)sender;
            panel_.BackColor = Color_Button_Down_;
        }

        public void panel_system_system_MouseEnter(object sender, EventArgs e)
        {
            Panel panel_ = (Panel)sender;
            panel_.BackColor = Color_Button_Move_;
        }

        public void panel_system_system_MouseLeave(object sender, EventArgs e)
        {
            Panel panel_ = (Panel)sender;
            panel_.BackColor = Color_Button_Base_;
        }

        public void panel_system_system_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)sender;
            panel_.BackColor = Color_Button_Move_;
        }

        public void panel_system_system_MouseUp(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)sender;
            panel_.BackColor = Color_Button_Move_;
        }

        public void panel_logo_MouseDown(object sender, MouseEventArgs e)
        {
            Panel panel_ = (Panel)sender;
            panel_.BackColor = Color_Logo_Down_;
        }

        public void panel_logo_MouseEnter(object sender, EventArgs e)
        {
            panel_logo.BackColor = Color_Logo_Move_;
        }

        public void panel_logo_MouseLeave(object sender, EventArgs e)
        {
            panel_logo.BackColor = Color_Logo_Base_;
        }

        public void panel_logo_MouseMove(object sender, MouseEventArgs e)
        {
            panel_logo.BackColor = Color_Logo_Move_;
        }

        public void panel_logo_MouseUp(object sender, MouseEventArgs e)
        {
            panel_logo.BackColor = Color_Logo_Move_;
        }

        public void button_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public void button_max_Click(object sender, EventArgs e)
        {
            if (Show_Maximize_)
            {
                ToggleMaximizeRestore();
            }
        }

        public void ToggleMaximizeRestore()
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        public void DragWindow(object? s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
            }
        }

        public void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void label_system_version_Click(object sender, EventArgs e)
        {

        }

        public void label_system_about_Click(object sender, EventArgs e)
        {

        }

        public void label_system_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void panel_logo_Click(object sender, EventArgs e)
        {
           
        }

        public void hahaha_form_base_titlebar_Load(object sender, EventArgs e)
        {
            
        }
    }
}
