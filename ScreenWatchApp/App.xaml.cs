namespace ScreenWatchApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public void RestartApp()
        {
            // Перезавантаження головної сторінки
            MainPage = new MainPage();
        }
    }
}
