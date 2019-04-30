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
            MainWindow.Pager.Items.Remove(MainWindow.tb);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (View.SelectedItems.Count != 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите удалить этого пациента и все что с ним связано?", "Подтверждение", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
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
                        finally
                        {
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
                    catch { }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textb.Text.ToLower();
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";

                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {
                        _con.Open();
                        string query = "select * from [Patients]";
                        if (textb.Text != "") // Note: txt_Search is the TextBox..
                        {
                            query += $" where Description Like '@{textb.Text}@' or Mobile_Phone Like '%{textb.Text}%' or Home_Phone Like '%{textb.Text}%' or  Work_Phone Like '%{textb.Text}%' or FatherName Like '%{textb.Text}%' or Surname Like '%{textb.Text}%' or Name Like '%{textb.Text}%'";
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {   
            try
            {
                TabItem tb = new TabItem() { Header = "Карточка: " + ((DataRowView)View.SelectedItems[0])["Name"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Surname"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString()) } };
                MainWindow.Pager.Items.Add(tb);
                MainWindow.Pager.SelectedItem = tb;
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            textb.Text = string.Empty;
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TabItem tb = new TabItem() { Header= "Карточка: " + ((DataRowView)View.SelectedItems[0])["Name"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Surname"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString()) } };
                MainWindow.Pager.Items.Add(tb);
                tb.IsSelected = true;
            }
            catch { }
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        { 
            TabItem tb = new TabItem() { Header = "Новая карточка", Content = new Frame() { Content = new New_Card() } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }



        private void View_SourceUpdated(object sender, EventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";

            SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            try
            {

                _con.Open();
                using (SQLiteTransaction tr = _con.BeginTransaction())
                {
                    using (SQLiteCommand command = _con.CreateCommand())
                    {
                        command.Transaction = tr;
                        command.CommandText =
                            $"update Patients set Name = '{((DataRowView)View.SelectedItems[0])["Name"].ToString()}', Surname = '{((DataRowView)View.SelectedItems[0])["Surname"].ToString()}', FatherName = '{((DataRowView)View.SelectedItems[0])["FatherName"].ToString()}', Mobile_Phone = '{((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString()}', Home_Phone = '{((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString()}', Work_Phone = '{((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString()}', Date_Birth = '{((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString()}', Gender = '{((DataRowView)View.SelectedItems[0])["Gender"].ToString()}', Card_Num = '{((DataRowView)View.SelectedItems[0])["Card_Num"].ToString()}', Description = '{((DataRowView)View.SelectedItems[0])["Description"].ToString()}', Date = '{((DataRowView)View.SelectedItems[0])["Date"].ToString()}' where Id = '{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
                        MessageBox.Show(((DataRowView)View.SelectedItems[0])["Surname"].ToString());
                        command.ExecuteNonQuery();

                        // command.CommandText =
                        //    $"update Patients set Name = '{((DataRowView)View.SelectedItems[0])["Name"].ToString()}', Surname = '{((DataRowView)View.SelectedItems[0])["Surname"].ToString()}', FatherName = '{((DataRowView)View.SelectedItems[0])["FatherName"].ToString()}', Mobile_Phone = '{((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString()}', Home_Phone = '{((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString()}', Work_Phone = '{((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString()}', Date_Birth = '{((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString()}', Gender = '{((DataRowView)View.SelectedItems[0])["Gender"].ToString()}', Card_Num = '{((DataRowView)View.SelectedItems[0])["Card_Num"].ToString()}', Description = '{((DataRowView)View.SelectedItems[0])["Description"].ToString()}', Date = '{((DataRowView)View.SelectedItems[0])["Date"].ToString()}' where Id = '{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";


                    }
                    tr.Commit();
                }
                //string query = $"Update [Patients] set [Name]='{((DataRowView)View.SelectedItems[0])["Name"].ToString()}', [Surname]='{((DataRowView)View.SelectedItems[0])["Surname"].ToString()}', [FatherName]='{((DataRowView)View.SelectedItems[0])["FatherName"].ToString()}', [Mobile_Phone]='{((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString()}', [Work_Phone]='{((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString()}', [Home_Phone]='{((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString()}', [Date_Birth]='{((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString()}', [Gender]='{((DataRowView)View.SelectedItems[0])["Gender"].ToString()}', [Card_Num]='{((DataRowView)View.SelectedItems[0])["Card_Num"]}', [Description]='{((DataRowView)View.SelectedItems[0])["Description"].ToString()}', [Date]='{((DataRowView)View.SelectedItems[0])["Date"].ToString()}' where Id = '{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
                //SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                //_cmd.ExecuteNonQuery();

                //SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                //DataTable _dt = new DataTable();
                //_adp.Fill(_dt);
                //View.ItemsSource = _dt.DefaultView;
                //_adp.Update(_dt);

                _con.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F5)
            {
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";

                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                try
                {

                    _con.Open();
                        using (SQLiteCommand command = new SQLiteCommand(_con))
                        {
                            command.CommandText =
                                $"update Patients set Name = '{((DataRowView)View.SelectedItems[0])["Name"].ToString()}', Surname = '{((DataRowView)View.SelectedItems[0])["Surname"].ToString()}', FatherName = '{((DataRowView)View.SelectedItems[0])["FatherName"].ToString()}', Mobile_Phone = '{((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString()}', Home_Phone = '{((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString()}', Work_Phone = '{((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString()}', Date_Birth = '{((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString()}', Gender = '{((DataRowView)View.SelectedItems[0])["Gender"].ToString()}', Card_Num = '{((DataRowView)View.SelectedItems[0])["Card_Num"].ToString()}', Description = '{((DataRowView)View.SelectedItems[0])["Description"].ToString()}', Date = '{((DataRowView)View.SelectedItems[0])["Date"].ToString()}' where Id = '{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
                            MessageBox.Show(((DataRowView)View.SelectedItems[0])["Surname"].ToString());
                            command.ExecuteNonQuery();

                            // command.CommandText =
                            //    $"update Patients set Name = '{((DataRowView)View.SelectedItems[0])["Name"].ToString()}', Surname = '{((DataRowView)View.SelectedItems[0])["Surname"].ToString()}', FatherName = '{((DataRowView)View.SelectedItems[0])["FatherName"].ToString()}', Mobile_Phone = '{((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString()}', Home_Phone = '{((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString()}', Work_Phone = '{((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString()}', Date_Birth = '{((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString()}', Gender = '{((DataRowView)View.SelectedItems[0])["Gender"].ToString()}', Card_Num = '{((DataRowView)View.SelectedItems[0])["Card_Num"].ToString()}', Description = '{((DataRowView)View.SelectedItems[0])["Description"].ToString()}', Date = '{((DataRowView)View.SelectedItems[0])["Date"].ToString()}' where Id = '{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";


                        }
                    //string query = $"Update [Patients] set [Name]='{((DataRowView)View.SelectedItems[0])["Name"].ToString()}', [Surname]='{((DataRowView)View.SelectedItems[0])["Surname"].ToString()}', [FatherName]='{((DataRowView)View.SelectedItems[0])["FatherName"].ToString()}', [Mobile_Phone]='{((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString()}', [Work_Phone]='{((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString()}', [Home_Phone]='{((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString()}', [Date_Birth]='{((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString()}', [Gender]='{((DataRowView)View.SelectedItems[0])["Gender"].ToString()}', [Card_Num]='{((DataRowView)View.SelectedItems[0])["Card_Num"]}', [Description]='{((DataRowView)View.SelectedItems[0])["Description"].ToString()}', [Date]='{((DataRowView)View.SelectedItems[0])["Date"].ToString()}' where Id = '{((DataRowView)View.SelectedItems[0])["Id"].ToString()}'";
                    //SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                    //_cmd.ExecuteNonQuery();

                    //SQLiteDataAdapter _adp = new SQLiteDataAdapter(_cmd);
                    //DataTable _dt = new DataTable();
                    //_adp.Fill(_dt);
                    //View.ItemsSource = _dt.DefaultView;
                    //_adp.Update(_dt);

                    _con.Close();
                }
                catch (Exception ex)
                {
                     MessageBox.Show(ex.Message);
                }
            }
            else if(e.Key == Key.F5)
            {
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                string text = "Select * From [Patients]";
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
        }
    }
}
