namespace SpaceGameMaui
{
    public class Minion
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double vx = -0.6;

        public Minion(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double deltaTime)
        {
            X += vx * deltaTime;

            if (X <= 0 || X >= 2)
            {
                X = Math.Clamp(X, 0, 2);
                vx = -vx;
                Y -= 0.08;
            }
        }
    }
}