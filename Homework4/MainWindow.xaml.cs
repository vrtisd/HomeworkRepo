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
using System.Windows.Threading;

namespace Homework4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.Address address = new Models.Address();
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            uxZipCode.Focus();

            uxMessage.Visibility = Visibility.Hidden;

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 3);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            uxMessage.Visibility = Visibility.Hidden;
            timer.Stop();
        }

        public override void EndInit()
        {
            base.EndInit();

            uxGrid.DataContext = address;
        }

        private void uxSubmit_Click(object sender, RoutedEventArgs e)
        {
            uxMessage.Visibility = Visibility.Visible;
            timer.Start();
        }
    }
}
