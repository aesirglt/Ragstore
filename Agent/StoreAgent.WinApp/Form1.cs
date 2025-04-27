using StoreAgent.WinApp.Domain;
using StoreAgent.WinApp.Infra;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StoreAgent.WinApp
{
    public partial class Form1 : Form
    {
        private Character character;
        private List<Point> pathPoints;
        private MovementManager movementManager;
        private MemoryManager memoryManager;

        public Form1()
        {
            InitializeComponent();
            character = new Character(0, 0);
            pathPoints = new List<Point>();
            
            // Inicializa os gerenciadores de memória e movimento
            memoryManager = new MemoryManager();
            movementManager = new MovementManager(memoryManager);
            
            // Tenta conectar ao processo do Ragnarok
            if (!memoryManager.AttachToProcess())
            {
                MessageBox.Show("Não foi possível conectar ao processo do Ragnarok Online. Certifique-se de que o jogo está aberto.");
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                var currentPos = movementManager.GetCurrentPosition();
                textBox1.AppendText($"Posição atual do personagem: X={currentPos.x}, Y={currentPos.y}\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao analisar posição: {ex.Message}");
            }
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            if (pathPoints.Count == 0)
            {
                MessageBox.Show("Adicione pelo menos um ponto antes de mover o personagem.");
                return;
            }

            try
            {
                foreach (var point in pathPoints)
                {
                    if (!movementManager.WalkTo(point.X, point.Y))
                    {
                        MessageBox.Show($"Erro ao mover para o ponto X={point.X}, Y={point.Y}");
                        return;
                    }
                    Thread.Sleep(500); // Aguarda um pouco entre cada ponto
                }
                MessageBox.Show("Movimento concluído com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro durante o movimento: {ex.Message}");
            }
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            ProcessCoordinates(txtCoordinates.Text);
            txtCoordinates.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Desenhar o personagem
            e.Graphics.FillEllipse(Brushes.Blue, 
                character.CurrentPosition.X - 5, 
                character.CurrentPosition.Y - 5, 
                10, 10);
        }

        public void AddPathPoint(int x, int y)
        {
            pathPoints.Add(new Point(x, y));
            textBox1.AppendText($"Ponto adicionado: X={x}, Y={y}\r\n");
            Invalidate();
        }

        public void ProcessCoordinates(string coordinates)
        {
            try
            {
                string[] parts = coordinates.Split(',');
                if (parts.Length == 2)
                {
                    int x = int.Parse(parts[0].Trim());
                    int y = int.Parse(parts[1].Trim());
                    AddPathPoint(x, y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar coordenadas: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            memoryManager.Detach();
        }
    }
}
