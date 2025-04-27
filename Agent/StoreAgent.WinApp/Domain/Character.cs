namespace StoreAgent.WinApp.Domain
{
    public class Character
    {
        public Point CurrentPosition { get; private set; }
        private List<Point> pathPoints;
        private int currentPathIndex;
        private System.Timers.Timer movementTimer;
        private const int MOVEMENT_SPEED = 10; // pixels por tick

        public Character(int startX, int startY)
        {
            CurrentPosition = new Point(startX, startY);
            pathPoints = new List<Point>();
            currentPathIndex = 0;
            movementTimer = new System.Timers.Timer();
            movementTimer.Interval = 50; // 50ms entre cada movimento
            movementTimer.Elapsed += MovementTimer_Tick;
        }

        public void SetPath(List<Point> points)
        {
            pathPoints = points;
            currentPathIndex = 0;
        }

        public void StartMovement()
        {
            if (pathPoints.Count > 0)
            {
                movementTimer.Start();
            }
        }

        public void StopMovement()
        {
            movementTimer.Stop();
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (currentPathIndex >= pathPoints.Count)
            {
                movementTimer.Stop();
                return;
            }

            Point target = pathPoints[currentPathIndex];
            int dx = target.X - CurrentPosition.X;
            int dy = target.Y - CurrentPosition.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance <= MOVEMENT_SPEED)
            {
                CurrentPosition = target;
                currentPathIndex++;
            }
            else
            {
                double ratio = MOVEMENT_SPEED / distance;
                CurrentPosition = new Point(
                    CurrentPosition.X + (int)(dx * ratio),
                    CurrentPosition.Y + (int)(dy * ratio)
                );
            }
        }
    }
} 