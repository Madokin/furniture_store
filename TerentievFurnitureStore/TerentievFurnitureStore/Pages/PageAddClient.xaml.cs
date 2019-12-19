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
    /// Interaction logic for PageAddClient.xaml
    /// </summary>
    public partial class PageAddClient : Page
    {
        Client _cc = null;
        public PageAddClient(Client currentClient)
        {
            InitializeComponent();
            _cc = currentClient;
            BtnAddEdit.Content = Properties.Resources.BtnEdit;

        }

        public PageAddClient()
        {
            InitializeComponent();
            BtnAddEdit.Content = Properties.Resources.BtnAdd;
        }

        private void BtnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TBxName.Text))
                error.AppendLine(Properties.Resources.ErrorName);
            if (DPDateOfBirth.SelectedDate == null)
                error.AppendLine(Properties.Resources.ErrorDateOfBirth);
            if (string.IsNullOrWhiteSpace(TBxAddress.Text))
                error.AppendLine(Properties.Resources.ErrorAddress);
            if (!MaskTBxPhoneNumber.MaskCompleted)
                error.AppendLine(Properties.Resources.ErrorPhone);
            if (!error.ToString().Equals(""))
            {
                MessageBox.Show(Properties.Resources.ErrorSomethingWrong + "\n\n" + error, Properties.Resources.CaptionError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                if (_cc == null)
                {
                    Client client = new Client()
                    {
                        idClient = AppData.context.Clients.Max(p => p.idClient) + 1,
                        Name = TBxName.Text,
                        DateOfBirth = (DateTime)DPDateOfBirth.SelectedDate,
                        Address = TBxAddress.Text,
                        Phone = MaskTBxPhoneNumber.Text
                    };
                    AppData.context.Clients.Add(client);
                    MessageBox.Show(Properties.Resources.MessageSuccessfullAdd, Properties.Resources.CaptionSuccessfully,
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _cc.Name = TBxName.Text;
                    _cc.DateOfBirth = (DateTime)DPDateOfBirth.SelectedDate;
                    _cc.Address = TBxAddress.Text;
                    _cc.Phone = MaskTBxPhoneNumber.Text;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CBxGender.ItemsSource = AppData.context.Genders.ToList();
            CBxGender.SelectedIndex = 0;
            if (_cc!=null)
            {
                TBxName.Text = _cc.Name;
                DPDateOfBirth.SelectedDate = _cc.DateOfBirth;
                CBxGender.SelectedItem = _cc.Gender;
                TBxAddress.Text = _cc.Address;
                MaskTBxPhoneNumber.Text = _cc.Phone;
                BtnAddEdit.Content = Properties.Resources.BtnEdit;
            }
        }
    }
}
