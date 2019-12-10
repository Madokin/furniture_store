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

namespace TerentievFurnitureStore.Pages
{
    /// <summary>
    /// Interaction logic for PageAddSale.xaml
    /// </summary>
    public partial class PageAddSale : Page
    {
        Sale _cs = null;
        public PageAddSale(Sale currentSale)
        {
            InitializeComponent();
            _cs = currentSale;
            BtnAddEdit.Content = Properties.Resources.BtnEdit;
        }

        public PageAddSale()
        {
            InitializeComponent();
            BtnAddEdit.Content = Properties.Resources.BtnAdd;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBxClient.ItemsSource = AppData.context.Clients.ToList();
            CBxProduct.ItemsSource = AppData.context.Products.ToList();
            if (_cs != null)
            {
                CBxClient.SelectedItem = _cs.Client;
                CBxProduct.SelectedItem = _cs.Product;
                DPDateOfSale.SelectedDate = _cs.DateOfSale;
                TBxQuantity.Text = _cs.Quantity.ToString();
                TBxPrice.Text = _cs.Price.ToString();
            }
        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            decimal price = 0;
            int quantity = 0;
            if (!(CBxClient.SelectedItem is Client))
                error.AppendLine(Properties.Resources.ErrorClient);
            if (!(CBxProduct.SelectedItem is Product))
                error.AppendLine(Properties.Resources.ErrorProduct);
            if (DPDateOfSale.SelectedDate == null)
                error.AppendLine(Properties.Resources.ErrorDateOfSale);
            if (string.IsNullOrWhiteSpace(TBxPrice.Text))
                error.AppendLine(Properties.Resources.ErrorPriceEmpty);
            else
                if (!decimal.TryParse(TBxPrice.Text, out price))
                error.AppendLine(Properties.Resources.ErrorPriceFormat);
            if (string.IsNullOrWhiteSpace(TBxQuantity.Text))
                error.AppendLine(Properties.Resources.ErrorQuantityEmpty);
            else
                 if (!int.TryParse(TBxQuantity.Text, out quantity))
                error.AppendLine(Properties.Resources.ErrorQuantityFormat);
            if (!error.ToString().Equals(""))
            {
                MessageBox.Show(Properties.Resources.ErrorSomethingWrong + "\n\n" + error, Properties.Resources.CaptionError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                if (_cs == null)
                {
                    Sale sale = new Sale()
                    {
                        idSale = AppData.context.Sales.Max(p => p.idSale) + 1,
                        Client = CBxClient.SelectedItem as Client,
                        Product = CBxProduct.SelectedItem as Product,
                        DateOfSale = (DateTime)DPDateOfSale.SelectedDate,
                        Quantity = quantity,
                        Price = price
                    };
                    AppData.context.Sales.Add(sale);
                    MessageBox.Show(Properties.Resources.MessageSuccessfullAdd, Properties.Resources.CaptionSuccessfully,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _cs.Client = CBxClient.SelectedItem as Client;
                    _cs.Product = CBxProduct.SelectedItem as Product;
                    _cs.DateOfSale = (DateTime)DPDateOfSale.SelectedDate;
                    _cs.Quantity = quantity;
                    _cs.Price = price;
                    MessageBox.Show(Properties.Resources.MessageSuccessfullEdit, Properties.Resources.CaptionSuccessfully,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                AppData.context.SaveChanges();
                AppData.WindowAdd.CloseDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.Resources.ErrorUnspecified + ex.Message, Properties.Resources.CaptionError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TBxQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buf = "";
            char[] array = (sender as TextBox).Text.ToCharArray();
            foreach (var item in array)
            {
                if (Char.IsDigit(item))
                    buf += item;
            }
            (sender as TextBox).Text = buf;
            (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
        }

        private void TBxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buf = "";
            bool dot = true;
            char[] array = (sender as TextBox).Text.ToCharArray();
            foreach (var item in array)
            {
                if (Char.IsDigit(item))
                    buf += item;
                if (item == '.' && dot)
                {
                    buf += item;
                    dot = false;
                }
            }
            (sender as TextBox).Text = buf;
            (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
        }
    }
}
