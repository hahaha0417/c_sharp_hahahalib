namespace hahahalib.ui
{
    partial class hahaha_form_about
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hahaha_form_about));
            panel1 = new Panel();
            label_name = new Label();
            text_content = new TextBox();
            pic_logo = new PictureBox();
            pic_image = new PictureBox();
            panel_title.SuspendLayout();
            panel_base.SuspendLayout();
            panel_body.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).BeginInit();
            SuspendLayout();
            // 
            // panel_title
            // 
            panel_title.Size = new Size(879, 65);
            // 
            // panel_base
            // 
            panel_base.Size = new Size(879, 727);
            // 
            // panel_body
            // 
            panel_body.Controls.Add(panel1);
            panel_body.Size = new Size(879, 662);
            // 
            // panel_title_system
            // 
            panel_title_system.Location = new Point(661, 0);
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.WindowFrame;
            panel1.Controls.Add(label_name);
            panel1.Controls.Add(text_content);
            panel1.Controls.Add(pic_logo);
            panel1.Controls.Add(pic_image);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(879, 662);
            panel1.TabIndex = 2;
            panel1.MouseDown += BaseMouseDown;
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.BackColor = Color.Transparent;
            label_name.Font = new Font("Microsoft JhengHei UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_name.ForeColor = Color.White;
            label_name.Location = new Point(141, 32);
            label_name.Name = "label_name";
            label_name.Size = new Size(227, 81);
            label_name.TabIndex = 1;
            label_name.Text = "ㄏㄏㄏ";
            label_name.MouseDown += BaseMouseDown;
            // 
            // text_content
            // 
            text_content.BackColor = Color.FromArgb(60, 60, 60);
            text_content.BorderStyle = BorderStyle.None;
            text_content.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            text_content.ForeColor = Color.White;
            text_content.Location = new Point(12, 129);
            text_content.Multiline = true;
            text_content.Name = "text_content";
            text_content.ReadOnly = true;
            text_content.Size = new Size(402, 364);
            text_content.TabIndex = 3;
            // 
            // pic_logo
            // 
            pic_logo.BackColor = Color.Transparent;
            pic_logo.Image = (Image)resources.GetObject("pic_logo.Image");
            pic_logo.Location = new Point(12, 12);
            pic_logo.Name = "pic_logo";
            pic_logo.Size = new Size(114, 101);
            pic_logo.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_logo.TabIndex = 2;
            pic_logo.TabStop = false;
            pic_logo.MouseDown += BaseMouseDown;
            // 
            // pic_image
            // 
            pic_image.BackColor = Color.Transparent;
            pic_image.Image = (Image)resources.GetObject("pic_image.Image");
            pic_image.Location = new Point(440, 236);
            pic_image.Name = "pic_image";
            pic_image.Size = new Size(356, 257);
            pic_image.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_image.TabIndex = 0;
            pic_image.TabStop = false;
            pic_image.MouseDown += BaseMouseDown;
            // 
            // hahaha_form_about
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 747);
            Name = "hahaha_form_about";
            Text = "關於";
            panel_title.ResumeLayout(false);
            panel_base.ResumeLayout(false);
            panel_body.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public Panel panel1;
        public Label label_name;
        public TextBox text_content;
        public PictureBox pic_logo;
        public PictureBox pic_image;
    }
}