﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Page
    {
        int current = 0;
        public Transactions()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string path = "Base/Denta.db";
            string text = "Select * From [Transactions]";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                View.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this).NavigationService.GoBack();
            (this).NavigationService.RemoveBackEntry();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
          //  
          //  string path = "Base/Denta.db";
          //  SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
          //  try
          //  {
          //      _con.Open();
          //      string query = $"Delete from [Transactions] where Id='{sel}'";
          //      if (textb.Text != "") // Note: txt_Search is the TextBox..
          //      {
          //          query += $" where Name Like '%{textb.Text}%'";
          //      }
          //      SQLiteCommand _cmd = new SQLiteCommand(query, _con);
          //      _cmd.ExecuteNonQuery();
          //
          //      SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
          //      DataTable _dt = new DataTable("tbl_user");
          //      _adp.Fill(_dt);
          //      View.ItemsSource = _dt.DefaultView;
          //      _adp.Update(_dt);
          //
          //      _con.Close();
          //  }
          //  catch (Exception ex)
          //  {
          //      MessageBox.Show(ex.Message);
          //  }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = "Base/Denta.db";
            switch (current)
            {
                case 0:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Transactions]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where Name Like '%{textb.Text}%'";
                        }
                        SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                        _cmd.ExecuteNonQuery();

                        SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                        DataTable _dt = new DataTable("tbl_user");
                        _adp.Fill(_dt);
                        View.ItemsSource = _dt.DefaultView;
                        _adp.Update(_dt);

                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                }
                case 1:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Transactions]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where Surname Like '%{textb.Text}%'";
                        }
                        SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                        _cmd.ExecuteNonQuery();

                        SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                        DataTable _dt = new DataTable();
                        _adp.Fill(_dt);
                        View.ItemsSource = _dt.DefaultView;
                        _adp.Update(_dt);

                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                }
                case 2:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Transactions]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where FatherName Like '%{textb.Text}%'";
                        }
                        SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                        _cmd.ExecuteNonQuery();

                        SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                        DataTable _dt = new DataTable();
                        _adp.Fill(_dt);
                        View.ItemsSource = _dt.DefaultView;
                        _adp.Update(_dt);

                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                }
                case 3:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Transactions]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where Description Like '@{textb.Text}@'";
                        }
                        SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                        _cmd.ExecuteNonQuery();

                        SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                        DataTable _dt = new DataTable();
                        _adp.Fill(_dt);
                        View.ItemsSource = _dt.DefaultView;
                        _adp.Update(_dt);

                        _con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            current = combo.SelectedIndex;
        }
    }
}
