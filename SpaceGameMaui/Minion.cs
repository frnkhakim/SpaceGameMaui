namespace SpaceGameMaui
{
    public class Minion
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double vx = -0.01;

        public Minion(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move()
        {
            X += vx;

            if (X <= 0 || X >= 2)
            {
                vx = -vx;
                Y -= 0.1;
            }
        }
    }
}