using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace KingsCloth
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        public Authorization()
        {
            InitializeComponent();
        }
        reqDB req = new reqDB();

        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 2; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        public void btnSignIn(object sender, RoutedEventArgs e)
        {
            string log, pas;
            log = txLogin.Text;
            pas = txPas.Password;
            var data = req.select_access(log, pas);

            if (data.Rows.Count > 0)
            {
                switch (data.Rows[0]["id_access"])
                {
                    case 1:
                        MainWindow mainWindow1 = new MainWindow();
                        CloseAllWindows();
                        mainWindow1.ShowDialog();
                        break;
                    case 2:
                        MainWindow mainWindow2 = new MainWindow();
                        CloseAllWindows();
                        mainWindow2.btn_add_user.Visibility = Visibility.Hidden;
                        mainWindow2.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnSignGuest(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow3 = new MainWindow();
            Close();
            mainWindow3.btn_add_user.Visibility = Visibility.Hidden;
            mainWindow3.btn_add_prod.Visibility = Visibility.Hidden;
            mainWindow3.btn_stats.Visibility = Visibility.Hidden;
            mainWindow3.btn_storage.Visibility = Visibility.Hidden;
            CloseAllWindows();
            mainWindow3.ShowDialog();
        }
    }
}