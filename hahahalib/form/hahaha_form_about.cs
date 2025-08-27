using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hahahalib.ui
{
    public partial class hahaha_form_about : hahahalib.ui.hahaha_form_base_titlebar
    {
        public hahaha_form_about()
        {
            InitializeComponent();

            panel_title_system.Visible = false;
            text_content.BackColor = Color.FromArgb(255, 60, 60, 60);
            text_content.ForeColor = Color.White;

            Size = new Size(830, 590);
        }

        private void BaseMouseDown(object sender, MouseEventArgs e)
        {
            Hide();
        }
    }
}
