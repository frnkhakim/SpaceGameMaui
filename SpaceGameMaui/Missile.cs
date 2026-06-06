namespace SpaceGameMaui
{
    public class Missile
    {
        public double X { get; set; }
        public double Y { get; set; }

        private double speed = 3.0;

        public Missile(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Update(double deltaTime)
        {
            Y += speed * deltaTime;
        }

        public bool IsOffScreen()
        {
            return Y > 2.0;
        }
    }
}