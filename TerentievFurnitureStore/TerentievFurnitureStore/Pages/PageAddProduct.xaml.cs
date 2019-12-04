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
    /// Interaction logic for PageAddProduct.xaml
    /// </summary>
    public partial class PageAddProduct : Page
    {
        Product _cp = null;
        public PageAddProduct(Product currentProduct)
        {
            InitializeComponent();
            _cp = currentProduct;
            BtnAddEdit.Content = Properties.Resources.BtnEdit;
        }

        public PageAddProduct()
        {
            InitializeComponent();
            BtnAddEdit.Content = Properties.Resources.BtnAdd;
        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            float weight = 0;
            if (string.IsNullOrWhiteSpace(TBxWeight.Text))
                error.AppendLine(Properties.Resources.ErrorWeightEmpty);
            else
                if (!float.TryParse(TBxWeight.Text, out weight))
                    error.AppendLine(Properties.Resources.ErrorWeightFormat);
            if (DPProductionDate.SelectedDate == null)
                error.AppendLine(Properties.Resources.ErrorProductionDate);
            if (string.IsNullOrWhiteSpace(TBxProductName.Text))
                error.AppendLine(Properties.Resources.ErrorProductName);
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

        private void TBxWeight_TextChanged(object sender, TextChangedEventArgs e)
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
