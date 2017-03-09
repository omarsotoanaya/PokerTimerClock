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

namespace PokerTimerClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan time = TimeSpan.Parse("20:00:00");

        public MainWindow()
        {
            InitializeComponent();
            lblClock.Content = time.ToString();
        }
 
        private void button_Click(object sender, RoutedEventArgs e)
        {
                      
            if(timer.IsEnabled)
            {
                timer.Stop();
                btnStart.Content = "Start";
            }
            else
            {
                timer.Start();
                btnStart.Content = "Stop";               
            }

            timer.Interval = new TimeSpan(TimeSpan.TicksPerMillisecond);
            timer.Tick += (s, a) =>
            {
                //var hours = DateTime.Now.Hour;
                //var minutes = DateTime.Now.Minute;
                //var seconds = DateTime.Now.Second;

                //var clocktext = string.Format("{0}:{1}:{2}", hours, minutes, seconds);
                time = time.Subtract(new TimeSpan(0, 0, 1));

                var clocktext = time.ToString();

                if(time.TotalMinutes.Equals(0) && time.TotalSeconds.Equals(0))
                {
                    timer.Stop();
                }

                lblClock.Content = clocktext;

            };

        }
    }
}
