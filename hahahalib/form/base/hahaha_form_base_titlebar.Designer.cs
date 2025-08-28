namespace hahahalib.ui
{
    partial class hahaha_form_base_titlebar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hahaha_form_base_titlebar));
            panel_title = new Panel();
            label_title = new Label();
            panel_logo = new Panel();
            panel_title_system = new Panel();
            flow_layout_panel_title_system = new FlowLayoutPanel();
            button_close = new Button();
            button_max = new Button();
            button_min = new Button();
            panel_base = new Panel();
            panel_body = new Panel();
            panel_title.SuspendLayout();
            panel_title_system.SuspendLayout();
            flow_layout_panel_title_system.SuspendLayout();
            panel_base.SuspendLayout();
            SuspendLayout();
            // 
            // panel_title
            // 
            panel_title.BackColor = SystemColors.ControlDarkDark;
            panel_title.Controls.Add(panel_title_system);
            panel_title.Controls.Add(label_title);
            panel_title.Controls.Add(panel_logo);
            panel_title.Dock = DockStyle.Top;
            panel_title.Location = new Point(0, 0);
            panel_title.Name = "panel_title";
            panel_title.Size = new Size(1010, 65);
            panel_title.TabIndex = 4;
            panel_title.MouseDown += panel_title_MouseDown;
            panel_title.MouseEnter += panel_title_MouseEnter;
            panel_title.MouseLeave += panel_title_MouseLeave;
            panel_title.MouseMove += panel_title_MouseMove;
            panel_title.MouseUp += panel_title_MouseUp;
            // 
            // label_title
            // 
            label_title.BackColor = Color.Transparent;
            label_title.Dock = DockStyle.Left;
            label_title.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 136);
            label_title.Location = new Point(83, 0);
            label_title.Name = "label_title";
            label_title.Padding = new Padding(10, 0, 0, 0);
            label_title.Size = new Size(350, 65);
            label_title.TabIndex = 6;
            label_title.Text = "hahaha";
            label_title.TextAlign = ContentAlignment.MiddleLeft;
            label_title.MouseDown += panel_title_MouseDown;
            label_title.MouseEnter += panel_title_MouseEnter;
            label_title.MouseLeave += panel_title_MouseLeave;
            label_title.MouseMove += panel_title_MouseMove;
            label_title.MouseUp += panel_title_MouseUp;
            // 
            // panel_logo
            // 
            panel_logo.BackColor = Color.Silver;
            panel_logo.BackgroundImage = (Image)resources.GetObject("panel_logo.BackgroundImage");
            panel_logo.Dock = DockStyle.Left;
            panel_logo.Location = new Point(0, 0);
            panel_logo.Name = "panel_logo";
            panel_logo.Size = new Size(83, 65);
            panel_logo.TabIndex = 8;
            panel_logo.Click += panel_logo_Click;
            panel_logo.MouseDown += panel_logo_MouseDown;
            panel_logo.MouseEnter += panel_logo_MouseEnter;
            panel_logo.MouseLeave += panel_logo_MouseLeave;
            panel_logo.MouseMove += panel_logo_MouseMove;
            panel_logo.MouseUp += panel_logo_MouseUp;
            // 
            // panel_title_system
            // 
            panel_title_system.Controls.Add(flow_layout_panel_title_system);
            panel_title_system.Dock = DockStyle.Right;
            panel_title_system.Location = new Point(792, 0);
            panel_title_system.Name = "panel_title_system";
            panel_title_system.Size = new Size(218, 65);
            panel_title_system.TabIndex = 5;
            // 
            // flow_layout_panel_title_system
            // 
            flow_layout_panel_title_system.Controls.Add(button_close);
            flow_layout_panel_title_system.Controls.Add(button_max);
            flow_layout_panel_title_system.Controls.Add(button_min);
            flow_layout_panel_title_system.Dock = DockStyle.Fill;
            flow_layout_panel_title_system.FlowDirection = FlowDirection.RightToLeft;
            flow_layout_panel_title_system.Location = new Point(0, 0);
            flow_layout_panel_title_system.Name = "flow_layout_panel_title_system";
            flow_layout_panel_title_system.Padding = new Padding(5);
            flow_layout_panel_title_system.Size = new Size(218, 65);
            flow_layout_panel_title_system.TabIndex = 9;
            flow_layout_panel_title_system.MouseDown += panel_title_MouseDown;
            flow_layout_panel_title_system.MouseEnter += panel_title_MouseEnter;
            flow_layout_panel_title_system.MouseLeave += panel_title_MouseLeave;
            flow_layout_panel_title_system.MouseMove += panel_title_MouseMove;
            flow_layout_panel_title_system.MouseUp += panel_title_MouseUp;
            // 
            // button_close
            // 
            button_close.BackColor = SystemColors.Control;
            button_close.FlatStyle = FlatStyle.Flat;
            button_close.Location = new Point(145, 8);
            button_close.Name = "button_close";
            button_close.Size = new Size(60, 51);
            button_close.TabIndex = 6;
            button_close.Text = "✕";
            button_close.UseVisualStyleBackColor = false;
            button_close.Click += button_close_Click;
            button_close.MouseDown += button_system_MouseDown;
            button_close.MouseEnter += button_system_MouseEnter;
            button_close.MouseLeave += button_system_MouseLeave;
            button_close.MouseMove += button_system_MouseMove;
            button_close.MouseUp += button_system_MouseUp;
            // 
            // button_max
            // 
            button_max.BackColor = SystemColors.Control;
            button_max.FlatStyle = FlatStyle.Flat;
            button_max.Location = new Point(79, 8);
            button_max.Name = "button_max";
            button_max.Size = new Size(60, 51);
            button_max.TabIndex = 5;
            button_max.Text = "☐";
            button_max.UseVisualStyleBackColor = false;
            button_max.Click += button_max_Click;
            button_max.MouseDown += button_system_MouseDown;
            button_max.MouseEnter += button_system_MouseEnter;
            button_max.MouseLeave += button_system_MouseLeave;
            button_max.MouseMove += button_system_MouseMove;
            button_max.MouseUp += button_system_MouseUp;
            // 
            // button_min
            // 
            button_min.BackColor = SystemColors.Control;
            button_min.FlatStyle = FlatStyle.Flat;
            button_min.Location = new Point(13, 8);
            button_min.Name = "button_min";
            button_min.Size = new Size(60, 51);
            button_min.TabIndex = 4;
            button_min.Text = "—";
            button_min.UseVisualStyleBackColor = false;
            button_min.Click += button_min_Click;
            button_min.MouseDown += button_system_MouseDown;
            button_min.MouseEnter += button_system_MouseEnter;
            button_min.MouseLeave += button_system_MouseLeave;
            button_min.MouseMove += button_system_MouseMove;
            button_min.MouseUp += button_system_MouseUp;
            // 
            // panel_base
            // 
            panel_base.Controls.Add(panel_body);
            panel_base.Controls.Add(panel_title);
            panel_base.Dock = DockStyle.Fill;
            panel_base.Location = new Point(10, 10);
            panel_base.Name = "panel_base";
            panel_base.Size = new Size(1010, 480);
            panel_base.TabIndex = 5;
            // 
            // panel_body
            // 
            panel_body.BackColor = Color.LightPink;
            panel_body.Dock = DockStyle.Fill;
            panel_body.Location = new Point(0, 65);
            panel_body.Name = "panel_body";
            panel_body.Size = new Size(1010, 415);
            panel_body.TabIndex = 6;
            // 
            // hahaha_form_base_titlebar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(1030, 500);
            Controls.Add(panel_base);
            Name = "hahaha_form_base_titlebar";
            Padding = new Padding(10);
            Text = "hahaha";
            Load += hahaha_form_base_titlebar_Load;
            Shown += hahaha_form_base_titlebar_Shown;
            panel_title.ResumeLayout(false);
            panel_title_system.ResumeLayout(false);
            flow_layout_panel_title_system.ResumeLayout(false);
            panel_base.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public Panel panel_title;
        public Panel panel_base;
        public Panel panel_body;
        public Panel panel_title_system;
        public Button button_close;
        public Button button_max;
        public Button button_min;
        public Label label_title;
        public Panel panel_logo;
        public FlowLayoutPanel flow_layout_panel_title_system;
    }
}