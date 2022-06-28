using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KingsCloth.Pages
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        DispatcherTimer timer = new DispatcherTimer();

        private void EN_Checked(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            Properties.Settings.Default.DefaultLanguage = new System.Globalization.CultureInfo("en");
            Properties.Settings.Default.LangueTogle = false;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.LangueTogle == false && shit == false)
            {
                RestartAlert restartAlert = new RestartAlert();
                restartAlert.ShowDialog();
            }
            shit = false;




        }

        private void RU_Checked(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            Properties.Settings.Default.DefaultLanguage = new System.Globalization.CultureInfo("ru-RU");
            Properties.Settings.Default.LangueTogle = true;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.LangueTogle == true && shit == false)
            {
                RestartAlert restartAlert = new RestartAlert();
                restartAlert.ShowDialog();
            }
            shit = false;

        }
        bool shit = false;
        public Settings()
        {
            InitializeComponent();
            shit = true;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                EN.IsChecked = true;
                RU.IsChecked = false;
            }
            if (Properties.Settings.Default.LangueTogle == true)
            {
                EN.IsChecked = false;
                RU.IsChecked = true;
            }
            if (Properties.Settings.Default.ThemeTogle == true)
            {
                LightTheme.IsChecked = true;
                DarkTheme.IsChecked = false;
            }
            if (Properties.Settings.Default.ThemeTogle == false)
            {
                LightTheme.IsChecked = false;
                DarkTheme.IsChecked = true;
            }

            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += timer_Tick;

        }








        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Authorization dialog = new Authorization();
            dialog.ShowDialog();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            SnackbarTwo.IsActive = false;
        }

        private void LightTheme_Checked(object sender, RoutedEventArgs e)
        {
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Colors/LightTheme.xaml", UriKind.RelativeOrAbsolute) });
            Properties.Settings.Default.ThemeTogle = true;
            Properties.Settings.Default.Save();

        }

        private void DarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("Colors/DarkTheme.xaml", UriKind.RelativeOrAbsolute) });
            Properties.Settings.Default.ThemeTogle = false;
            Properties.Settings.Default.Save();

        }

       
    }
}

