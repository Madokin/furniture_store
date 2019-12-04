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

namespace TerentievFurnitureStore.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = new TimeSpan(0, 0, 1)
        };
        DateTime _DTend = DateTime.Now;
        System.Drawing.Point _point = System.Windows.Forms.Control.MousePosition;
        string title;

        public MainWindow()
        {
            if (Properties.Settings.Default.currentCulture != "")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo
                    .GetCultureInfo(Properties.Settings.Default.currentCulture);
            }
            InitializeComponent();
            AppData.mainFrame = MainFrame;
            AppData.mainFrame.Navigate(new Pages.PageAuthorization());
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_point != System.Windows.Forms.Control.MousePosition)
            {
                _point = System.Windows.Forms.Control.MousePosition;
                _DTend = DateTime.Now;
            }
            if (DateTime.Now - _DTend >= new TimeSpan(0, 2, 0))
            {
                AppData.mainFrame.Navigate(new Pages.PageAuthorization());
                timer.Stop();
            }
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            title = (((Frame)sender).Content as Page).Title;
            if (title == "PageAuthorization")
            {
                BtnBack.Visibility = Visibility.Hidden;
                BtnLogout.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnLogout.Visibility = Visibility.Visible;
                timer.Start();
                Timer_Tick(null, null);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (title.Equals("PageMenu"))
            {
                if (MessageBox.Show(Properties.Resources.MessageExit, Properties.Resources.CaptionExit,
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    AppData.mainFrame.Navigate(new Pages.PageAuthorization());
                return;
            }
            AppData.mainFrame.GoBack();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.MessageExit, Properties.Resources.CaptionExit,
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                AppData.mainFrame.Navigate(new Pages.PageAuthorization());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
