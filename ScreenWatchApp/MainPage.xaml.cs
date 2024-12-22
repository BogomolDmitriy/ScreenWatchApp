using System;
using System.Timers;
//using Windows.Networking.NetworkOperators;

namespace ScreenWatchApp
{
    public partial class MainPage : ContentPage
    {
        private int HourLim;
        private int MinuteLim;

        private DateTime startDate = new DateTime(2024, 12, 31);
        private DateTime endDate = new DateTime(2025 + 1, 01, 01);

        Random random = new Random();

        //private System.Timers.Timer _timer;
        private CancellationTokenSource _cts;
        private bool _ChangingTimes;

        public MainPage()
        {
            InitializeComponent();
            SetDynamicFontSize();
            StartTimer();
            _ChangingTimes = true;
            HourLim = 25;
            MinuteLim = 61;
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

        //private void StartTimer()
        //{
        //    _timer = new System.Timers.Timer(1000); // Обновлять каждую секунду
        //    _timer.Elapsed += OnTimedEvent;
        //    _timer.Start();
        //}

        private void StartTimer()
        {
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    try
                    {
                        // Обновление UI в основном потоке
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            UpdateTimes();
                        });

                        await Task.Delay(1000); // Задержка 1 секунда
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка таймера: {ex.Message}");
                        break;
                    }
                }
            }, _cts.Token);
        }

        //private void OnTimedEvent(object sender, ElapsedEventArgs e)
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        UpdateTimes();
        //    });
        //}

        private void UpdateTimes()
        {
            DateTime now = DateTime.Now;
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
            byte red = (byte)random.Next(0, 256);
            byte green = (byte)random.Next(0, 256);
            byte blue = (byte)random.Next(0, 256);

            return Color.FromRgb(red, green, blue);
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    _timer?.Stop();
        //    _timer?.Dispose();
        //}
    }

}
