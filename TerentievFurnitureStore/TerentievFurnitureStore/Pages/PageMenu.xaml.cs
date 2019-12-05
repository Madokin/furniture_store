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
using TerentievFurnitureStore.Windows;

namespace TerentievFurnitureStore.Pages
{
    /// <summary>
    /// Interaction logic for PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
        }

        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            AppData.mainFrame.Navigate(new PageClients());
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            AppData.mainFrame.Navigate(new PageProducts());
        }

        private void BtnSales_Click(object sender, RoutedEventArgs e)
        {
            AppData.mainFrame.Navigate(new PageSales());
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            OpenAddWindow(new PageAddClient());
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            OpenAddWindow(new PageAddProduct());
        }

        private void BtnAddSale_Click(object sender, RoutedEventArgs e)
        {
            OpenAddWindow(new PageAddSale());
        }

        void OpenAddWindow(Page page)
        {
            AppData.WindowAdd = new WindowAddEdit(page);
            AppData.WindowAdd.ShowDialog();
        }
    }
}
