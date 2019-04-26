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
    /// Interaction logic for Card.xaml
    /// </summary>
    public partial class Card : Page
    {
        public Card()
        {
            InitializeComponent();
        }

        public Card(string Name,string Surname,string FatherName,string id)
        {
            InitializeComponent();
            Title = "Карточка: " + Surname + "  " + Name + "  " + FatherName;
            names.Text += Surname +"  "+ Name +"  "+ FatherName;


            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";

            string text = $"Select [Date] From [Patients] where Id='{id}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Info.Text += "Дата создания карты: " + dataReader.GetString(0) + "\n";
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            text = $"Select [Date_Birth] From [Patients] where Id='{id}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Info.Text += "Дата рождения: " + dataReader.GetString(0)+"\n";
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            text = $"Select [Mobile_Phone] From [Patients] where Id='{id}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Info.Text += "Мобильный: " + dataReader.GetString(0) + "\n";
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            text = $"Select [Home_Phone] From [Patients] where Id='{id}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Info.Text += "Домашний: " + dataReader.GetString(0) + "\n";
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            text = $"Select [Work_Phone] From [Patients] where Id='{id}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Info.Text += "Робочий: " + dataReader.GetString(0) + "\n";
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            text = $"Select [Description] From [Patients] where Id='{id}'";
            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Info.Text += "Описание: " + dataReader.GetString(0);
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            text = $"Select [Date] From [Treatment] where id_Patient='{id}'";

            try
            {
                SQLiteConnection con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                con.Open();
                SQLiteCommand comand = new SQLiteCommand(text, con);
                SQLiteDataReader dataReader = comand.ExecuteReader();
                while (dataReader.Read())
                {
                    Treatment.Text += "Дата: " + dataReader.GetString(0) + "\n";
                    text = $"Select [Description] From [Treatment] where id_Patient='{id}' and Date='{dataReader.GetString(0)}'";

                    try
                    {
                        SQLiteConnection con1 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                        con1.Open();
                        SQLiteCommand comand1 = new SQLiteCommand(text, con1);
                        SQLiteDataReader dataReader1 = comand1.ExecuteReader();
                        while (dataReader1.Read())
                        {
                            Treatment.Text += "Описание: " + dataReader1.GetString(0) + "\n";
                            text = $"Select [Price] From [Treatment] where id_Patient='{id}' and Date='{dataReader.GetString(0)}'";

                            try
                            {
                                SQLiteConnection con2 = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                                con2.Open();
                                SQLiteCommand comand2 = new SQLiteCommand(text, con2);
                                SQLiteDataReader dataReader2 = comand2.ExecuteReader();
                                while (dataReader2.Read())
                                {
                                    Treatment.Text += "Цена: " + dataReader2.GetString(0) + "\n";
                                }
                                con2.Dispose();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                            }
                        }
                        con1.Dispose();
                        
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                con.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this).NavigationService.GoBack();
            (this).NavigationService.RemoveBackEntry();
        }
    }
}
