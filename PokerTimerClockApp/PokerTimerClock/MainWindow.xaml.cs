using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using System.Timers;
using Business;

namespace PokerTimerClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan _time;
        private DispatcherTimer _timer = new DispatcherTimer();
        private int currentRound = 0;
        private Configuration _conf;
        private int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            Initialize();
            RestartTimer();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            TimerStartStop();
        }

        private void TimerStartStop()
        {
            // Verify if the timer is running to stablish the buttons Labels.
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                btnStart.Content = ">";
            }
            else
            {
                _timer.Start();
                btnStart.Content = "||";
            }

        }

        private void Initialize()
        {
            _conf = ConfigurationLoader.GetConfiguration();

            _timer.Interval = new TimeSpan(TimeSpan.TicksPerSecond);
            _timer.Tick += (s, a) =>
            {
                // Substract a second.
                _time = _time.Subtract(new TimeSpan(0, 0, 1));

                if (_time.TotalSeconds.Equals(0))
                {
                    RestartTimer();
                }

                // Draw the current time and color.
                UpdateClockColor();
                lblClock.Content = string.Format(_time.ToString("mm':'ss"));

            };
        }

        private void RestartTimer()
        {
            _time = _conf.RoundTime;
            lblClock.Content = _time.ToString("mm':'ss");
            currentRound++;
            GetRoundInfo(currentRound);
        }

        private void GetRoundInfo(int currentRound)
        {
            if (_conf.Blinds.Count() >= currentRound)
            {
                lblSmallBlind.Content = _conf.Blinds[count].SmallBlind;
                if (!string.IsNullOrEmpty(_conf.Blinds[count].Ante))
                {
                    lblBig.Content = "Big Blind";
                    lblBigBlind.Content = _conf.Blinds[count].BigBlind;
                    lblAnteTxt.Content = "Ante";
                    lblAnte.Content = _conf.Blinds[count].Ante;
                }
                else
                {
                    lblBig.Content = string.Empty;
                    lblAnteTxt.Content = "Big Blind";
                    lblAnte.Content = _conf.Blinds[count].BigBlind;
                }
                count++;
            }
            // Get Next Round Info.
            if (_conf.Blinds.Count() > count)
            {
                lblNextSmall.Content = _conf.Blinds[count].SmallBlind;
                lblNextBigBlind.Content = _conf.Blinds[count].BigBlind;
                lblNextRound.Content = count + 1;
                lblNextAnte.Content = _conf.Blinds[count].Ante ?? string.Empty;
            }

        }

        private void UpdateClockColor()
        {
            var leftTime = (_conf.RoundTime - _time).TotalSeconds;
            var totalTime = _conf.RoundTime.TotalSeconds;
            var red = (leftTime / totalTime) * 255;
            var green = (totalTime - leftTime) / totalTime * 255;
            var blue = 0;

            var color = Color.FromRgb((byte)red, (byte)green, (byte)blue);
            var brush = new SolidColorBrush(color);

            lblClock.Foreground = brush;


        }

        private void lblPlus_Click(object sender, RoutedEventArgs e)
        {
            var plus = 20;
            var total = 50;

            List<int> milista = new List<int>();
            for(int plus1 = 1; plus1 < plus; plus1++)
            {
                total = (total*plus1) / 2;
                var money = Math.Round(total / 100d)*100;
                milista.Add((int)money);

            }

            foreach(var t in milista)
            {

            }
            
        }
    }
}
