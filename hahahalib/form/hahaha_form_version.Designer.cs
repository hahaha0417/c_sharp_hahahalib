namespace hahahalib.ui
{
    partial class hahaha_form_version
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hahaha_form_version));
            panel_body = new Panel();
            pic_image = new PictureBox();
            label_name = new Label();
            pic_logo = new PictureBox();
            label_license = new Label();
            label_version_label = new Label();
            label_author_label = new Label();
            label_author = new Label();
            label_version = new Label();
            label_description = new Label();
            label_company = new Label();
            label_company_label = new Label();
            label_license_label = new Label();
            panel_body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_image).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pic_logo).BeginInit();
            SuspendLayout();
            // 
            // panel_body
            // 
            panel_body.BackColor = SystemColors.WindowFrame;
            panel_body.Controls.Add(label_license_label);
            panel_body.Controls.Add(label_company);
            panel_body.Controls.Add(label_company_label);
            panel_body.Controls.Add(label_description);
            panel_body.Controls.Add(label_version);
            panel_body.Controls.Add(label_author);
            panel_body.Controls.Add(label_author_label);
            panel_body.Controls.Add(label_version_label);
            panel_body.Controls.Add(label_license);
            panel_body.Controls.Add(pic_logo);
            panel_body.Controls.Add(label_name);
            panel_body.Controls.Add(pic_image);
            panel_body.Dock = DockStyle.Fill;
            panel_body.Location = new Point(0, 0);
            panel_body.Name = "panel_body";
            panel_body.Size = new Size(800, 494);
            panel_body.TabIndex = 0;
            // 
            // pic_image
            // 
            pic_image.BackColor = Color.Transparent;
            pic_image.Image = (Image)resources.GetObject("pic_image.Image");
            pic_image.Location = new Point(441, 234);
            pic_image.Name = "pic_image";
            pic_image.Size = new Size(356, 257);
            pic_image.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_image.TabIndex = 0;
            pic_image.TabStop = false;
            // 
            // label_name
            // 
            label_name.AutoSize = true;
            label_name.Font = new Font("Microsoft JhengHei UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_name.ForeColor = Color.White;
            label_name.Location = new Point(144, 23);
            label_name.Name = "label_name";
            label_name.Size = new Size(227, 81);
            label_name.TabIndex = 1;
            label_name.Text = "ㄏㄏㄏ";
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
            // label_license
            // 
            label_license.AutoSize = true;
            label_license.BackColor = Color.Transparent;
            label_license.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_license.ForeColor = Color.White;
            label_license.Location = new Point(132, 432);
            label_license.Name = "label_license";
            label_license.Size = new Size(79, 41);
            label_license.TabIndex = 3;
            label_license.Text = "MIT";
            // 
            // label_version_label
            // 
            label_version_label.AutoSize = true;
            label_version_label.BackColor = Color.Transparent;
            label_version_label.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_version_label.ForeColor = Color.White;
            label_version_label.Location = new Point(26, 262);
            label_version_label.Name = "label_version_label";
            label_version_label.Size = new Size(106, 41);
            label_version_label.TabIndex = 4;
            label_version_label.Text = "版本 : ";
            // 
            // label_author_label
            // 
            label_author_label.AutoSize = true;
            label_author_label.BackColor = Color.Transparent;
            label_author_label.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_author_label.ForeColor = Color.White;
            label_author_label.Location = new Point(26, 318);
            label_author_label.Name = "label_author_label";
            label_author_label.Size = new Size(106, 41);
            label_author_label.TabIndex = 5;
            label_author_label.Text = "作者 : ";
            // 
            // label_author
            // 
            label_author.AutoSize = true;
            label_author.BackColor = Color.Transparent;
            label_author.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_author.ForeColor = Color.White;
            label_author.Location = new Point(132, 318);
            label_author.Name = "label_author";
            label_author.Size = new Size(132, 41);
            label_author.TabIndex = 6;
            label_author.Text = "hahaha";
            // 
            // label_version
            // 
            label_version.AutoSize = true;
            label_version.BackColor = Color.Transparent;
            label_version.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_version.ForeColor = Color.White;
            label_version.Location = new Point(132, 262);
            label_version.Name = "label_version";
            label_version.Size = new Size(118, 41);
            label_version.TabIndex = 7;
            label_version.Text = "1.0.0.0";
            // 
            // label_description
            // 
            label_description.AutoSize = true;
            label_description.BackColor = Color.Transparent;
            label_description.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_description.ForeColor = Color.White;
            label_description.Location = new Point(153, 115);
            label_description.Name = "label_description";
            label_description.Size = new Size(75, 52);
            label_description.TabIndex = 8;
            label_description.Text = "ㄏㄏㄏ\r\nㄏㄏㄏ";
            // 
            // label_company
            // 
            label_company.AutoSize = true;
            label_company.BackColor = Color.Transparent;
            label_company.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_company.ForeColor = Color.White;
            label_company.Location = new Point(132, 375);
            label_company.Name = "label_company";
            label_company.Size = new Size(224, 41);
            label_company.TabIndex = 10;
            label_company.Text = "hahaha Corp.";
            // 
            // label_company_label
            // 
            label_company_label.AutoSize = true;
            label_company_label.BackColor = Color.Transparent;
            label_company_label.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_company_label.ForeColor = Color.White;
            label_company_label.Location = new Point(26, 375);
            label_company_label.Name = "label_company_label";
            label_company_label.Size = new Size(106, 41);
            label_company_label.TabIndex = 9;
            label_company_label.Text = "公司 : ";
            // 
            // label_license_label
            // 
            label_license_label.AutoSize = true;
            label_license_label.BackColor = Color.Transparent;
            label_license_label.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label_license_label.ForeColor = Color.White;
            label_license_label.Location = new Point(26, 432);
            label_license_label.Name = "label_license_label";
            label_license_label.Size = new Size(106, 41);
            label_license_label.TabIndex = 11;
            label_license_label.Text = "授權 : ";
            // 
            // hahaha_form_version
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 494);
            Controls.Add(panel_body);
            Name = "hahaha_form_version";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "版本";
            panel_body.ResumeLayout(false);
            panel_body.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic_image).EndInit();
            ((System.ComponentModel.ISupportInitialize)pic_logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public Panel panel_body;
        public Label label_name;
        public PictureBox pic_image;
        public PictureBox pic_logo;
        public Label label_license;
        public Label label_version_label;
        public Label label_company;
        public Label label_company_label;
        public Label label_description;
        public Label label_version;
        public Label label_author;
        public Label label_author_label;
        public Label label_license_label;
    }
}