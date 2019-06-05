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
            
            View.ItemsSource = DatabaseWorker.SelectDepth().Tables[0].DefaultView;
            View1.ItemsSource = DatabaseWorker.SelectPered().Tables[0].DefaultView;
            
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
                View.ItemsSource = DatabaseWorker.SelectDepth().Tables[0].DefaultView;
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
                    DataRowView row = (DataRowView)View.SelectedItems[0];
                    DatabaseWorker.InsertTransaction(row["Suma"].ToString(), row["Description"].ToString(), row["id_Patient"].ToString(), row["Date"].ToString(), "Погашение долга");
                    DatabaseWorker.DeleteDepth(row["id"].ToString());
                    View.ItemsSource = DatabaseWorker.SelectDepth().Tables[0].DefaultView;
                   
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
                            View.ItemsSource = DatabaseWorker.SelectDepth().Tables[0].DefaultView;                            
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
                try
                {
                    View1.ItemsSource = DatabaseWorker.SelectPered().Tables[0].DefaultView;
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //DatabaseWorker.InsertTransaction("100", "dsfsdf", "1", "asfds", "sfdsdjkfsnk");
            if (View1.SelectedItems.Count != 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите принять эту предоплату?", "Подтверждение", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView row = (DataRowView)View1.SelectedItems[0];
                        
                        try
                        {
                            DatabaseWorker.InsertTransaction(row["Suma"].ToString(), row["Description"].ToString(), row["id_Patient"].ToString(), row["Date"].ToString(), "Принятие предоплаты");
                            DatabaseWorker.DeletePered(row["id"].ToString());                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            try
                            {
                                View1.ItemsSource = DatabaseWorker.SelectPered().Tables[0].DefaultView;
                             
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
            Patient patient = DatabaseWorker.getPatient(((DataRowView)View.SelectedItems[0])["id_Patient"].ToString());            
            string tmp = string.Empty;
            tmp = "Карточка:" + patient.Name + " "+patient.Surname+" "+patient.FatherName;         
            TabItem tb = new TabItem() { Header = tmp, Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["id_Patient"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }



        private void View_MouseDoubleClick1(object sender, MouseButtonEventArgs e)
        {
            Patient patient = DatabaseWorker.getPatient(((DataRowView)View1.SelectedItems[0])["id_Patient"].ToString());
            
            string tmp = string.Empty;
            tmp = "Карточка:" + patient.Name + " "+patient.Surname+" "+patient.FatherName;            
            TabItem tb = new TabItem() { Header = tmp, Content = new Frame() { Content = new Card(((DataRowView)View1.SelectedItems[0])["id_Patient"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }

    }
}
