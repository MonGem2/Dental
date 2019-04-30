using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Dental
{
    /// <summary>
    /// Interaction logic for Depther.xaml
    /// </summary>
    public partial class Depther : Window
    {
        public Depther()
        {
            InitializeComponent();
        }

        double max_sum=0;
        string ID="";
        int id_Patient=0;

        public Depther(double sum, string id, int id_Patient)
        {
            InitializeComponent();
            max_sum = sum;
            ID = id;
            this.id_Patient = id_Patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.Parse(Sum.Text) > max_sum)
                {
                    MessageBox.Show("Сумма больше долга!!!");
                }
                else if (double.Parse(Sum.Text) <= 0)
                {
                    MessageBox.Show("Сумма не можеть быть меньше или равной нулю!!!");
                }
                else
                {
                    string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try
                    {

                        string query="";
                        _con.Open();
                        if (max_sum != double.Parse(Sum.Text))
                        {
                            query = $"Update [Depth] set Suma='{max_sum - double.Parse(Sum.Text)}' where Id='{ID}'";

                            SQLiteConnection _con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                            _con1.Open();
                            string query1 = $"insert into [Transactions] (Suma,id_Patient,Date,Type) values ('{Sum.Text}','{id_Patient}','Неполное погашение долга')";
                            SQLiteCommand _cmd1 = new SQLiteCommand(query1, _con1);
                            _cmd1.ExecuteNonQuery();

                            _con1.Close();
                        }
                        else
                        {
                            query = $"Delete from Depth where Id='{ID}'";

                            SQLiteConnection _con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                            _con1.Open();
                            string query1 = $"insert into [Transactions] (Suma,Description,id_Patient,Date,Type) values ('{Sum.Text}','{id_Patient}','Погашение долга')";
                            SQLiteCommand _cmd1 = new SQLiteCommand(query1, _con1);
                            _cmd1.ExecuteNonQuery();

                            _con1.Close();
                        }
                        SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                        _cmd.ExecuteNonQuery();

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
