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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dental
{
    /// <summary>
    /// Interaction logic for New_Card.xaml
    /// </summary>
    public partial class New_Card : Page
    {
        public New_Card()
        {
            InitializeComponent();
            var t = from TabItem el in MainWindow.Pager.Items where (el.Content as Frame).Content == this select el;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = from TabItem el in MainWindow.Pager.Items where (el.Content as Frame).Content == this select el;
            MainWindow.Pager.Items.Remove(t.First());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (surname.Text == string.Empty)
            {
                MessageBox.Show("Surname can`t be empty!!!");
            }
            else
            {
                string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
                SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                _con.Open();
                string query = $"insert into [Patients] (Name,Surname,FatherName,Gender,Mobile_Phone,Home_Phone,Work_Phone,Date_Birth,Description,Date) values ('{name.Text}','{surname.Text}','{fathername.Text}','{gender.Text}','{mobphone.Text}','{homephone.Text}','{workphone.Text}','{birth.Text}','{descr.Text}','{DateTime.Today.ToShortDateString()}')";
                SQLiteCommand _cmd = new SQLiteCommand(query, _con);
                _cmd.ExecuteNonQuery();

                _con.Close();

                var t = from TabItem el in MainWindow.Pager.Items where (el.Content as Frame).Content == this select el;
                MainWindow.Pager.Items.Remove(t.First());
            }
        }
    }
}
