namespace Totten.Solution.BotAgent.WinApp.UserControls;
using System.Windows.Forms;

public partial class TabUC : UserControl
{
    private CharUC _charUC;
    public TabUC(CharUC charUC)
    {
        InitializeComponent();
        _charUC = charUC;
        SetComponents();
    }

    private void SetComponents()
    {
        _charUC.Dock = DockStyle.Fill;
        pnlCharInfo.Controls.Add(_charUC);
    }

    public void RefreshChar()
    {
        _charUC.LoadValues();
    }
}
