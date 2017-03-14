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
                btnStart.Content = "Start";
            }
            else
            {
                _timer.Start();
                btnStart.Content = "Stop";
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
                // Draw the current time.
                lblClock.Content = string.Format(_time.ToString("mm':'ss"));

                if (_time.TotalSeconds.Equals(0))
                {
                    RestartTimer();
                }

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
            if(_conf.Blinds.Count() >= currentRound)
            {
                lblSmallBlind.Content = _conf.Blinds[count].SmallBlind;
                lblBigBlind.Content = _conf.Blinds[count].BigBlind;
                lblAnte.Content = _conf.Blinds[count].Ante ?? string.Empty;
                count++;
            }
            // Get Next Round Info.
            if(_conf.Blinds.Count() > count)
            {  
                lblNextSmall.Content = _conf.Blinds[count].SmallBlind;
                lblNextBigBlind.Content = _conf.Blinds[count].BigBlind;
                lblNextRound.Content = count+1;
                lblNextAnte.Content = _conf.Blinds[count].Ante ?? string.Empty;
            }

        }

       
    }
}
