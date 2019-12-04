using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TerentievFurnitureStore
{
    class AppData
    {
        public static Entities.TerentievDataBaseEntities context = new Entities.TerentievDataBaseEntities();
        public static Frame mainFrame;
        public static Entities.User currentUser;
        public static Windows.WindowAddEdit WindowAdd = new Windows.WindowAddEdit();
    }
}
