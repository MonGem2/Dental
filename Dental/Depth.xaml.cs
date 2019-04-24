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
        int current = 0;
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
            try
            {
                (new AddDepth(((DataRowView)View.SelectedItems[0])["Id"].ToString())).ShowDialog();
            }
            catch { }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            try
            {
                DataRowView row = (DataRowView)View.SelectedItems[0];

                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                try
                {
                    _con.Open();
                    string query = $"Delete from [Depth] where Id='{ row["id"]}'";
                    SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                    _cmd.ExecuteNonQuery();

                    _con.Close();
                }
                catch
                {
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
                    catch
                    {
                    }

                }
            }
            catch { }
        }
    }
}
