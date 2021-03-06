﻿using System;
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
        string Id;
        public Card()
        {
            InitializeComponent();
        }

        public Card(string Name,string Surname,string FatherName,string id)
        {
            InitializeComponent();
            

            
            var t = from TabItem el in MainWindow.Pager.Items where (el.Content as Frame).Content == this select el;
            try
            {
                t.First().Header = Title;
            }
            catch{ }
            names.Text += Surname +"  "+ Name +"  "+ FatherName;
            Id = id;
            update();
            

            
        }

        public void update()
        {
            
            Patient patient = DatabaseWorker.getPatient(Id);
            Title = "";
            Info.Text = "";
            Treatment.Text = "";

            Title = "Карточка: " + patient.Name + "  " + patient.Surname + "  " + patient.FatherName;
            Info.Text += "Имя:"+patient.Name+"\n";
            Info.Text += "Фамилия:" + patient.Surname + "\n";
            Info.Text += "Отчество:" + patient.FatherName + "\n";
            Info.Text += "Пол:" + patient.Gender + "\n";
            Info.Text += "Дата создания карты: " + patient.Date + "\n";
            Info.Text += "Дата рождения: " + patient.Date_Birth + "\n";
            Info.Text += "Мобильный: " + patient.Mobile_Phone + "\n";
            Info.Text += "Домашний: " + patient.Home_Phone + "\n";
            Info.Text += "Робочий: " + patient.Work_Phone + "\n";
            Info.Text += "Описание: " + patient.Description;
            Treatment.Text = DatabaseWorker.GetTreatmentString(Id);
            //foreach(var el in DatabaseWorker.getPatientsTransactionString(Id))
            //Transact.Text += el+"\n";
            Transact.Content = new PatientDepth(Id);
        }

        public Card(string id)
        {
            InitializeComponent();
            Id = id;
            update();
            
           

            
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var t = from TabItem el in MainWindow.Pager.Items where (el.Content as Frame).Content == this select el;
            MainWindow.Pager.Items.Remove(t.First());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddTreatment(Id)).ShowDialog();
            }
            catch { }
            update();
        }
    }
}
