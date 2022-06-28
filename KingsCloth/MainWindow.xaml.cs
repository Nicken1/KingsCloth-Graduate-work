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
using MySql.Data.MySqlClient;


namespace KingsCloth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = Properties.Settings.Default.DefaultLanguage;
            InitializeComponent();
            if (Properties.Settings.Default.ThemeTogle == false)
            {
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Colors/DarkTheme.xaml", UriKind.RelativeOrAbsolute) });
                Title.Text = "Home";
            }
            if (Properties.Settings.Default.ThemeTogle == true)
            {
                App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Colors/LightTheme.xaml", UriKind.RelativeOrAbsolute) });
                Title.Text = "Главная";
            }

            fContainer.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
            
            Title.Visibility = Visibility.Visible;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            GridBackground.Visibility = Visibility.Visible;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            GridBackground.Visibility = Visibility.Hidden;
        }
        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Home";
            else
                Title.Text = "Главная";

            Title.Visibility = Visibility.Visible;
        }

        private void ButtonOpenSettings_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Settings.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Settings";
            else
                Title.Text = "Настройки";
            Title.Visibility = Visibility.Visible;
        }
        private void ButtonStorage_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Storage.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Storage";
            else
                Title.Text = "Склады";
            Title.Visibility = Visibility.Visible;
        }

        private void ButtonOpenHelp_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/HelpMenu.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Help";
            else
                Title.Text = "Помощь";
            Title.Visibility = Visibility.Visible;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //private void btnRestore_Click(object sender, RoutedEventArgs e)
        //{
        //    if (WindowState == WindowState.Normal)
        //        WindowState = WindowState.Maximized;
        //    else
        //        WindowState = WindowState.Normal;
        //}
 
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/AddUser.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Add User";
            else
                Title.Text = "Добавить пользователя";
            Title.Visibility = Visibility.Visible;

        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/AddProduct.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Add Product";
            else
                Title.Text = "Добавить продукт";
            Title.Visibility = Visibility.Visible;
        }

        private void ButtonStats_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Stats.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Stats";
            else
                Title.Text = "Статистика";
            Title.Visibility = Visibility.Visible;

        }

        private void ButtonCatalog_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Catalog.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Catalog";
            else
                Title.Text = "Каталог";
            Title.Visibility = Visibility.Visible;

        }

        private void ButtonBacket_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new System.Uri("Pages/Basket.xaml", UriKind.RelativeOrAbsolute));
            if (Properties.Settings.Default.LangueTogle == false)
                Title.Text = "Basket";
            else
                Title.Text = "Корзина";
            Title.Visibility = Visibility.Visible;
        }

    }
}
