﻿using KingsCloth.Pages.PagesAssets;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KingsCloth.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        public AddUser()
        {
            InitializeComponent();
            update_listView();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserDialog dialog = new AddUserDialog();
            dialog.Show();
        }

        DataTable table = new DataTable();
        reqDB db = new reqDB();

        private void update_listView()
        {
            listviewUsers.Items.Clear();

            table = db.select_user();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                this.listviewUsers.Items.Add(new users
                {
                    id = (int)table.Rows[i][0],
                    login = table.Rows[i][1].ToString(),
                    name = table.Rows[i][2].ToString(),
                    surname = table.Rows[i][3].ToString(),
                    password = table.Rows[i][4].ToString(),
                    id_access = table.Rows[i][5].ToString()
                });
            }
        }

        private void btn_update(object sender, RoutedEventArgs e)
        {
            update_listView();
        }
    }
}
