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
            StartTimer();
            _ChangingTimes = true;
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
