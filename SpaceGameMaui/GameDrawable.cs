using Microsoft.Maui.Graphics;

namespace SpaceGameMaui
{
    public class GameDrawable : IDrawable
    {
        public GameState Game;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float width = dirtyRect.Width;
            float height = dirtyRect.Height;

            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(dirtyRect);

            // 🔥 Flip Y (VERY IMPORTANT)
            float FlipY(double y) => (float)(height - (y / 2.0 * height));

            // Ship
            canvas.FillColor = Colors.Blue;
            canvas.FillRectangle(
                (float)(Game.Ship.X / 2.0 * width),
                FlipY(Game.Ship.Y),
                40,
                40
            );

            // Minions
            canvas.FillColor = Colors.Red;
            foreach (var m in Game.Minions)
            {
                canvas.FillCircle(
                    (float)(m.X / 2.0 * width),
                    FlipY(m.Y),
                    10
                );
            }

            // Missiles
            canvas.FillColor = Colors.Yellow;
            foreach (var missile in Game.Missiles)
            {
                canvas.FillCircle(
                    (float)(missile.X / 2.0 * width),
                    FlipY(missile.Y),
                    5
                );
            }

            // Score
            canvas.FontColor = Colors.White;
            canvas.FontSize = 18;

            canvas.DrawString(
                $"Score: {Game.Score}",
                10, 10, 200, 50,
                HorizontalAlignment.Left,
                VerticalAlignment.Top
            );
        }
    }
}