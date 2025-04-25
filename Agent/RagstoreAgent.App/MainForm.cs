using RagstoreAgent.App.Core;
using RagstoreAgent.App.Services;

namespace RagstoreAgent.App;

public partial class MainForm : Form
{
    private readonly BotController _botController;
    private readonly ProxyService _proxyService;
    private bool _isRunning;

    public MainForm()
    {
        InitializeComponent();
        _botController = new BotController();
        _proxyService = new ProxyService();
    }

    private void InitializeComponent()
    {
        this.btnStart = new Button();
        this.btnStop = new Button();
        this.txtLog = new TextBox();
        
        // 
        // btnStart
        // 
        this.btnStart.Location = new Point(12, 12);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new Size(75, 23);
        this.btnStart.Text = "Iniciar";
        this.btnStart.Click += btnStart_Click;
        
        // 
        // btnStop
        // 
        this.btnStop.Location = new Point(93, 12);
        this.btnStop.Name = "btnStop";
        this.btnStop.Size = new Size(75, 23);
        this.btnStop.Text = "Parar";
        this.btnStop.Enabled = false;
        this.btnStop.Click += btnStop_Click;
        
        // 
        // txtLog
        // 
        this.txtLog.Location = new Point(12, 41);
        this.txtLog.Multiline = true;
        this.txtLog.Name = "txtLog";
        this.txtLog.ReadOnly = true;
        this.txtLog.ScrollBars = ScrollBars.Vertical;
        this.txtLog.Size = new Size(460, 308);
        
        // 
        // MainForm
        // 
        this.ClientSize = new Size(484, 361);
        this.Controls.Add(this.txtLog);
        this.Controls.Add(this.btnStop);
        this.Controls.Add(this.btnStart);
        this.Name = "MainForm";
        this.Text = "Ragstore Agent";
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        if (_isRunning) return;

        try
        {
            _isRunning = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            await _proxyService.StartAsync();
            Log("Proxy iniciado na porta 8080");

            await _botController.StartAsync();
            Log("Bot iniciado");
        }
        catch (Exception ex)
        {
            Log($"Erro ao iniciar: {ex.Message}");
            await StopAll();
        }
    }

    private async void btnStop_Click(object sender, EventArgs e)
    {
        await StopAll();
    }

    private async Task StopAll()
    {
        try
        {
            await _botController.StopAsync();
            await _proxyService.StopAsync();
        }
        finally
        {
            _isRunning = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            Log("Bot e proxy parados");
        }
    }

    private void Log(string message)
    {
        if (txtLog.InvokeRequired)
        {
            txtLog.Invoke(() => Log(message));
            return;
        }

        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
    }

    private Button btnStart;
    private Button btnStop;
    private TextBox txtLog;
} 