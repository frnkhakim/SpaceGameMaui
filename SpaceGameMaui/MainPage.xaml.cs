namespace SpaceGameMaui
{
    public partial class MainPage : ContentPage
    {
        GameState game = new GameState();
        GameDrawable drawable = new GameDrawable();
        DateTime lastFrameTime;

        public MainPage()
        {
            InitializeComponent();

            drawable.Game = game;
            GameView.Drawable = drawable;

            lastFrameTime = DateTime.UtcNow;

            Dispatcher.StartTimer(TimeSpan.FromMilliseconds(16), () =>
            {
                var now = DateTime.UtcNow;
                var deltaTime = (now - lastFrameTime).TotalSeconds;
                lastFrameTime = now;

                game.Update(deltaTime);
                GameView.Invalidate();
                return true;
            });
        }

        private void OnLeftPressed(object? sender, EventArgs e)
        {
            game.IsLeftPressed = true;
        }

        private void OnLeftReleased(object? sender, EventArgs e)
        {
            game.IsLeftPressed = false;
        }

        private void OnRightPressed(object? sender, EventArgs e)
        {
            game.IsRightPressed = true;
        }

        private void OnRightReleased(object? sender, EventArgs e)
        {
            game.IsRightPressed = false;
        }

        private void OnShoot(object? sender, EventArgs e)
        {
            game.Shoot();
        }
    }
}