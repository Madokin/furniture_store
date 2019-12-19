using mshtml;
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

namespace TerentievFurnitureStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageReport.xaml
    /// </summary>
    public partial class PageReport : Page
    {
        public PageReport()
        {
            InitializeComponent();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            var currentSales = AppData.context.Sales.ToList();

            if (PickerStart.SelectedDate.HasValue == true)
            {
                currentSales = currentSales.Where(p => p.DateOfSale >= PickerStart.SelectedDate.Value).ToList();
            }
            if (PickerEnd.SelectedDate.HasValue == true)
            {
                currentSales = currentSales.Where(p => p.DateOfSale <= PickerEnd.SelectedDate.Value).ToList();
            }

            var result = new StringBuilder();

            
            result.Append("<html>");
            result.Append("<meta charset='utf-8'/>");
            result.Append("<body>");

           
            result.Append("<p align='center'> <b>Отчет по продажам</b> </p>");

            
            result.Append("<table width=100% border=1 bordercolor=#000 style='border-collapse:collapse;'>");

            
            result.Append("<tr>");
            
            result.Append("<td align=center><b>Код продажи</b></td>");
            result.Append("<td align=center><b>Клиент</b></td>");
            result.Append("<td align=center><b>Товар</b></td>");
            result.Append("<td align=center><b>Дата продажи</b></td>");
            result.Append("<td align=center><b>Количество</b></td>");
            result.Append("<td align=center><b>Цена</b></td>");
            result.Append("</tr>");

            
            foreach (var item in currentSales)
            {
                
                result.Append("<tr>");
                
                result.Append($"<td align=center>{item.idSale}</td>");
                result.Append($"<td align=center>{item.Client}</td>");
                result.Append($"<td align=center>{item.Product.Name}</td>");
                result.Append($"<td align=center>{item.DateOfSale.ToString("dd.MM.yyyy")}</td>");
                result.Append($"<td align=center>{item.Quantity}</td>");
                result.Append($"<td align=center>{item.Price} руб.</td>");
                result.Append("</tr>");
            }

            
            result.Append("</table>");
            result.Append("</body>");
            result.Append("</html>");

            
            BrowserReport.NavigateToString(result.ToString());
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            // Чтобы подключить объект IHTMLDocument2 необходимо добавить ссылку на Microsoft.mshtml (References - Assemblies).
            var document = BrowserReport.Document as IHTMLDocument2;
            document.execCommand("Print");
        }
    }
}
