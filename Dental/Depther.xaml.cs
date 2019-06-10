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
                    DatabaseWorker.InsertPered((double.Parse(Sum.Text) - max_sum).ToString(), "Остаток из долга", id_Patient.ToString(), DateTime.Today.ToLongDateString());
                    DatabaseWorker.DeleteDepth(ID);
                    DatabaseWorker.InsertTransaction(max_sum.ToString(), "", id_Patient.ToString(), DateTime.Today.ToLongDateString(), "Погашение долга");
                    DatabaseWorker.InsertTransaction((double.Parse(Sum.Text) - max_sum).ToString(), "", id_Patient.ToString(), DateTime.Today.ToLongDateString(), "Добавление предоплаты");
                    this.Close();
                }
                else if (double.Parse(Sum.Text) <= 0)
                {
                    MessageBox.Show("Сумма не можеть быть меньше или равной нулю!!!");
                }
                else
                {
                    try
                    {
                       

                        if (max_sum > double.Parse(Sum.Text))
                        {
                            
                            DatabaseWorker.ReduceDepth(ID, Sum.Text);
                            DatabaseWorker.InsertTransaction(Sum.Text, "", id_Patient.ToString(), DateTime.Today.ToLongDateString(), "Неполное погашение долга");
                        }
                        else
                        {
                            DatabaseWorker.DeleteDepth(ID);
                            DatabaseWorker.InsertTransaction(Sum.Text, "", id_Patient.ToString(), DateTime.Today.ToLongDateString(), "Погашение долга");
                            
                        }
                        this.Close();
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
