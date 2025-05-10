namespace Totten.Solution.BotAgent.WinApp.UserControls;

partial class CharUC
{
    /// <summary> 
    /// Variável de designer necessária.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Limpar os recursos que estão sendo usados.
    /// </summary>
    /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && ( components != null ))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Código gerado pelo Designer de Componentes

    /// <summary> 
    /// Método necessário para suporte ao Designer - não modifique 
    /// o conteúdo deste método com o editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        boxPlayer = new GroupBox();
        txtLocation = new TextBox();
        lblLocation = new Label();
        txtZeny = new TextBox();
        lblZeny = new Label();
        txtWeight = new TextBox();
        lblWeight = new Label();
        barSp = new ProgressBar();
        label5 = new Label();
        barHp = new ProgressBar();
        label3 = new Label();
        txtLevel = new TextBox();
        label4 = new Label();
        txtClass = new TextBox();
        label2 = new Label();
        txtName = new TextBox();
        label1 = new Label();
        boxPlayer.SuspendLayout();
        SuspendLayout();
        // 
        // boxPlayer
        // 
        boxPlayer.Controls.Add(txtLocation);
        boxPlayer.Controls.Add(lblLocation);
        boxPlayer.Controls.Add(txtZeny);
        boxPlayer.Controls.Add(lblZeny);
        boxPlayer.Controls.Add(txtWeight);
        boxPlayer.Controls.Add(lblWeight);
        boxPlayer.Controls.Add(barSp);
        boxPlayer.Controls.Add(label5);
        boxPlayer.Controls.Add(barHp);
        boxPlayer.Controls.Add(label3);
        boxPlayer.Controls.Add(txtLevel);
        boxPlayer.Controls.Add(label4);
        boxPlayer.Controls.Add(txtClass);
        boxPlayer.Controls.Add(label2);
        boxPlayer.Controls.Add(txtName);
        boxPlayer.Controls.Add(label1);
        boxPlayer.Dock = DockStyle.Top;
        boxPlayer.Location = new Point(0, 0);
        boxPlayer.Name = "boxPlayer";
        boxPlayer.Size = new Size(763, 162);
        boxPlayer.TabIndex = 2;
        boxPlayer.TabStop = false;
        boxPlayer.Text = "Informações do personagem";
        // 
        // txtLocation
        // 
        txtLocation.Location = new Point(366, 82);
        txtLocation.Name = "txtLocation";
        txtLocation.ReadOnly = true;
        txtLocation.Size = new Size(148, 23);
        txtLocation.TabIndex = 17;
        // 
        // lblLocation
        // 
        lblLocation.AutoSize = true;
        lblLocation.Location = new Point(289, 90);
        lblLocation.Name = "lblLocation";
        lblLocation.Size = new Size(71, 15);
        lblLocation.TabIndex = 16;
        lblLocation.Text = "Localização:";
        // 
        // txtZeny
        // 
        txtZeny.Location = new Point(177, 82);
        txtZeny.Name = "txtZeny";
        txtZeny.ReadOnly = true;
        txtZeny.Size = new Size(103, 23);
        txtZeny.TabIndex = 15;
        // 
        // lblZeny
        // 
        lblZeny.AutoSize = true;
        lblZeny.Location = new Point(130, 90);
        lblZeny.Name = "lblZeny";
        lblZeny.Size = new Size(41, 15);
        lblZeny.TabIndex = 14;
        lblZeny.Text = "Zenys:";
        // 
        // txtWeight
        // 
        txtWeight.Location = new Point(55, 82);
        txtWeight.Name = "txtWeight";
        txtWeight.ReadOnly = true;
        txtWeight.Size = new Size(69, 23);
        txtWeight.TabIndex = 13;
        // 
        // lblWeight
        // 
        lblWeight.AutoSize = true;
        lblWeight.Location = new Point(6, 90);
        lblWeight.Name = "lblWeight";
        lblWeight.Size = new Size(35, 15);
        lblWeight.TabIndex = 12;
        lblWeight.Text = "Peso:";
        // 
        // barSp
        // 
        barSp.Location = new Point(318, 53);
        barSp.Name = "barSp";
        barSp.Size = new Size(196, 23);
        barSp.TabIndex = 11;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Location = new Point(289, 61);
        label5.Name = "label5";
        label5.Size = new Size(23, 15);
        label5.TabIndex = 10;
        label5.Text = "SP:";
        // 
        // barHp
        // 
        barHp.Location = new Point(318, 24);
        barHp.Name = "barHp";
        barHp.Size = new Size(196, 23);
        barHp.TabIndex = 9;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(286, 32);
        label3.Name = "label3";
        label3.Size = new Size(26, 15);
        label3.TabIndex = 8;
        label3.Text = "HP:";
        // 
        // txtLevel
        // 
        txtLevel.Location = new Point(225, 53);
        txtLevel.Name = "txtLevel";
        txtLevel.ReadOnly = true;
        txtLevel.Size = new Size(55, 23);
        txtLevel.TabIndex = 6;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(182, 61);
        label4.Name = "label4";
        label4.Size = new Size(37, 15);
        label4.TabIndex = 5;
        label4.Text = "Level:";
        // 
        // txtClass
        // 
        txtClass.Location = new Point(55, 53);
        txtClass.Name = "txtClass";
        txtClass.ReadOnly = true;
        txtClass.Size = new Size(121, 23);
        txtClass.TabIndex = 3;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(6, 61);
        label2.Name = "label2";
        label2.Size = new Size(43, 15);
        label2.TabIndex = 2;
        label2.Text = "Classe:";
        // 
        // txtName
        // 
        txtName.Location = new Point(55, 24);
        txtName.Name = "txtName";
        txtName.ReadOnly = true;
        txtName.Size = new Size(225, 23);
        txtName.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(6, 32);
        label1.Name = "label1";
        label1.Size = new Size(43, 15);
        label1.TabIndex = 0;
        label1.Text = "Nome:";
        // 
        // CharUserControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(boxPlayer);
        Name = "CharUserControl";
        Size = new Size(763, 172);
        boxPlayer.ResumeLayout(false);
        boxPlayer.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private GroupBox boxPlayer;
    private Label label1;
    private TextBox txtName;
    private TextBox txtClass;
    private Label label2;
    private Label label4;
    private TextBox txtLevel;
    private Label label3;
    private ProgressBar barHp;
    private ProgressBar barSp;
    private Label label5;
    private TextBox txtWeight;
    private Label lblWeight;
    private Label lblZeny;
    private TextBox txtZeny;
    private Label lblLocation;
    private TextBox txtLocation;
}
