namespace StoreAgent.WinApp
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            textBox1 = new TextBox();
            btnAnalyze = new Button();
            BtnMove = new Button();
            txtCoordinates = new TextBox();
            btnAddPoint = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(820, 672);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(BtnMove);
            tabPage1.Controls.Add(btnAnalyze);
            tabPage1.Controls.Add(txtCoordinates);
            tabPage1.Controls.Add(btnAddPoint);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(812, 644);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(192, 72);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(3, 148);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(806, 490);
            textBox1.TabIndex = 0;
            // 
            // btnAnalyze
            // 
            btnAnalyze.Location = new Point(6, 6);
            btnAnalyze.Name = "btnAnalyze";
            btnAnalyze.Size = new Size(118, 34);
            btnAnalyze.TabIndex = 1;
            btnAnalyze.Text = "analyze";
            btnAnalyze.UseVisualStyleBackColor = true;
            btnAnalyze.Click +=  btnAnalyze_Click ;
            // 
            // BtnMove
            // 
            BtnMove.Location = new Point(130, 6);
            BtnMove.Name = "BtnMove";
            BtnMove.Size = new Size(118, 34);
            BtnMove.TabIndex = 2;
            BtnMove.Text = "move";
            BtnMove.UseVisualStyleBackColor = true;
            BtnMove.Click +=  BtnMove_Click ;
            // 
            // txtCoordinates
            // 
            txtCoordinates.Location = new Point(12, 41);
            txtCoordinates.Name = "txtCoordinates";
            txtCoordinates.Size = new Size(100, 23);
            txtCoordinates.TabIndex = 3;
            // 
            // btnAddPoint
            // 
            btnAddPoint.Location = new Point(118, 41);
            btnAddPoint.Name = "btnAddPoint";
            btnAddPoint.Size = new Size(50, 23);
            btnAddPoint.TabIndex = 4;
            btnAddPoint.Text = "Add";
            btnAddPoint.UseVisualStyleBackColor = true;
            btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 680);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button BtnMove;
        private Button btnAnalyze;
        private TextBox textBox1;
        private TabPage tabPage2;
        private TextBox txtCoordinates;
        private Button btnAddPoint;
    }
}
