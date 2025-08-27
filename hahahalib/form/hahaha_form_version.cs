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
