using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace hahahalib.ui
{
    /// <summary>
    /// 使用共用自訂標題列基底表單的版本資訊視窗。
    /// 點擊基底區域時只會隱藏，不會直接釋放。
    /// </summary>
    public partial class hahaha_form_version : hahahalib.ui.hahaha_form_base_titlebar
    {
        public hahaha_form_version()
        {
            InitializeComponent();

            panel_title_system.Visible = false;

            Size = new Size(830, 600);
        }

        private void BaseMouseDown(object sender, MouseEventArgs e)
        {
            Hide();
        }
    }
}
