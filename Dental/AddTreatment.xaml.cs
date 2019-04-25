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
    public partial class AddTreatment : Window
    {
        public AddTreatment()
        {
            InitializeComponent();
        }
        string id_Patient;
        public AddTreatment(string id_Patient)
        {
            InitializeComponent();
            Date.Text = DateTime.Today.ToShortDateString();
            this.id_Patient = id_Patient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Descr.Text == string.Empty || Date.Text == string.Empty)
            {
                MessageBox.Show("Заполните поля!!!");
            }
            else
            {
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                _con.Open();
                string query = $"insert into [Treatment] (Date,Description,id_Patient) values ('{Date.Text}','{Descr.Text}','{id_Patient}')";
                SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                _cmd.ExecuteNonQuery();

                _con.Close();
                this.Close();
            }
        }
    }
}
