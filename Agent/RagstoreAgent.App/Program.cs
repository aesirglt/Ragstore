using RagstoreAgent.App.Core;

namespace RagstoreAgent.App;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        ApplicationConfiguration.Initialize();
        
        // Configurar manipulação de exceções não tratadas
        Application.ThreadException += (sender, e) => 
            MessageBox.Show($"Erro: {e.Exception.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        AppDomain.CurrentDomain.UnhandledException += (sender, e) => 
            MessageBox.Show($"Erro fatal: {e.ExceptionObject}", "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);

        Application.Run(new MainForm());
    }
}
