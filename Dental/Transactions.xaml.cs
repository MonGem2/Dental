﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
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

namespace Dental
{
    public partial class Transactions : Page
    {
        int current = 0;
        public Transactions()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                View.ItemsSource = DatabaseWorker.SelectTransactions().Tables[0].DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Pager.Items.Remove(MainWindow.tb3);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddTransaction()).ShowDialog();
            }
            catch { }
            finally
            {
                View.ItemsSource = DatabaseWorker.SelectTransactions().Tables[0].DefaultView;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            View.ItemsSource = DatabaseWorker.FindTransactions(textb.Text).DefaultView;
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Patient patient = DatabaseWorker.getPatient(((DataRowView)View.SelectedItems[0])["Id_patient"].ToString());
            string tmp = "Карточка:" + patient.Name+ " "+patient.Surname+" "+patient.FatherName;
            TabItem tb = new TabItem() { Header=tmp, Content = new Frame() { Content = new Card(patient.Id) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textb.Text = string.Empty;
        }
    }
}
