namespace Totten.Solution.BotAgent.WinApp.UserControls;

using System.Diagnostics;
using System.Windows.Forms;
using Totten.Solution.BotAgent.Domain.Features.Characters;

public partial class CharUC : UserControl
{
    private ICharacterService _characterService;
    private Process _process;

    public CharUC(ICharacterService characterService, Process process)
    {
        InitializeComponent();
        _characterService = characterService;
        _process = process;
        LoadValues();
    }
    private void ProgressBar(ProgressBar progressBar, int hp, int hpMax)
    {
        if (hpMax <= 0) return;

        progressBar.Minimum = 1;
        progressBar.Maximum = hpMax;

        progressBar.Value = Math.Min(Math.Max(hp, progressBar.Minimum), progressBar.Maximum);
    }
    public void LoadValues()
    {
        //var character = _characterService.GetCharacter(_process);

        var character = new CPlayer
        {
            posX = _characterService.GetX(_process),
            posY = _characterService.GetY(_process),
            sp = _characterService.GetSp(_process),
            spMax = _characterService.GetSpMax(_process),
        };

        if(barSp.Maximum != character.spMax)
        {
            ProgressBar(barHp, character.hp, character.hpMax);
            ProgressBar(barSp, character.sp, character.spMax);
        }

        txtName.Text = $"{character.name}";
        txtClass.Text = $"{character.className}";
        txtLevel.Text = $"{character.level}/{character.classLevel}";
        txtWeight.Text = $"{character.weight}";
        txtZeny.Text = $"{character.zeny}";
        txtLocation.Text = $"{character.map} {character.posX},{character.posY}";

        barSp.Value = character.sp;

    }
}
