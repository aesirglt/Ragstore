﻿namespace Totten.Solution.BotAgent.WinApp.UserControls;

partial class ConfigurationUC
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
        lblConfiguration = new Label();
        SuspendLayout();
        // 
        // lblConfiguration
        // 
        lblConfiguration.AutoSize = true;
        lblConfiguration.Location = new Point(3, 12);
        lblConfiguration.Name = "lblConfiguration";
        lblConfiguration.Size = new Size(38, 15);
        lblConfiguration.TabIndex = 0;
        lblConfiguration.Text = "label1";
        // 
        // ConfigurationUC
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(lblConfiguration);
        Name = "ConfigurationUC";
        Size = new Size(763, 172);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblConfiguration;
}
