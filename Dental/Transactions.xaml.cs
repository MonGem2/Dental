using System;
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
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
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
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                try
                {
                    _con.Open();
                    string query = "select * from [Transactions]";
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
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            textb.Text.ToLower();
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Transactions]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where Description Like '@{textb.Text}@' or where id_Patient Like '%{textb.Text}%' or where Suma Like '%{textb.Text}%' or where Type Like '%{textb.Text}%'";
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
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            string tmp = string.Empty;
            string text = $"Select [Name] From [Patients] where Id='{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    tmp = "Карточка:" + dataReader.GetString(0) + " ";
                }
                con.Dispose();
            }
            catch
            {

            }

            text = $"Select [Surname] From [Patients] where Id='{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    tmp += dataReader.GetString(0) + " ";
                }
                con.Dispose();
            }
            catch
            {

            }

            text = $"Select [FatherName] From [Patients] where Id='{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    tmp += dataReader.GetString(0) + " ";
                }
                con.Dispose();
            }
            catch
            {

            }


            TabItem tb = new TabItem() { Header=tmp, Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Id"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textb.Text = string.Empty;
        }
    }
}
