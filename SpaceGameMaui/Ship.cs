namespace SpaceGameMaui
{
    public class Ship
    {
        public double X { get; set; } = 1.0;
        public double Y { get; set; } = 0.1;

        private const double MoveSpeed = 1.25;

        public void Update(double deltaTime, bool moveLeft, bool moveRight)
        {
            double direction = 0;
            if (moveLeft)
                direction -= 1;
            if (moveRight)
                direction += 1;

            X += direction * MoveSpeed * deltaTime;
            X = Math.Clamp(X, 0, 2);
        }

        public void MoveLeft()
        {
            X -= 0.1;
            if (X < 0) X = 0;
        }

        public void MoveRight()
        {
            X += 0.1;
            if (X > 2) X = 2;
        }
    }
}