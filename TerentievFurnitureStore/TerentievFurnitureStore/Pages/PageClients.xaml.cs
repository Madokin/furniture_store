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
using TerentievFurnitureStore.Entities;
using TerentievFurnitureStore.Windows;

namespace TerentievFurnitureStore.Pages
{
    /// <summary>
    /// Interaction logic for PageClients.xaml
    /// </summary>
    public partial class PageClients : Page
    {
        public PageClients()
        {
            InitializeComponent();
            UpdateData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppData.WindowAdd = new WindowAddEdit(new PageAddClient((Client)DGClients.SelectedItem));
            AppData.WindowAdd.ShowDialog();
            UpdateData();
        }

        private void TBxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var clients = AppData.context.Clients.ToList();
            if (CBxSearch.SelectedIndex != 0)
            {
                clients = clients.Where(p => p.Gender == CBxSearch.SelectedItem).ToList();
            }
            if (!TBxSearch.Text.Equals(""))
                clients = clients.Where(p => p.Name.ToLower().Contains(TBxSearch.Text.ToLower())).ToList();
            if (clients.Count == 0)
            {
                TBNothing.Visibility = Visibility.Visible;
                SVDataGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                TBNothing.Visibility = Visibility.Hidden;
                SVDataGrid.Visibility = Visibility.Visible;
                DGClients.ItemsSource = clients;
            }
            if (clients.Count>6)
            {
                SVDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else
            {
                SVDataGrid.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                DGClients.ItemsSource = AppData.context.Clients.ToList();
            }
        }

        private void CBxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppData.context.Clients.Remove((Client)DGClients.SelectedItem);
                AppData.context.SaveChanges();
            }
            UpdateData();
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            AppData.WindowAdd = new WindowAddEdit(new PageAddClient());
            AppData.WindowAdd.ShowDialog();
            UpdateData();
        }

        private void DPSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
