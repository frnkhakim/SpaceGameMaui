namespace SpaceGameMaui
{
    public class Missile
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double speed = 0.05;

        public Missile(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Update()
        {
            Y += speed; // 🚀 UP (game logic)
        }

        public bool IsOffScreen()
        {
            return Y > 2.0;
        }
    }
}