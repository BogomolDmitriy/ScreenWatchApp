//using ScreenWatchApp.WinUI;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Timers;
//using Windows.Networking.NetworkOperators;

namespace ScreenWatchApp
{
    public partial class MainPage : ContentPage
    {
        private int HourLim;
        private int MinuteLim;
        private int screenRefreshTime = 500;

        private int cauntRestartHour;

        private bool intermediateBool;

        private DateTime startDate = new DateTime(2024, 12, 31);
        private DateTime endDate = new DateTime(2025 + 1, 01, 01);

        Random random = new Random();

        private System.Timers.Timer _timerT;
        //private System.Timers.Timer _timerF;
        //private CancellationTokenSource _cts;
        private bool _ChangingTimes;

        public MainPage()
        {
            InitializeComponent();
            SetDynamicFontSize();
            StartTimer();
            _ChangingTimes = true;
            HourLim = 25;
            MinuteLim = 61;
            intermediateBool = true;
            cauntRestartHour = 0;
        }

        //private void NewYear()
        //{
        //    year = DateTime.Now.Year;
        //}

        private void SetDynamicFontSize()
        {
            // Получаем информацию об экране
            var displayInfo = DeviceDisplay.MainDisplayInfo;

            // Вычисляем размер шрифта на основе меньшей стороны экрана
            double screenWidth = displayInfo.Width / displayInfo.Density; // В логических единицах (DIP)
            double screenHeight = displayInfo.Height / displayInfo.Density; // В логических единицах (DIP)
            double minDimension = Math.Min(screenWidth, screenHeight);

            // Устанавливаем размер шрифта (примерно 50% от меньшей стороны)
            //TimeLabel.FontSize = minDimension * 0.5;
            Hour1.FontSize = minDimension * 0.55;
            Hour2.FontSize = minDimension * 0.55;
            Sim.FontSize = minDimension * 0.55;
            Minute1.FontSize = minDimension * 0.55;
            Minute2.FontSize = minDimension * 0.55;
            Seconds.FontSize = minDimension * 0.25;
        }

        private void StartTimer()
        {
            _timerT = new System.Timers.Timer(screenRefreshTime); // Обновлять каждую секунду
            _timerT.Elapsed += OnTimedEvent;
            _timerT.Start();
        }

        //private void StartTimer()
        //{
        //    _cts = new CancellationTokenSource();

        //    Task.Run(async () =>
        //    {
        //        while (!_cts.IsCancellationRequested)
        //        {
        //            try
        //            {
        //                // Обновление UI в основном потоке
        //                MainThread.BeginInvokeOnMainThread(() =>
        //                {
        //                    UpdateTimes();
        //                });

        //                await Task.Delay(500); // Задержка 0.5 секунды
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Ошибка таймера: {ex.Message}");
        //                break;
        //            }
        //        }
        //    }, _cts.Token);
        //}

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                UpdateTimes();
            });
        }

        private void UpdateTimes()
        {
            DateTime now = DateTime.Now;

            if (intermediateBool)
            {
                Seconds.Text = now.ToString("ss");

                if (MinuteLim != now.Minute)
                {
                    string[] charactersMinute = now.ToString("mm").Select(c => c.ToString()).ToArray();
                    Minute1.Text = charactersMinute[0];
                    Minute2.Text = charactersMinute[1];
                    MinuteLim = now.Minute;


                    if (HourLim != now.Hour)
                    {
                        string[] charactersHour = now.ToString("HH").Select(c => c.ToString()).ToArray();
                        Hour1.Text = charactersHour[0];
                        Hour2.Text = charactersHour[1];
                        HourLim = now.Hour;
                        TimerVariable();

                        cauntRestartHour++;

                        if (cauntRestartHour >= 3)
                        {
                            Restart();
                        }

                    }
                }

                if (_ChangingTimes)
                {
                    Sim.TextColor = Colors.Black;
                    _ChangingTimes = false;
                }

                else
                {
                    Sim.TextColor = Colors.DarkGreen;
                    _ChangingTimes = true;
                }

                intermediateBool = false;
            }

            else
            {
                intermediateBool = true;
            }


            if (now.Date >= startDate.Date && now.Date <= endDate.Date)
            {
                Minute1.TextColor = RandColor();
                Minute2.TextColor = RandColor();

                Hour1.TextColor = RandColor();
                Hour2.TextColor = RandColor();

                Seconds.TextColor = RandColor();

                if (_ChangingTimes)
                {
                    Sim.TextColor = RandColor();
                }
            }
        }

        public Color RandColor()
        {
            byte red = (byte)random.Next(100,255);
            byte green = (byte)random.Next(150,255);
            byte blue = (byte)random.Next(0, 150);

            return Color.FromRgb(red, green, blue);
        }

        protected override void OnDisappearing()
        {
            StopTimer();
        }

        private void TimerVariable()
        {
            StopTimer();
            StartTimer();
        }

        private void StopTimer()
        {
            base.OnDisappearing();
            _timerT?.Stop();
            _timerT?.Dispose();
        }

        private void Restart()
        {
            if (Application.Current is App app)
            {
                app.RestartApp();
            }

            cauntRestartHour = 0;
        }

    }
}