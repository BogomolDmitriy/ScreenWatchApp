using System;
using System.Timers;

namespace ScreenWatchApp
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer _timer;
        private bool _ChangingTimes;

        public MainPage()
        {
            InitializeComponent();
            SetDynamicFontSize();
            StartTimer();
            _ChangingTimes = true;
        }

        private void SetDynamicFontSize()
        {
            // Получаем информацию об экране
            var displayInfo = DeviceDisplay.MainDisplayInfo;

            // Вычисляем размер шрифта на основе меньшей стороны экрана
            double screenWidth = displayInfo.Width / displayInfo.Density; // В логических единицах (DIP)
            double screenHeight = displayInfo.Height / displayInfo.Density; // В логических единицах (DIP)
            double minDimension = Math.Min(screenWidth, screenHeight);

            // Устанавливаем размер шрифта (примерно 50% от меньшей стороны)
            TimeLabel.FontSize = minDimension * 0.5; // 50% от размера экрана
        }

        private void StartTimer()
        {
            _timer = new System.Timers.Timer(1000); // Обновлять каждую секунду
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateTimes();
            });
        }

        private void UpdateTimes()
        {
            if (_ChangingTimes)
            {
                TimeLabel.Text = DateTime.Now.ToString("HH:mm");
                _ChangingTimes = false;
            }

            else
            {
                TimeLabel.Text = DateTime.Now.ToString("HH mm");
                _ChangingTimes = true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _timer?.Stop();
            _timer?.Dispose();
        }
    }

}
