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

namespace PokerTimerClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan _time;
        private DispatcherTimer _timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            // Get the timer ticks.
            TimerTicks();
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Verify if the clock is running.
            TimerStatus();  
        }

        private void TimerStatus()
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

        private void TimerTicks()
        {
            // Initialize round.
            Round round = new Round();

            _time = round.GetRoundData().RoundTime;
            string clocktext = string.Format("{0:00}:{1:00}", _time.Minutes, _time.Seconds);

            // Set the First Time Clock.
            lblClock.Content = clocktext;

            // Set the interval and the tick.
            _timer.Interval = new TimeSpan(TimeSpan.TicksPerSecond);
            _timer.Tick += (s, a) =>
            {
                // Substract a second.
                _time = _time.Subtract(new TimeSpan(0, 0, 1));

                clocktext = _time.ToString("mm':'ss");

                if (_time.Minutes.Equals(10))
                {
                    lblClock.Foreground = Brushes.Yellow;
                }
                if (_time.TotalMinutes.Equals(0) && _time.TotalSeconds.Equals(0))
                {
                    _timer.Stop();
                }

                // Draw the current time.
                lblClock.Content = clocktext;
            };
        }

    }
}
