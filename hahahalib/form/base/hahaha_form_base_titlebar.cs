using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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


        const int GWL_STYLE = -16;
        const int WS_CAPTION = 0x00C00000;
        const int WS_THICKFRAME = 0x00040000; // 也叫 WS_SIZEBOX

        // ===== Win32 =====
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int WM_NCHITTEST = 0x84;
        const int WM_SYSCOMMAND = 0x112;

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
        public Color Color_System_ = Color.FromArgb(255, 110, 110, 110);

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

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FormBorderStyle = FormBorderStyle.None;
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

            Load += (s, e) => MaximizedBounds = Screen.FromHandle(Handle).WorkingArea; 
            SizeChanged += (s, e) =>
            {
                button_max.Text = WindowState == FormWindowState.Maximized ? "🗗" : "☐";
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            };

     
             
            
        }

        // 邊緣拉伸
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
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
            style &= ~WS_THICKFRAME;                 // 去掉系統可縮放邊框
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
