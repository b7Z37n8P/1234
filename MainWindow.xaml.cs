using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace TimeDisplayApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _currentTime;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            _currentTime = TimeSpan.Zero;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromSeconds(1));
            txtDisplay.Text = _currentTime.ToString(@"hh\:mm\:ss");
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9:]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetTime_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSpan.TryParse(txtInput.Text, out TimeSpan newTime))
            {
                _currentTime = newTime;
                txtDisplay.Text = _currentTime.ToString(@"hh\:mm\:ss");
            }
            else
            {
                MessageBox.Show("Неверный формат времени. Введите чч:мм:сс");
            }
        }
    }
}