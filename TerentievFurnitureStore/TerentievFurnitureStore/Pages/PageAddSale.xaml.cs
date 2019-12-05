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
            CBxClient.SelectedItem = _cs.Client;
            CBxProduct.SelectedItem = _cs.Product;
            DPDateOfSale.SelectedDate = _cs.DateOfSale;
            TBxQuantity.Text = _cs.Quantity.ToString();
            TBxPrice.Text = _cs.Price.ToString();
        }

        public PageAddSale()
        {
            InitializeComponent();
        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            decimal price = 0;
            int quantity = 0;
            if (!(CBxClient.SelectedItem is Client))
                error.AppendLine(Properties.Resources.);
            if (!(CBxProduct.SelectedItem is Product))
                error.AppendLine(Properties.Resources.);
            if (DPDateOfSale.SelectedDate == null)
                error.AppendLine(Properties.Resources.);
            if (string.IsNullOrWhiteSpace(TBxPrice.Text))
                error.AppendLine(Properties.Resources.);
            else
                if (!decimal.TryParse(TBxPrice.Text, out price))
                error.AppendLine(Properties.Resources.);
            if (string.IsNullOrWhiteSpace(TBxQuantity.Text))
                error.AppendLine(Properties.Resources.);
            else
                 if (!int.TryParse(TBxQuantity.Text, out quantity))
                error.AppendLine(Properties.Resources.);
            if (!error.ToString().Equals(""))
            {
                MessageBox.Show(Properties.Resources.ErrorSomethingWrong + "\n\n" + error, Properties.Resources.CaptionError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                if (_cp == null)
                {
                    Product product = new Product()
                    {
                        idProduct = AppData.context.Products.Max(p => p.idProduct) + 1,
                        Name = TBxProductName.Text,
                        Date = (DateTime)DPProductionDate.SelectedDate,
                        Weight = weight
                    };
                    AppData.context.Products.Add(product);
                    MessageBox.Show(Properties.Resources.MessageSuccessfullAdd, Properties.Resources.CaptionSuccessfully,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _cp.Name = TBxProductName.Text;
                    _cp.Date = (DateTime)DPProductionDate.SelectedDate;
                    _cp.Weight = weight;
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
    }
}
