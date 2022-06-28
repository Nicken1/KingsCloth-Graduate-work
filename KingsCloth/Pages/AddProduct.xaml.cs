using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Drawing;

namespace KingsCloth.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        public AddProduct()
        {
            InitializeComponent();

            update_items_storage();

            var e = (BitmapSource)picture.Source;

            imageData = BitmapSource2ByteArray(e);
        }

        Byte[] BitmapSource2ByteArray(BitmapSource source)
        {
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();

            encoder.Save(stream);
            return stream.ToArray();
        }

        byte[] imageData = null;
        reqDB db = new reqDB();
        private void update_items_storage()
        {
            var dt = db.select_storage_address();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmb_storage.Items.Add(dt.Rows[i]["id"].ToString() + " " + dt.Rows[i]["address"].ToString());
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        string pat;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;...",
            };
            
            
            if (openFileDialog.ShowDialog() == true)
            {
                pat = openFileDialog.FileName;
                try
                {
                    using (FileStream fs = new FileStream(pat, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                        fs.Dispose();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Выберете другую картинку");
                }
                
                picture.Source = new BitmapImage(new Uri(pat));
            }
            
        }

        private void clck_upload(object sender, RoutedEventArgs e)
        {
            reqDB req = new reqDB();

            using (var stream = new MemoryStream(req.select_picture_product(21)))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                picture.Source = image;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(tx_xs.Text) +
                Convert.ToInt32(tx_s.Text) +
                Convert.ToInt32(tx_m.Text) +
                Convert.ToInt32(tx_l.Text) +
                Convert.ToInt32(tx_xl.Text) +
                Convert.ToInt32(tx_xxl.Text) > 0)
                {
                    string cmb = cmb_storage.Text;
                    string id_storage = "";
                    for (int i = 0; i < cmb.Length; i++)
                    {
                        if (!Char.IsWhiteSpace(cmb[i]))
                            id_storage += cmb[i];
                        else break;
                    }

                        reqDB req = new reqDB();
                    req.insert_size(
                        Convert.ToInt32(tx_xs.Text),
                        Convert.ToInt32(tx_s.Text),
                        Convert.ToInt32(tx_m.Text),
                        Convert.ToInt32(tx_l.Text),
                        Convert.ToInt32(tx_xl.Text),
                        Convert.ToInt32(tx_xxl.Text));
                    int max_id_size = Convert.ToInt32(req.select_max_id_size().Rows[0][0]);
                    decimal d = Convert.ToInt32(tx_cost.Text);
                    if (Properties.Settings.Default.LangueTogle == false)
                        req.insert_product(tx_title.Text, (int)d, cmb_category.SelectedIndex, tx_material.Text, cmb_color.Text, tx_description.Text, imageData, Convert.ToInt32(id_storage), max_id_size);
                    else
                        req.insert_product(tx_title.Text, (int)Math.Ceiling(d/60), cmb_category.SelectedIndex, tx_material.Text, cmb_color.Text, tx_description.Text, imageData, Convert.ToInt32(id_storage), max_id_size);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении товара");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Укажите корректные данные");
                tx_xs.Text = "0";
                tx_s.Text = "0";
                tx_m.Text = "0";
                tx_l.Text = "0";
                tx_xl.Text = "0";
                tx_xxl.Text = "0";
            }
        }


    }
}
