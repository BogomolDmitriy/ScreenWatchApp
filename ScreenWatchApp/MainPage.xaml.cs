using System;
using System.Timers;

namespace ScreenWatchApp
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer _timer;

        public MainPage()
        {
            InitializeComponent();
            StartTimer();
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
                TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            });
        }

        private void OnUpdateTimeClicked(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _timer?.Stop();
            _timer?.Dispose();
        }
    }

}
