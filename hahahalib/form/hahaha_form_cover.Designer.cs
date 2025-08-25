namespace hahahalib.ui
{
    partial class hahaha_form_cover
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hahaha_form_cover));
            panel_body = new Panel();
            panel_main = new Panel();
            label_name = new Label();
            panel_status = new Panel();
            label_status = new Label();
            pic_logo = new PictureBox();
            pic_image = new PictureBox();
            panel_body.SuspendLayout();
            panel_main.SuspendLayout();
            panel_status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).BeginInit();
            SuspendLayout();
            // 
            // panel_body
            // 
            panel_body.BackColor = SystemColors.WindowFrame;
            panel_body.Controls.Add(panel_main);
            panel_body.Controls.Add(panel_status);
            panel_body.Controls.Add(pic_logo);
            panel_body.Controls.Add(pic_image);
            panel_body.Dock = DockStyle.Fill;
            panel_body.Location = new Point(0, 0);
            panel_body.Name = "panel_body";
            panel_body.Size = new Size(808, 505);
            panel_body.TabIndex = 1;
            // 
            // panel_main
            // 
            panel_main.Controls.Add(label_name);
            panel_main.Location = new Point(122, 165);
            panel_main.Name = "panel_main";
            panel_main.Size = new Size(516, 84);
            panel_main.TabIndex = 5;
            // 
            // label_name
            // 
            label_name.BackColor = Color.Transparent;
            label_name.Dock = DockStyle.Fill;
            label_name.Font = new Font("Microsoft JhengHei UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_name.ForeColor = Color.White;
            label_name.Location = new Point(0, 0);
            label_name.Name = "label_name";
            label_name.Size = new Size(516, 84);
            label_name.TabIndex = 1;
            label_name.Text = "ㄏㄏㄏ";
            label_name.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel_status
            // 
            panel_status.Controls.Add(label_status);
            panel_status.Location = new Point(187, 443);
            panel_status.Name = "panel_status";
            panel_status.Size = new Size(366, 50);
            panel_status.TabIndex = 4;
            // 
            // label_status
            // 
            label_status.Dock = DockStyle.Fill;
            label_status.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_status.ForeColor = Color.White;
            label_status.Location = new Point(0, 0);
            label_status.Name = "label_status";
            label_status.Size = new Size(366, 50);
            label_status.TabIndex = 3;
            label_status.Text = "ㄏㄏㄏ";
            label_status.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pic_logo
            // 
            pic_logo.BackColor = Color.Transparent;
            pic_logo.Image = (Image)resources.GetObject("pic_logo.Image");
            pic_logo.Location = new Point(318, 36);
            pic_logo.Name = "pic_logo";
            pic_logo.Size = new Size(114, 101);
            pic_logo.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_logo.TabIndex = 2;
            pic_logo.TabStop = false;
            // 
            // pic_image
            // 
            pic_image.BackColor = Color.Transparent;
            pic_image.Image = (Image)resources.GetObject("pic_image.Image");
            pic_image.Location = new Point(449, 245);
            pic_image.Name = "pic_image";
            pic_image.Size = new Size(356, 257);
            pic_image.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_image.TabIndex = 0;
            pic_image.TabStop = false;
            // 
            // hahaha_form_cover
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 505);
            Controls.Add(panel_body);
            Name = "hahaha_form_cover";
            Text = "封面";
            panel_body.ResumeLayout(false);
            panel_main.ResumeLayout(false);
            panel_status.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic_logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public Panel panel_body;
        public PictureBox pic_logo;
        public Label label_name;
        public PictureBox pic_image;
        public Label label_status;
        public Panel panel_main;
        public Panel panel_status;
    }
}