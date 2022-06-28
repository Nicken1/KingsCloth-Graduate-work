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

namespace KingsCloth.Pages
{
    /// <summary>
    /// Логика взаимодействия для RestartAlert.xaml
    /// </summary>
    public partial class RestartAlert : Window
    {
        public RestartAlert()
        {
            InitializeComponent();
        }

        private void btn_ok(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void btn_later(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
