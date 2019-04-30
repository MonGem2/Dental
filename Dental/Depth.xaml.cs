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
    /// Interaction logic for Depth.xaml
    /// </summary>
    public partial class Depth : Page
    {
        public Depth()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            string text = "Select * From [Depth]";
            SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                View.ItemsSource = ds.Tables[0].DefaultView;
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            text = "Select * From [Pered]";
            SQLiteConnection con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            try
            {
                con1.Open();
                DataSet ds = new DataSet();
                var da = new SQLiteDataAdapter(text, con1);
                da.AcceptChangesDuringUpdate = true;
                da.Fill(ds);
                View1.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Pager.Items.Remove(MainWindow.tb2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddDepth()).ShowDialog();
            }
            catch { }
            finally
            {
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                try
                {
                    _con.Open();
                    string query = "select * from [Depth]";
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            if (View.SelectedItems.Count!=0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Вы хотите полностью погасить этот долг?", "Подтверждение", MessageBoxButton.YesNoCancel);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView row = (DataRowView)View.SelectedItems[0];

                        SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                        try
                        {

                            double sum = (double)row["Suma"];
                            SQLiteConnection _con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                            _con1.Open();
                            string query1 = $"insert into [Transactions] (Suma,Description,id_Patient,Date,Type) values ('{sum.ToString()}','{row["Description"].ToString()}','{row["id_Patient"].ToString()}','{row["Date"].ToString()}','Погашение долга')";
                            SQLiteCommand _cmd1 = new SQLiteCommand(query1, _con1);
                            _cmd1.ExecuteNonQuery();

                            _con1.Close();


                            _con.Open();
                            string query = $"Delete from [Depth] where Id='{ row["id"]}'";
                            SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                            _cmd.ExecuteNonQuery();

                            _con.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            try
                            {
                                _con.Open();
                                string query = "select * from [Depth]";
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

                        }
                    }
                    catch { }
                }
                if (dialogResult == MessageBoxResult.No)
                {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try {
                        DataRowView row = (DataRowView)View.SelectedItems[0];
                        (new Depther(double.Parse(row["Suma"].ToString()),row["Id"].ToString(),int.Parse(row["id_Patient"].ToString()))).ShowDialog();
                        }
                    catch { }
                    finally
                    {
                        try
                        {
                            _con.Open();
                            string query = "select * from [Depth]";
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

                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddPered()).ShowDialog();
            }
            catch { }
            finally
            {
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                try
                {
                    _con.Open();
                    string query = "select * from [Pered]";
                    SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                    _cmd.ExecuteNonQuery();

                    SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                    DataTable _dt = new DataTable();
                    _adp.Fill(_dt);
                    View1.ItemsSource = _dt.DefaultView;
                    _adp.Update(_dt);

                    _con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (View1.SelectedItems.Count != 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите принять эту предоплату?", "Подтверждение", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                    try
                    {
                        DataRowView row = (DataRowView)View1.SelectedItems[0];

                        SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                        try
                        {

                            double sum = (double)row["Suma"];
                            SQLiteConnection _con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                            _con1.Open();
                            string query1 = $"insert into [Transactions] (Suma,Description,id_Patient,Date,Type) values ('{sum.ToString()}','{row["Description"].ToString()}','{row["id_Patient"].ToString()}','{row["Date"].ToString()}','Принятие предоплаты')";
                            SQLiteCommand _cmd1 = new SQLiteCommand(query1, _con1);
                            _cmd1.ExecuteNonQuery();

                            _con1.Close();


                            _con.Open();
                            string query = $"Delete from [Pered] where Id='{ row["id"]}'";
                            SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                            _cmd.ExecuteNonQuery();

                            _con.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            try
                            {
                                _con.Open();
                                string query = "select * from [Pered]";
                                SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                                _cmd.ExecuteNonQuery();

                                SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                                DataTable _dt = new DataTable("tbl_user");
                                _adp.Fill(_dt);
                                View1.ItemsSource = _dt.DefaultView;
                                _adp.Update(_dt);

                                _con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                    }
                    catch { }
                }
            }
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            string tmp = string.Empty;
            string text = $"Select [Name] From [Patients] where Id='{((DataRowView)View.SelectedItems[0])["id_Patient"].ToString()}'";
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

            text = $"Select [Surname] From [Patients] where Id='{((DataRowView)View.SelectedItems[0])["id_Patient"].ToString()}'";
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

            text = $"Select [FatherName] From [Patients] where Id='{((DataRowView)View.SelectedItems[0])["id_Patient"].ToString()}'";
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


            TabItem tb = new TabItem() { Header = tmp, Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["id_Patient"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }



        private void View_MouseDoubleClick1(object sender, MouseButtonEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            string tmp = string.Empty;
            string text = $"Select [Name] From [Patients] where Id='{((DataRowView)View1.SelectedItems[0])["id_Patient"].ToString()}'";
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

            text = $"Select [Surname] From [Patients] where Id='{((DataRowView)View1.SelectedItems[0])["id_Patient"].ToString()}'";
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

            text = $"Select [FatherName] From [Patients] where Id='{((DataRowView)View1.SelectedItems[0])["id_Patient"].ToString()}'";
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


            TabItem tb = new TabItem() { Header = tmp, Content = new Frame() { Content = new Card(((DataRowView)View1.SelectedItems[0])["id_Patient"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }

    }
}
