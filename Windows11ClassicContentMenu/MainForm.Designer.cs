namespace Windows11ClassicContentMenu
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnClassic = new System.Windows.Forms.Button();
            this.BtnDefault = new System.Windows.Forms.Button();
            this.BtnRestartExplorer = new System.Windows.Forms.Button();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.LanguageSelector = new System.Windows.Forms.ToolStripComboBox();
            this.TopMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuItemGithub = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuItemLogDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuItemLogClear = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.TopMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.BtnClassic);
            this.flowLayoutPanel1.Controls.Add(this.BtnDefault);
            this.flowLayoutPanel1.Controls.Add(this.BtnRestartExplorer);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // BtnClassic
            // 
            resources.ApplyResources(this.BtnClassic, "BtnClassic");
            this.BtnClassic.Name = "BtnClassic";
            this.BtnClassic.UseVisualStyleBackColor = true;
            this.BtnClassic.Click += new System.EventHandler(this.BtnClassic_Click);
            // 
            // BtnDefault
            // 
            this.BtnDefault.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.BtnDefault, "BtnDefault");
            this.BtnDefault.Name = "BtnDefault";
            this.BtnDefault.UseVisualStyleBackColor = true;
            this.BtnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
            // 
            // BtnRestartExplorer
            // 
            resources.ApplyResources(this.BtnRestartExplorer, "BtnRestartExplorer");
            this.BtnRestartExplorer.Name = "BtnRestartExplorer";
            this.BtnRestartExplorer.UseVisualStyleBackColor = true;
            this.BtnRestartExplorer.Click += new System.EventHandler(this.BtnRestartExplorer_Click);
            // 
            // TopMenu
            // 
            this.TopMenu.BackColor = System.Drawing.SystemColors.Control;
            this.TopMenu.GripMargin = new System.Windows.Forms.Padding(0);
            this.TopMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LanguageSelector,
            this.TopMenuHelp});
            resources.ApplyResources(this.TopMenu, "TopMenu");
            this.TopMenu.Name = "TopMenu";
            // 
            // LanguageSelector
            // 
            this.LanguageSelector.BackColor = System.Drawing.SystemColors.Control;
            this.LanguageSelector.Name = "LanguageSelector";
            resources.ApplyResources(this.LanguageSelector, "LanguageSelector");
            // 
            // TopMenuHelp
            // 
            this.TopMenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TopMenuItemGithub,
            this.TopMenuItemLogDirectory,
            this.TopMenuItemLogClear,
            this.TopMenuItemAbout});
            this.TopMenuHelp.Name = "TopMenuHelp";
            resources.ApplyResources(this.TopMenuHelp, "TopMenuHelp");
            // 
            // TopMenuItemGithub
            // 
            this.TopMenuItemGithub.Name = "TopMenuItemGithub";
            resources.ApplyResources(this.TopMenuItemGithub, "TopMenuItemGithub");
            this.TopMenuItemGithub.Click += new System.EventHandler(this.TopMenuItemGithub_Click);
            // 
            // TopMenuItemLogDirectory
            // 
            this.TopMenuItemLogDirectory.Name = "TopMenuItemLogDirectory";
            resources.ApplyResources(this.TopMenuItemLogDirectory, "TopMenuItemLogDirectory");
            this.TopMenuItemLogDirectory.Click += new System.EventHandler(this.TopMenuItemLogDirectory_Click);
            // 
            // TopMenuItemLogClear
            // 
            this.TopMenuItemLogClear.Name = "TopMenuItemLogClear";
            resources.ApplyResources(this.TopMenuItemLogClear, "TopMenuItemLogClear");
            this.TopMenuItemLogClear.Click += new System.EventHandler(this.TopMenuItemLogClear_Click);
            // 
            // TopMenuItemAbout
            // 
            this.TopMenuItemAbout.Name = "TopMenuItemAbout";
            resources.ApplyResources(this.TopMenuItemAbout, "TopMenuItemAbout");
            this.TopMenuItemAbout.Click += new System.EventHandler(this.TopMenuItemAbout_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.TopMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.TopMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button BtnClassic;
        private System.Windows.Forms.Button BtnDefault;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripComboBox LanguageSelector;
        private System.Windows.Forms.ToolStripMenuItem TopMenuHelp;
        private System.Windows.Forms.Button BtnRestartExplorer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem TopMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem TopMenuItemGithub;
        private System.Windows.Forms.ToolStripMenuItem TopMenuItemLogDirectory;
        private System.Windows.Forms.ToolStripMenuItem TopMenuItemLogClear;
    }
}

