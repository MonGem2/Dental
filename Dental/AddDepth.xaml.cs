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
    /// Interaction logic for AddTreatment.xaml
    /// </summary>
    public partial class AddDepth : Window
    {
        public AddDepth()
        {
            InitializeComponent();
            Date.SelectedDate = DateTime.Today;
        }
        string id_Patient;
        public AddDepth(string id_Patient)
        {
            InitializeComponent();
            Price.Text = DateTime.Today.ToShortDateString();
            this.id_Patient = id_Patient;
            Id_Pat.Text = id_Patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Price.Text == string.Empty || Id_Pat.Text == string.Empty)
            {
                MessageBox.Show("Заполните поля!!!");
            }
            else
            {
                Price.Text.Replace('.', ',');
                try
                {
                    string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    _con.Open();
                    string query = $"insert into [Depth] (Suma,Description,id_Patient,Date) values ('{Price.Text}','{Descr.Text}','{Id_Pat.Text}','{Date.Text}')";
                    SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                    _cmd.ExecuteNonQuery();

                    _con.Close();


                    SQLiteConnection _con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    _con1.Open();
                    string query1 = $"insert into [Transactions] (Suma,Description,id_Patient,Date,Type) values ('{Price.Text}','{Descr.Text}','{Id_Pat.Text}','{Date.Text}','Добавлен долг')";
                    SQLiteCommand _cmd1 = new SQLiteCommand(query1, _con1);
                    _cmd1.ExecuteNonQuery();

                    _con1.Close();
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
