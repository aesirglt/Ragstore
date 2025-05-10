namespace Totten.Solution.BotAgent.WinApp.UserControls;

partial class TabUC
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
        pnlCharInfo = new Panel();
        SuspendLayout();
        // 
        // pnlCharInfo
        // 
        pnlCharInfo.Dock = DockStyle.Top;
        pnlCharInfo.Location = new Point(0, 0);
        pnlCharInfo.Name = "pnlCharInfo";
        pnlCharInfo.Size = new Size(763, 172);
        pnlCharInfo.TabIndex = 0;
        // 
        // TabUC
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(pnlCharInfo);
        Name = "TabUC";
        Size = new Size(763, 607);
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlCharInfo;
}
