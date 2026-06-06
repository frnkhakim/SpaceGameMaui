namespace SpaceGameMaui
{
    public partial class SplashPage : ContentPage
    {
        private bool hasNavigated;

        public SplashPage()
        {
            InitializeComponent();
            SplashAnimation.AnimationCompleted += OnAnimationCompleted;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnAnimationCompleted(object? sender, EventArgs e)
        {
            await NavigateToShellAsync();
        }

        private async Task NavigateToShellAsync()
        {
            if (hasNavigated)
                return;

            hasNavigated = true;

            if (SplashAnimation is not null)
                await SplashAnimation.FadeTo(0, 250, Easing.CubicInOut);

            var window = Window;
            if (window is null)
                return;

            var shell = new AppShell
            {
                Opacity = 0
            };

            window.Page = shell;
            await shell.FadeTo(1, 350, Easing.CubicInOut);
        }
    }
}
