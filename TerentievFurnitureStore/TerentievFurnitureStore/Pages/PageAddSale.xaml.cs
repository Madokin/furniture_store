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
        }

        public PageAddSale()
        {
            InitializeComponent();
        }
    }
}
