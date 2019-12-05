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
    /// Interaction logic for PageSales.xaml
    /// </summary>
    public partial class PageSales : Page
    {
        public PageSales()
        {
            InitializeComponent();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppData.WindowAdd = new WindowAddEdit(new PageAddSale((Sale)DGSales.SelectedItem));
            AppData.WindowAdd.ShowDialog();
            UpdateData();
        }

        private void TBxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var sales = AppData.context.Sales.ToList();
            if (DPSearch.SelectedDate != null)
            {
                sales = sales.Where(p => p.DateOfSale == DPSearch.SelectedDate).ToList();
            }
            if (!TBxSearch.Text.Equals(""))
            {
                sales = sales.Where(p => p.Product.Name.ToLower().Contains(TBxSearch.Text.ToLower())
                || p.Client.Name.ToLower().Contains(TBxSearch.Text.ToLower())).ToList();
            }
            if (sales.Count == 0)
                DGSales.Visibility = Visibility.Hidden;
            else
                DGSales.Visibility = Visibility.Visible;
            DGSales.ItemsSource = sales;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                DGSales.ItemsSource = AppData.context.Sales.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppData.context.Sales.Remove((Sale)DGSales.SelectedItem);
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
