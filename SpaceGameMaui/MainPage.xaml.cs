namespace SpaceGameMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly GameState game = new GameState();
        private readonly GameDrawable drawable = new GameDrawable();
        private readonly HighScoreService highScoreService = new HighScoreService();

        private DateTime lastFrameTime;
        private double elapsedSeconds;
        private bool isPlaying;
        private bool gameOverProcessed;

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

                if (isPlaying)
                {
                    elapsedSeconds += deltaTime;
                    game.Update(deltaTime);
                    UpdateHud();

                    if (game.IsGameOver && !gameOverProcessed)
                        ShowGameOver();
                }

                GameView.Invalidate();
                return true;
            });

            ShowIntro();
        }

        private void ShowIntro()
        {
            isPlaying = false;
            gameOverProcessed = false;
            IntroOverlay.IsVisible = true;
            GameOverOverlay.IsVisible = false;
            HudPanel.IsVisible = false;
            ControlsPanel.IsVisible = false;
            GameView.Invalidate();
        }

        private void StartNewGame()
        {
            game.Reset();
            elapsedSeconds = 0;
            gameOverProcessed = false;
            isPlaying = true;
            IntroOverlay.IsVisible = false;
            GameOverOverlay.IsVisible = false;
            HudPanel.IsVisible = true;
            ControlsPanel.IsVisible = true;
            UpdateHud();
            GameView.Invalidate();
        }

        private void UpdateHud()
        {
            ScoreLabel.Text = $"Score: {game.Score}";
            TimerLabel.Text = $"Time: {FormatTime(elapsedSeconds)}";
        }

        private void ShowGameOver()
        {
            isPlaying = false;
            gameOverProcessed = true;

            HudPanel.IsVisible = false;
            ControlsPanel.IsVisible = false;
            GameOverOverlay.IsVisible = true;

            if (game.IsWin)
            {
                var rank = highScoreService.RecordWinAndGetRank(game.Score, elapsedSeconds);
                GameOverTitleLabel.Text = "You Win!";
                var rankText = rank <= 10 ? $"Top 10 Rank: #{rank}" : $"Overall Rank: #{rank} (outside Top 10)";
                GameOverDetailsLabel.Text = $"Score: {game.Score}\nTime: {FormatTime(elapsedSeconds)}\n{rankText}";
            }
            else
            {
                GameOverTitleLabel.Text = "You Lost";
                GameOverDetailsLabel.Text = $"Score: {game.Score}\nTime: {FormatTime(elapsedSeconds)}";
            }

            var topTen = highScoreService.GetTop10Wins();
            if (topTen.Count == 0)
            {
                TopTenLabel.Text = "No wins recorded yet.";
                return;
            }

            var lines = topTen
                .Select((entry, index) =>
                    $"{index + 1,2}. {FormatTime(entry.TimeSeconds),8}  Score {entry.Score,3}")
                .ToList();

            TopTenLabel.Text = string.Join(Environment.NewLine, lines);
        }

        private static string FormatTime(double totalSeconds)
        {
            if (totalSeconds < 0)
                totalSeconds = 0;

            var time = TimeSpan.FromSeconds(totalSeconds);
            return $"{(int)time.TotalMinutes:00}:{time.Seconds:00}.{time.Milliseconds / 10:00}";
        }

        private void OnStartGame(object? sender, EventArgs e)
        {
            StartNewGame();
        }

        private void OnPlayAgain(object? sender, EventArgs e)
        {
            StartNewGame();
        }

        private void OnBackToIntro(object? sender, EventArgs e)
        {
            ShowIntro();
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
            if (isPlaying)
                game.Shoot();
        }
    }
}