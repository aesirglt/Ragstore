namespace Totten.Solution.BotAgent.WinApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            tabControl1 = new TabControl();
            tab1 = new TabPage();
            pnlMenu = new Panel();
            txtServer = new TextBox();
            lblServer = new Label();
            btnStop = new Button();
            btnStart = new Button();
            btnInfos = new Button();
            button1 = new Button();
            pnlMain.SuspendLayout();
            tabControl1.SuspendLayout();
            pnlMenu.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(tabControl1);
            pnlMain.Controls.Add(pnlMenu);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(974, 635);
            pnlMain.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tab1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(203, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(771, 635);
            tabControl1.TabIndex = 1;
            // 
            // tab1
            // 
            tab1.Location = new Point(4, 24);
            tab1.Name = "tab1";
            tab1.Padding = new Padding(3);
            tab1.Size = new Size(763, 607);
            tab1.TabIndex = 0;
            tab1.Text = "Jogo 1";
            tab1.UseVisualStyleBackColor = true;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.White;
            pnlMenu.Controls.Add(button1);
            pnlMenu.Controls.Add(txtServer);
            pnlMenu.Controls.Add(lblServer);
            pnlMenu.Controls.Add(btnStop);
            pnlMenu.Controls.Add(btnStart);
            pnlMenu.Controls.Add(btnInfos);
            pnlMenu.Dock = DockStyle.Left;
            pnlMenu.Location = new Point(0, 0);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(203, 635);
            pnlMenu.TabIndex = 0;
            // 
            // txtServer
            // 
            txtServer.Dock = DockStyle.Bottom;
            txtServer.Location = new Point(0, 612);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(203, 23);
            txtServer.TabIndex = 4;
            txtServer.Text = "Ragexe";
            // 
            // lblServer
            // 
            lblServer.Anchor =  AnchorStyles.Bottom  |  AnchorStyles.Right ;
            lblServer.AutoSize = true;
            lblServer.Location = new Point(0, 594);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(110, 15);
            lblServer.TabIndex = 0;
            lblServer.Text = "Nome do processo:";
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.LightSkyBlue;
            btnStop.Dock = DockStyle.Top;
            btnStop.Enabled = false;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.ForeColor = SystemColors.ControlText;
            btnStop.Location = new Point(0, 96);
            btnStop.Margin = new Padding(0);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(203, 48);
            btnStop.TabIndex = 3;
            btnStop.Text = "Parar";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click +=  btnStop_Click ;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.LightSkyBlue;
            btnStart.Dock = DockStyle.Top;
            btnStart.Enabled = false;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.ForeColor = SystemColors.ControlText;
            btnStart.Location = new Point(0, 48);
            btnStart.Margin = new Padding(0);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(203, 48);
            btnStart.TabIndex = 2;
            btnStart.Text = "Iniciar";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click +=  btnStart_Click ;
            // 
            // btnInfos
            // 
            btnInfos.BackColor = Color.LightSkyBlue;
            btnInfos.Dock = DockStyle.Top;
            btnInfos.FlatAppearance.BorderSize = 0;
            btnInfos.FlatStyle = FlatStyle.Flat;
            btnInfos.ForeColor = SystemColors.ControlText;
            btnInfos.Location = new Point(0, 0);
            btnInfos.Margin = new Padding(0);
            btnInfos.Name = "btnInfos";
            btnInfos.Size = new Size(203, 48);
            btnInfos.TabIndex = 1;
            btnInfos.Text = "Carregar infos";
            btnInfos.UseVisualStyleBackColor = false;
            btnInfos.Click +=  btnInfos_Click ;
            // 
            // button1
            // 
            button1.BackColor = Color.LightSkyBlue;
            button1.Dock = DockStyle.Top;
            button1.Enabled = false;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(0, 144);
            button1.Margin = new Padding(0);
            button1.Name = "button1";
            button1.Size = new Size(203, 48);
            button1.TabIndex = 5;
            button1.Text = "Parar";
            button1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(974, 635);
            Controls.Add(pnlMain);
            Name = "Form1";
            Text = "Form1";
            pnlMain.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            pnlMenu.ResumeLayout(false);
            pnlMenu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlMenu;
        private TabControl tabControl1;
        private TabPage tab1;
        private Button btnInfos;
        private Button btnStart;
        private Button btnStop;
        private Label lblServer;
        private TextBox txtServer;
        private Button button1;
    }
}
