namespace SpaceGameMaui
{
    public class Ship
    {
        public double X { get; set; } = 1.0;
        public double Y { get; set; } = 0.1;

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