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
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Dental
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page
    {
        int current=0;
        public Patients()
        {
            InitializeComponent();
        }
        //DataRowView row = (DataRowView)dg.SelectedItems[0];
        //row["Id"]
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            string text = "Select * From [Patients]";
            // MessageBox.Show(path);
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
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            try
            {
                DataRowView row = (DataRowView)View.SelectedItems[0];

                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                try
                {
                    _con.Open();
                    string query = $"Delete from [Patients] where Id='{ row["id"]}'";
                    SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                    _cmd.ExecuteNonQuery();

                    _con.Close();

                    SQLiteConnection _con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    _con1.Open();
                    string query1 = $"Delete from [Treatment] where id_Patient='{ row["id"]}'";
                    SQLiteCommand _cmd1 = new SQLiteCommand(query1, _con1);
                    _cmd1.ExecuteNonQuery();

                    _con1.Close();

                    SQLiteConnection _con2 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    _con2.Open();
                    string query2 = $"Delete from [Depth] where id_Patient='{ row["id"]}'";
                    SQLiteCommand _cmd2 = new SQLiteCommand(query2, _con2);
                    _cmd2.ExecuteNonQuery();

                    _con2.Close();

                    SQLiteConnection _con3 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    _con3.Open();
                    string query3 = $"Delete from [Transactions] where id_Patient='{ row["id"]}'";
                    SQLiteCommand _cmd3 = new SQLiteCommand(query3, _con3);
                    _cmd3.ExecuteNonQuery();

                    _con3.Close();
                }
                catch
                {
                }
                finally {
                    try
                    {
                        _con.Open();
                        string query = "select * from [Patients]";
                        SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                        _cmd.ExecuteNonQuery();

                        SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                        DataTable _dt = new DataTable("tbl_user");
                        _adp.Fill(_dt);
                        View.ItemsSource = _dt.DefaultView;
                        _adp.Update(_dt);

                        _con.Close();
                    }
                    catch
                    {
                    }
                    
                }
            }
            catch{ }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";

            switch (current)
            {
                case 0:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Patients]";
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
                        string query = "select * from [Patients]";
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
                        string query = "select * from [Patients]";
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
                        string query = "select * from [Patients]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where Mobile_Phone Like '%{textb.Text}%' or Home_Phone Like '%{textb.Text}%' or  Work_Phone Like '%{textb.Text}%'";
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
                case 4:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Patients]";
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
                case 5:
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Patients]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where CardNum Like '@{textb.Text}@'";
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
            this.NavigationService.Navigate(new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString()));
            }
            catch { }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddTreatment(((DataRowView)View.SelectedItems[0])["Id"].ToString())).ShowDialog();
            }
            catch { }
        }
    }
}
