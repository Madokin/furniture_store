using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TerentievFurnitureStore.Pages
{
    /// <summary>
    /// Interaction logic for PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        DispatcherTimer _timer;
        public PageAuthorization()
        {
            InitializeComponent();
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "ru-RU":
                    BorderRu.BorderBrush = Brushes.Green;
                    BorderUK.BorderBrush = null;
                    break;
                case "en-US":
                    BorderRu.BorderBrush = null;
                    BorderUK.BorderBrush = Brushes.Green;
                    break;
            }
            _timer = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 1),
            };
            _timer.Tick += Timer_Tick;
            if (Properties.Settings.Default.TimeBan.Seconds > 0)
            {
                _timer.Start();
                Timer_Tick(null, null);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.TimeBan.Seconds != 0 || Properties.Settings.Default.TimeBan.Minutes == 1)
            {
                Properties.Settings.Default.TimeBan -= new TimeSpan(0, 0, 1);
                Properties.Settings.Default.Save();
                TBTime.Text = $"Привышено кол-во попыток ввода, следующая попытка будет доступна через {Properties.Settings.Default.TimeBan.Seconds} секунд";
            }
            else
            {
                BtnLogin.IsEnabled = true;
                TBTime.Text = $"";
                _timer.Stop();
            }
        }

        void LanguageChange(object sender, MouseButtonEventArgs e)
        {
            string culture = "";
            string border = (sender as Border).Name;
            switch (border)
            {
                case "BorderRu":
                    if (Thread.CurrentThread.CurrentUICulture.Name=="ru-RU")
                    { 
                        return;
                    } 
                    culture = "ru-Ru";
                    break;
                case "BorderUK":
                    if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                    {
                        return;
                    }
                    culture = "en-Us";
                    break;
            }
            Properties.Settings.Default.currentCulture = culture;
            Properties.Settings.Default.Save();
            Application.Current.Shutdown();
            Process.Start(App.ResourceAssembly.Location);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TBxLogin.Text.Equals("") && PBPassword.Password.Equals(""))
            {
                MessageBox.Show(Properties.Resources.ErrorNotEterned, Properties.Resources.CaptionError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Entities.User user = AppData.context.Users.Where(p => p.Login == TBxLogin.Text && p.Password == PBPassword.Password).FirstOrDefault();
                if (user == null)
                {
                    MessageBox.Show(Properties.Resources.ErrorInvalidLog, Properties.Resources.CaptionError, MessageBoxButton.OK, MessageBoxImage.Error);
                    Properties.Settings.Default.countTry++;
                    Properties.Settings.Default.Save();
                    if (Properties.Settings.Default.countTry == 3)
                    {
                        Properties.Settings.Default.TimeBan = new TimeSpan(0, 1, 0);
                        Properties.Settings.Default.countTry = 2;
                        Properties.Settings.Default.Save();
                        _timer.Start();
                        Timer_Tick(null, null);
                        BtnLogin.IsEnabled = false;
                    }
                    return;
                }
                AppData.currentUser = user;
                AppData.mainFrame.Navigate(new PageMenu());
                Properties.Settings.Default.countTry = 0;
                Properties.Settings.Default.Save();
            }
            catch
            {
                MessageBox.Show(Properties.Resources.ErrorNoConnect, Properties.Resources.CaptionError, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
