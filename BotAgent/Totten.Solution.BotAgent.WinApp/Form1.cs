namespace Totten.Solution.BotAgent.WinApp
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Totten.Solution.BotAgent.Domain.Base;
    using Totten.Solution.BotAgent.Domain.Features.Characters;
    using Totten.Solution.BotAgent.Domain.Features.Sessions;
    using Totten.Solution.BotAgent.Infra.Memory;
    using Totten.Solution.BotAgent.ServiceApplication.Features;
    using Totten.Solution.BotAgent.WinApp.UserControls;

    public partial class Form1 : Form
    {
        private Process[] _games;
        private TabUC[] _tabs;
        private ICharacterService _characterService;
        private IMemoryReader _memoryReader;
        private CancellationTokenSource updateTabsCancellation;
        public Form1()
        {
            InitializeComponent();
            _memoryReader = new MemoryReader();
            _characterService = new CharacterService(_memoryReader);
        }

        private void btnInfos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServer.Text))
            {
                MessageBox.Show("Digite o nome do processo a ser adicionado");
                return;
            }
            _games = Process.GetProcessesByName(txtServer.Text);

            if (_games.Length <= 0)
            {
                MessageBox.Show("Nenhum processo encontrado com nome informado.");
                return;
            }

            _tabs = new TabUC[_games.Length];

            for (int i = 0; i < _games.Length; i++)
            {
                _tabs[i] = new TabUC(new CharUC(_characterService, _games[i]));
                tab1.Controls.Clear();
                tab1.Controls.Add(_tabs[i]);
            }

            btnStart.Enabled = true;
            btnInfos.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            updateTabsCancellation = new CancellationTokenSource();
            unsafe
            {
                IntPtr baseAddress = _games[0].MainModule.BaseAddress;
                IntPtr funcAddress = baseAddress + 0x70B2FD;

                var func = (FuncDelegate)Marshal.GetDelegateForFunctionPointer(funcAddress, typeof(FuncDelegate));

                void* eaxStruct = (void*)0x0C191CF0;

                func(eaxStruct);
            }
            Task.Run(async () =>
            {
                while (!updateTabsCancellation.IsCancellationRequested)
                {
                    foreach (var tab in _tabs)
                    {
                        tab.Invoke(() => tab.RefreshChar());
                    }

                    await Task.Delay(1000, updateTabsCancellation.Token);
                }
            }, updateTabsCancellation.Token);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            updateTabsCancellation.Cancel();
        }
    }
}
