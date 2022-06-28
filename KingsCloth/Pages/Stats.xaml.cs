using System;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;
namespace KingsCloth.Pages
{
    /// <summary>
    /// Логика взаимодействия для Stats.xaml
    /// </summary>
    public partial class Stats : Page
    {
        int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
        double s1 = 0, s2 = 0, s3 = 0, s4 = 0;
        double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
        long cost, discount;

        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        string Tdiscount, TVAT, TProfit, TSalesProfit, TProductSold;

        reqDB req = new reqDB();

        long GraphVAT, GraphProfit, GraphDiscount;

        public Stats()
        {
            InitializeComponent();

            cost = req.select_all_cost();
            discount = req.select_all_discount();

            PieGraph();
            ColumnValues();
            ColumnGraph();
            Column2Values();
            Column2Graph();


            if (Properties.Settings.Default.LangueTogle == false)
            {
                Text1.Text = "SalesProfit: " + (cost + discount).ToString() + " $";
                Text2.Text = "Discount: " + discount.ToString() + " $";
                Text3.Text = "VAT: " + ((cost - discount) / 100 * 20).ToString() + " $";
                Text4.Text = "Profit: " + (cost - (cost - discount) / 100 * 20).ToString() + " $";
                Description.Text = "* This pie chart shows the Total Profit minus used discount coupons and VAT.";
                Tdiscount = "Discount";
                TVAT = "VAT";
                TProfit = "Profit";
                TSalesProfit = "Sales profit";
                TProductSold = "Product Sold";

            }
            else
            {
                Text1.Text = "Прибыль с продаж: " + ((cost + discount) * 60).ToString() + " ₽";
                Text2.Text = "Скидка: " + (discount * 60).ToString() + " ₽";
                Text3.Text = "НДС: " + (((cost - discount) / 100 * 20) * 60).ToString() + " ₽";
                Text4.Text = "Общаяя прибыль: " + ((cost - (cost - discount) / 100 * 20) * 60).ToString() + " ₽";
                Description.Text = "* На этой круговой диаграмме показана общая прибыль за вычетом использованных купонов на скидку и НДС.";
                Tdiscount = "Скидка";
                TVAT = "НДС";
                TProfit = "Прибыль";
                TSalesProfit = "Прибыль с продаж";
                TProductSold = "Продано товара";

            }

            



        }

        public void PieGraph()
        {
            if (Properties.Settings.Default.LangueTogle == false)
            {
                SeriesCollection = new SeriesCollection
            {

                new PieSeries
                {
                    Title = Tdiscount,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(discount)  },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = TVAT,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(cost- ((cost - discount) / 100 *20)) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = TProfit,
                    Values = new ChartValues<ObservableValue> { new ObservableValue((cost - discount) / 100 *20) },
                    DataLabels = true

                }
            };
            }
            else
                SeriesCollection = new SeriesCollection
            {

                new PieSeries
                {
                    Title = Tdiscount,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(discount*60)  },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = TVAT,
                    Values = new ChartValues<ObservableValue> { new ObservableValue((cost- (cost - discount) / 100 *20) * 60) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = TProfit,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(((cost - discount) / 100 *20)*60) },
                    DataLabels = true

                }
            };


            DataContext = this;
        }

        public void PieLoad()
        {
            Pie.Visibility = Visibility.Visible;
            Column.Visibility = Visibility.Hidden;
            Column2.Visibility = Visibility.Hidden;
            Line.Visibility = Visibility.Visible;
            stackPanel2.Visibility = Visibility.Visible;
            Line2.Visibility = Visibility.Visible;
            stackPanel.Width = 260;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                Text1.Text = "SalesProfit: " + (cost + discount).ToString() + " $";
                Text2.Text = "Discount: " + discount.ToString() + " $";
                Text3.Text = "VAT: " + ((cost - discount) / 100 * 20).ToString() + " $";
                Text4.Text = "Profit: " + (cost - (cost - discount) / 100 * 20).ToString() + " $";
                Description.Text = "* This pie chart shows the Total Profit minus used discount coupons and VAT.";
            }
            else
            {
                Text1.Text = "Прибыль с продаж: " + ((cost + discount) * 60).ToString() + " ₽";
                Text2.Text = "Скидка: " + (discount * 60).ToString() + " ₽";
                Text3.Text = "НДС: " + (((cost - discount) / 100 * 20)).ToString() + " ₽";
                Text4.Text = "Общаяя прибыль: " + ((cost - (cost - discount) / 100 * 20) * 60).ToString() + " ₽";
                Description.Text = "* На этой круговой диаграмме показана общая прибыль за вычетом использованных купонов на скидку и НДС.";
            }

        }


        private void replace_text(string replace, string text, Word.Document wordDoc)
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: replace, ReplaceWith: text);
        }
        string exePath = AppDomain.CurrentDomain.BaseDirectory;
        //private readonly string path = @"c:\test.docx";

        public void btn_export(object sender, RoutedEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            Word.Document wordDoc;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                wordDoc = wordApp.Documents.Open(exePath + "pattern.docx");
                replace_text("{q1}", q1.ToString(), wordDoc);
                replace_text("{s1}", s1.ToString(), wordDoc);
                replace_text("{d1}", d1.ToString(), wordDoc);
                var p1 = (s1 - d1) * 0.8;
                replace_text("{p1}", p1.ToString(), wordDoc);

                replace_text("{q2}", q2.ToString(), wordDoc);
                replace_text("{s2}", s2.ToString(), wordDoc);
                replace_text("{d2}", d2.ToString(), wordDoc);
                var p2 = (s2 - d2) * 0.8;
                replace_text("{p2}", p2.ToString(), wordDoc);

                replace_text("{q3}", q3.ToString(), wordDoc);
                replace_text("{s3}", s3.ToString(), wordDoc);
                replace_text("{d3}", d3.ToString(), wordDoc);
                var p3 = (s3 - d3) * 0.8;
                replace_text("{p3}", p3.ToString(), wordDoc);

                replace_text("{q4}", q4.ToString(), wordDoc);
                replace_text("{s4}", s4.ToString(), wordDoc);
                replace_text("{d4}", d4.ToString(), wordDoc);
                var p4 = (s4 - d4) * 0.8;
                replace_text("{p4}", p4.ToString(), wordDoc);

                var pT = p1 + p2 + p3 + p4;
                replace_text("{pT}", pT.ToString(), wordDoc);
            }


            else
            {
                wordDoc = wordApp.Documents.Open(exePath + "pattern_ru.docx");

                replace_text("{q1}", q1.ToString(), wordDoc);
                replace_text("{s1}", Convert.ToString(s1 * 60), wordDoc);
                replace_text("{d1}", Convert.ToString(d1 * 60), wordDoc);
                var p1 = (s1 - d1) * 0.8 * 60;
                replace_text("{p1}", p1.ToString(), wordDoc);

                replace_text("{q2}", q2.ToString(), wordDoc);
                replace_text("{s2}", Convert.ToString(s2 * 60), wordDoc);
                replace_text("{d2}", Convert.ToString(d2 * 60), wordDoc);
                var p2 = (s2 - d2) * 0.8 * 60;
                replace_text("{p2}", p2.ToString(), wordDoc);

                replace_text("{q3}", q3.ToString(), wordDoc);
                replace_text("{s3}", Convert.ToString(s3 * 60), wordDoc);
                replace_text("{d3}", Convert.ToString(d3 * 60), wordDoc);
                var p3 = (s3 - d3) * 0.8 * 60;
                replace_text("{p3}", p3.ToString(), wordDoc);

                replace_text("{q4}", q4.ToString(), wordDoc);
                replace_text("{s4}", Convert.ToString(s4 * 60), wordDoc);
                replace_text("{d4}", Convert.ToString(d4 * 60), wordDoc);
                var p4 = (s4 - d4) * 0.8 * 60;
                replace_text("{p4}", p4.ToString(), wordDoc);

                var pT = p1 + p2 + p3 + p4;
                replace_text("{pT}", pT.ToString(), wordDoc);
            }

           

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DOCX file (*.docx)|*.docx|PDF file (*.pdf)|*.pdf";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    if (saveFileDialog.FileName.Contains(".pdf"))
                    {
                        wordDoc.ExportAsFixedFormat(saveFileDialog.FileName, Word.WdExportFormat.wdExportFormatPDF);
                        MessageBox.Show("Файл успешно сохранен");
                    }
                    else if (saveFileDialog.FileName.Contains(".docx"))
                    {
                        wordDoc.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Файл успешно сохранен");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                wordDoc.Close();
            }
        }

        public void ColumnValues()
        {
            reqDB req = new reqDB();
            var table = req.select_history();


            for (int i = 0; i < table.Rows.Count; i++)
            {
                DateTime myDate = Convert.ToDateTime(table.Rows[i]["date"].ToString());
                
                if (myDate.Year == DateTime.Now.Year)
                {
                    switch (myDate.Month)
                    {
                        case 1:
                        case 2:
                        case 3:
                            q1 += (int)table.Rows[i]["count_product"];
                            s1 += Convert.ToDouble(table.Rows[i]["cost"]);
                            d1 += Convert.ToDouble(table.Rows[i]["discount"]);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            q2 += (int)table.Rows[i]["count_product"];
                            s2 += Convert.ToDouble(table.Rows[i]["cost"]);
                            d2 += Convert.ToDouble(table.Rows[i]["discount"]);
                            break;
                        case 7:
                        case 8:
                        case 9:
                            q3 += (int)table.Rows[i]["count_product"];
                            s3 += Convert.ToDouble(table.Rows[i]["cost"]);
                            d3 += Convert.ToDouble(table.Rows[i]["discount"]);
                            break;
                        case 10:
                        case 11:
                        case 12:
                            q4 += (int)table.Rows[i]["count_product"];
                            s4 += Convert.ToDouble(table.Rows[i]["cost"]);
                            d4 += Convert.ToDouble(table.Rows[i]["discount"]);
                            break;
                    }
                }
            }
        }


        public void ColumnGraph()
        {

            if (Properties.Settings.Default.LangueTogle == false)
            {
                SeriesCollection1 = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = TSalesProfit,
                    Values = new ChartValues<double> {s1, s2, s3, s4},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = TVAT,
                    Values = new ChartValues<double> {d1, d2, d3, d4},
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };
            }
            else
            {
                SeriesCollection1 = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = TSalesProfit,
                    Values = new ChartValues<double> {s1*60, s2*60, s3*60, s4*60},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = TVAT,
                    Values = new ChartValues<double> {d1*60, d2*60, d3*60, d4*60},
                    StackMode = StackMode.Values,
                    DataLabels = true
                }
            };
            }
            


            if (Properties.Settings.Default.LangueTogle == false)
            {
                Labels = new[] { "1 Quarter", "2 Quarter", "3 Quarter", "4 Quarter" };
                Formatter = value => value + " $";
            }
            else
            {
                Labels = new[] { "1 Квартал", "2 Квартал", "3 Квартал", "4 Квартал" };
                Formatter = value => value + " ₽";
            }


            DataContext = this;
        }

        public void ColumnLoad()
        {
            Pie.Visibility = Visibility.Hidden;
            Column.Visibility = Visibility.Visible;
            Column2.Visibility = Visibility.Hidden;
            Line2.Visibility = Visibility.Visible;
            Line.Visibility = Visibility.Hidden;
            stackPanel2.Visibility = Visibility.Visible;
            stackPanel.Width = 260;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                Text1.Text = "1 Quarter: " + (s1 - d1).ToString() + " $";
                Text2.Text = "2 Quarter: " + (s2 - d2).ToString() + " $";
                Text3.Text = "3 Quarter: " + (s3 - d3).ToString() + " $";
                Text4.Text = "4 Quarter: " + (s4 - d4).ToString() + " $";
                Description.Text = "* This chart shows quarterly income after taxes.";
            }
            else
            {
                Text1.Text = "1 Квартал: " + ((s1 - d1) * 60).ToString() + " ₽";
                Text2.Text = "2 Квартал: " + ((s2 - d2) * 60).ToString() + " ₽";
                Text3.Text = "3 Квартал: " + ((s3 - d3) * 60).ToString() + " ₽";
                Text4.Text = "4 Квартал: " + ((s4 - d4) * 60).ToString() + " ₽";
                Description.Text = "* На этой диаграмме показан квартальный доход после уплаты налогов.";
            }
        }



        public void Column2Values()
        {
            reqDB req = new reqDB();
            var table = req.select_history();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DateTime myDate = Convert.ToDateTime(table.Rows[i]["date"].ToString());

                if (myDate.Year == DateTime.Now.Year)
                {
                    switch (myDate.Month)
                    {
                        case 1:
                        case 2:
                        case 3:
                            q1 += (int)table.Rows[i]["count_product"];
                            break;
                        case 4:
                        case 5:
                        case 6:
                            q2 += (int)table.Rows[i]["count_product"];
                            break;
                        case 7:
                        case 8:
                        case 9:
                            q3 += (int)table.Rows[i]["count_product"];
                            break;
                        case 10:
                        case 11:
                        case 12:
                            q4 += (int)table.Rows[i]["count_product"];
                            break;
                    }
                }
            }
        }

        public void Column2Graph()
        {
            SeriesCollection2 = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = TProductSold,
                    Values = new ChartValues<double> { q1, q2, q3, q4 }
                }
            };
            if (Properties.Settings.Default.LangueTogle == false)
            {
                Labels = new[] { "1 Quarter", "2 Quarter", "3 Quarter", "4 Quarter" };
                Formatter = value => value.ToString("N");
            }
            else
            {
                Labels = new[] { "1 Квартал", "2 Квартал", "3 Квартал", "4 Квартал" };
                Formatter = value => value.ToString("N");
            }

            DataContext = this;
        }

        public void Column2Load()
        {
            Pie.Visibility = Visibility.Hidden;
            Column.Visibility = Visibility.Hidden;
            Column2.Visibility = Visibility.Visible;
            Line2.Visibility = Visibility.Visible;
            Line.Visibility = Visibility.Hidden;
            stackPanel2.Visibility = Visibility.Visible;
            stackPanel.Width = 260;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                Text1.Text = "1 Quarter: " + q1 + "";
                Text2.Text = "2 Quarter: " + q2 + "";
                Text3.Text = "3 Quarter: " + q3 + "";
                Text4.Text = "4 Quarter: " + q4 + "";
                Description.Text = "* This chart shows the number of items sold per quarter.";
            }
            else
            {
                Text1.Text = "1 Квартал: " + q1 + "";
                Text2.Text = "2 Квартал: " + q2 + "";
                Text3.Text = "3 Квартал: " + q3 + "";
                Text4.Text = "4 Квартал: " + q4 + "";
                Description.Text = "* На этой диаграмме показано количество товаров, проданных за квартал.";
            }

        }

        public void StorageStatsLoad()
        {
            Pie.Visibility = Visibility.Hidden;
            Column.Visibility = Visibility.Hidden;
            Line.Visibility = Visibility.Hidden;
            Column2.Visibility = Visibility.Hidden;
            Line2.Visibility = Visibility.Hidden;
            stackPanel.Width = 750;
            stackPanel2.Visibility = Visibility.Hidden;
            if (Properties.Settings.Default.LangueTogle == false)
            {
                Text1.Text = "In storage: 'Складовик' product left (" + (req.select_all_count_for_stats(1)).ToString() + "/1000) for amount: " + req.select_all_cost_for_stats(1) + " $";
                Text2.Text = "In storage: 'Норд Склад' product left (" + (req.select_all_count_for_stats(2)).ToString() + "/656) for amount: " + req.select_all_cost_for_stats(2) + " $";
                Text3.Text = "In storage: 'М-Логистик' product left (" + (req.select_all_count_for_stats(3)).ToString() + "/984) for amount: " + req.select_all_cost_for_stats(3) + " $";
                Text4.Text = "In storage: 'Ваш-Склад' product left (" + (req.select_all_count_for_stats(4)).ToString() + "/120) for amount: " + req.select_all_cost_for_stats(4) + " $";
                Description.Text = "* These statistics show how many items are left in stock and their cost";
            }
            else
            {

                Text1.Text = "На складе: 'Складовик' осталось товара (" + (req.select_all_count_for_stats(1)).ToString() + "/1000) на сумму: " + (req.select_all_cost_for_stats(1) * 60) + " ₽";
                Text2.Text = "На складе: 'Норд Склад' осталось товара (" + (req.select_all_count_for_stats(2)).ToString() + "/656) на сумму: " + (req.select_all_cost_for_stats(2) * 60) + " ₽";
                Text3.Text = "На складе: 'М-Логистик' осталось товара (" + (req.select_all_count_for_stats(3)).ToString() + "/984) на сумму: " + (req.select_all_cost_for_stats(3) * 60) + " ₽";
                Text4.Text = "На складе: 'Ваш-Склад' осталось товара (" + (req.select_all_count_for_stats(4)).ToString() + "/120) на сумму: " + (req.select_all_cost_for_stats(4) * 60) + " ₽";
                Description.Text = "* Эта статистика показывает, сколько товаров осталось на складе и их стоимость.";
            }


        }

        private void ColumnDiogram_Click(object sender, RoutedEventArgs e)
        {
            ColumnLoad();
        }
        private void PieDiogram_Click(object sender, RoutedEventArgs e)
        {
            PieLoad();
        }
        private void StorageStats_Click(object sender, RoutedEventArgs e)
        {
            StorageStatsLoad();
        }

        private void ColumnDiogram2_Click(object sender, RoutedEventArgs e)
        {
            Column2Load();
        }
    }
}
