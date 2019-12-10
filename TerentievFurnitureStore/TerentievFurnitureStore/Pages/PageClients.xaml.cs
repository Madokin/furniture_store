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
            if (DPSearch.SelectedDate != null)
            {
                clients = clients.Where(p => p.DateOfBirth == DPSearch.SelectedDate).ToList();
            }
            if (!TBxSearch.Text.Equals(""))
                clients = clients.Where(p => p.Name.ToLower().Contains(TBxSearch.Text.ToLower())).ToList();
            if (clients.Count == 0)
                DGClients.Visibility = Visibility.Hidden;
            else
                DGClients.Visibility = Visibility.Visible;
            DGClients.ItemsSource = clients;
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

        private void DPSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            DPSearch.SelectedDate = null;
            UpdateData();
        }

    }
}
