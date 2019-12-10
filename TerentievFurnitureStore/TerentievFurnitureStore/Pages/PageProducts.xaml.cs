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
    /// Interaction logic for PageProducts.xaml
    /// </summary>
    public partial class PageProducts : Page
    {
        public PageProducts()
        {
            InitializeComponent();
            UpdateData();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppData.WindowAdd = new WindowAddEdit(new PageAddProduct((Product)DGProducts.SelectedItem));
            AppData.WindowAdd.ShowDialog();
            UpdateData();
        }

        private void TBxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var products = AppData.context.Products.ToList();
            if (DPSearch.SelectedDate !=null)
            {
                products = products.Where(p => p.Date == DPSearch.SelectedDate.Value.Date).ToList();
            }
            if (!TBxSearch.Text.Equals(""))
                products = products.Where(p => p.Name.ToLower().Contains(TBxSearch.Text.ToLower())).ToList();
            if (products.Count == 0)
                DGProducts.Visibility = Visibility.Hidden;
            else
                DGProducts.Visibility = Visibility.Visible;
            DGProducts.ItemsSource = products;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                DGProducts.ItemsSource = AppData.context.Products.ToList();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppData.context.Products.Remove((Product)DGProducts.SelectedItem);
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
