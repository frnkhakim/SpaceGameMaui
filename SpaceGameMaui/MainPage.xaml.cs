namespace SpaceGameMaui
{
    public partial class MainPage : ContentPage
    {
        GameState game = new GameState();
        GameDrawable drawable = new GameDrawable();

        public MainPage()
        {
            InitializeComponent();

            drawable.Game = game;
            GameView.Drawable = drawable;

            Dispatcher.StartTimer(TimeSpan.FromMilliseconds(16), () =>
            {
                game.Update();
                GameView.Invalidate();
                return true;
            });
        }

        private void OnLeft(object sender, EventArgs e)
        {
            game.Ship.MoveLeft();
        }

        private void OnRight(object sender, EventArgs e)
        {
            game.Ship.MoveRight();
        }

        private void OnShoot(object sender, EventArgs e)
        {
            game.Missiles.Add(
                new Missile(game.Ship.X, game.Ship.Y + 0.1) // 🚀 important
            );
        }
    }
}