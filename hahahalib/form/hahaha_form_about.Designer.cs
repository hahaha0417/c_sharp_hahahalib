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
            panel_body = new Panel();
            label_name = new Label();
            text_content = new TextBox();
            pic_logo = new PictureBox();
            pic_image = new PictureBox();
            panel_body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).BeginInit();
            SuspendLayout();
            // 
            // panel_body
            // 
            panel_body.BackColor = SystemColors.WindowFrame;
            panel_body.Controls.Add(label_name);
            panel_body.Controls.Add(text_content);
            panel_body.Controls.Add(pic_logo);
            panel_body.Controls.Add(pic_image);
            panel_body.Dock = DockStyle.Fill;
            panel_body.Location = new Point(0, 0);
            panel_body.Name = "panel_body";
            panel_body.Size = new Size(808, 505);
            panel_body.TabIndex = 1;
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
            // 
            // text_content
            // 
            text_content.Location = new Point(12, 129);
            text_content.Multiline = true;
            text_content.Name = "text_content";
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
            // 
            // hahaha_form_about
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 505);
            Controls.Add(panel_body);
            Name = "hahaha_form_about";
            Text = "關於";
            panel_body.ResumeLayout(false);
            panel_body.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_image).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public Panel panel_body;
        public PictureBox pic_logo;
        public Label label_name;
        public PictureBox pic_image;
        public TextBox text_content;
    }
}