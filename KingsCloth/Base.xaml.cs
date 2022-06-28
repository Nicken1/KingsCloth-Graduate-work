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
using System.Windows.Shapes;

namespace KingsCloth
{
    /// <summary>
    /// Логика взаимодействия для Base.xaml
    /// </summary>
    public partial class Base : Window
    {
        public Base()
        {
            InitializeComponent();

            MainWindow mainWindow3 = new MainWindow();
            Close();
            mainWindow3.btn_add_user.Visibility = Visibility.Hidden;
            mainWindow3.btn_add_prod.Visibility = Visibility.Hidden;
            mainWindow3.btn_stats.Visibility = Visibility.Hidden;
            mainWindow3.btn_storage.Visibility = Visibility.Hidden;
            mainWindow3.ShowDialog();
        }
    }
}
