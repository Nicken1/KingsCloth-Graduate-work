﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KingsCloth.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogDialog.xaml
    /// </summary>
    public partial class CatalogDialog : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public CatalogDialog()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Tick += timer_Tick;
            Load();
            

            if (Properties.Settings.Default.ThemeTogle == true)
            {
                SnackbarTwo.Background = new SolidColorBrush(Colors.LightGray);
            }
            if (Properties.Settings.Default.ThemeTogle == false)
            {
                SnackbarTwo.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#36393f");
            }
        }

        reqDB req = new reqDB();

        bool xs = false,
            s = false,
            m = false,
            l = false,
            xl = false,
            xxl = false;

        private void Load()
        {
            tx_name.Text = total.name;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                tx_cost.Text = "Price: " + total.price.ToString() + "$";
                tx_color.Text = "Color: " + total.color.ToString();
                tx_left.Text = "Left:";
            }
            else
            {
                tx_cost.Text = "Цена: " + (total.price).ToString() + "₽";
                tx_color.Text = "Цвет: " + total.color.ToString();
                tx_left.Text = "Осталось:";
            }
            pic_product.Source = total.image;
            for (int i = 0; i <= 5; i++)
            {
                enable_btn(i);
            }

            for (int i = 0; i < basket_data.dt_prod.Rows.Count; i++)
            {
                if ((int)basket_data.dt_prod.Rows[i]["id"] == total.id_product)
                {
                    for (int y = 1; y < basket_data.dt_size.Columns.Count; y++)
                    {
                        if (basket_data.dt_size.Rows[i][y] != DBNull.Value && (int)basket_data.dt_size.Rows[i][y] > 0)
                        {
                            switch (basket_data.dt_size.Columns[y].ColumnName)
                            {
                                case "xs":
                                    xs = true;
                                    break;
                                case "s":
                                    s = true;
                                    break;
                                case "m":
                                    m = true;
                                    break;
                                case "l":
                                    l = true;
                                    break;
                                case "xl":
                                    xl = true;
                                    break;
                                case "xxl":
                                    xxl = true;
                                    break;
                            }
                        }
                    }
                }
            }
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            SnackbarTwo.IsActive = false;
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

        private void insert_main(string size)
        {
            if (basket_data.dt_prod.Columns.Count == 0 && basket_data.dt_size.Columns.Count == 0)
            {
                basket_data.dt_prod = req.select_product_by_id(total.id_product);
                basket_data.dt_size = req.select_size((int)basket_data.dt_prod.Rows[basket_data.dt_prod.Rows.Count - 1]["id_size"]);
                basket_data.dt_prod.Rows.Clear();
                basket_data.dt_size.Rows.Clear();
            }
            basket_data.dt_prod.ImportRow(req.select_product_by_id(total.id_product).Rows[0]);
            basket_data.dt_size.ImportRow(req.select_size_by_size((int)basket_data.dt_prod.Rows[basket_data.dt_prod.Rows.Count - 1]["id_size"], size).Rows[0]);
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(listbox.SelectedIndex.ToString());
            if (listbox.SelectedIndex != -1)
            {


                switch (listbox.SelectedIndex)
                {
                    case 0:
                        {
                            if (xs == false)
                            {
                                insert_main("xs");
                                xs = true;
                                SnackbarTwo.IsActive = true;
                                timer.Start();
                            }
                            else
                                MessageBox.Show("Данный размер уже добавлен в корзину");
                            
                        }
                        break;
                    case 1:
                        {

                            if (s == false)
                            {
                                insert_main("s");
                                s = true;
                                SnackbarTwo.IsActive = true;
                                timer.Start();
                            }
                            else
                                MessageBox.Show("Данный размер уже добавлен в корзину");
                        }
                        break;
                    case 2:
                        {
                            if (m == false)
                            {
                                insert_main("m");
                                m = true;
                                SnackbarTwo.IsActive = true;
                                timer.Start();
                            }
                            else
                                MessageBox.Show("Данный размер уже добавлен в корзину");
                        }
                        break;
                    case 3:
                        {
                            if (l == false)
                            {
                                insert_main("l");
                                l = true;
                                SnackbarTwo.IsActive = true;
                                timer.Start();
                            }
                            else
                                MessageBox.Show("Данный размер уже добавлен в корзину");
                        }
                        break;
                    case 4:
                        {
                            if (xl == false)
                            {
                                insert_main("xl");
                                xl = true;
                                SnackbarTwo.IsActive = true;
                                timer.Start();
                            }
                            else
                                MessageBox.Show("Данный размер уже добавлен в корзину");                        }
                        break;
                    case 5:
                        {
                            if (xxl == false)
                            {
                                insert_main("xxl");
                                xxl = true;
                                SnackbarTwo.IsActive = true;
                                timer.Start();
                            }
                            else
                                MessageBox.Show("Данный размер уже добавлен в корзину");
                        }
                        break;
                }
            }
            else
                MessageBox.Show("Размер не выбран");

        }

        public void enable_btn(int index)
        {
            
            switch (index)
            {
                case 0:
                    {
                        if (total.left.Rows[0]["xs"] != DBNull.Value && total.left.Rows[0]["xs"].ToString() != "0")
                            tx_leftt.Text = total.left.Rows[0]["xs"].ToString();
                        else
                        {
                            btn_xs.IsEnabled = false;
                            btn_xs.IsSelected = false;
                        }

                    }
                    break;
                case 1:
                    {
                        if (total.left.Rows[0]["s"] != DBNull.Value && total.left.Rows[0]["s"].ToString() != "0")
                            tx_leftt.Text =  total.left.Rows[0]["s"].ToString();
                        else
                            btn_s.IsEnabled = false;
                    }
                    break;
                case 2:
                    {
                        if (total.left.Rows[0]["m"] != DBNull.Value && total.left.Rows[0]["m"].ToString() != "0")
                            tx_leftt.Text = total.left.Rows[0]["m"].ToString();
                        else
                            btn_m.IsEnabled = false;
                    }
                    break;
                case 3:
                    {
                        if (total.left.Rows[0]["l"] != DBNull.Value && total.left.Rows[0]["l"].ToString() != "0")
                            tx_leftt.Text = total.left.Rows[0]["l"].ToString();
                        else
                            btn_l.IsEnabled = false;
                    }
                    break;
                case 4:
                    {
                        if (total.left.Rows[0]["xl"] != DBNull.Value && total.left.Rows[0]["xl"].ToString() != "0")
                            tx_leftt.Text = total.left.Rows[0]["xl"].ToString();
                        else
                            btn_xl.IsEnabled = false;
                    }
                    break;
                case 5:
                    {
                        if (total.left.Rows[0]["xxl"] != DBNull.Value && total.left.Rows[0]["xxl"].ToString() != "0")
                            tx_leftt.Text = total.left.Rows[0]["xxl"].ToString();
                        else
                            btn_xxl.IsEnabled = false;
                    }
                    break;
            }
        }

        public void listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enable_btn(listbox.SelectedIndex);
        }
    }
}
