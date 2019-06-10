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
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Показать пациента";
            menuItem.Click += OpenPatient;
            contextMenu.Items.Add(menuItem);
            menuItem = new MenuItem();
            menuItem.Header = "Погасить долг";
            menuItem.Click += Pay_the_debt_off;
            contextMenu.Items.Add(menuItem);
            View.ContextMenu = contextMenu;



            contextMenu = new ContextMenu();
            menuItem = new MenuItem();
            menuItem.Header = "Показать пациента";
            menuItem.Click += Open_Patient;
            contextMenu.Items.Add(menuItem);
            menuItem = new MenuItem();
            menuItem.Header = "Принять предоплату";
            menuItem.Click += Accept_prepayment;
            contextMenu.Items.Add(menuItem);
            View1.ContextMenu = contextMenu;
        }

        private void Page_Loaded(object sender, object e)
        {
            DataTable dt = DatabaseWorker.SelectDepth().Tables[0];
            dt.Columns["Id"].ColumnName = "Идентификатор";
            dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
            dt.Columns["Description"].ColumnName = "Описание";
            dt.Columns["Date"].ColumnName = "Дата";
            dt.Columns["Suma"].ColumnName = "Сума";
            View.ItemsSource = dt.DefaultView;
            dt = DatabaseWorker.SelectPered().Tables[0];
            dt.Columns["Id"].ColumnName = "Идентификатор";
            dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
            dt.Columns["Description"].ColumnName = "Описание";
            dt.Columns["Date"].ColumnName = "Дата";
            dt.Columns["Suma"].ColumnName = "Сума";
            View1.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, object e)
        {
            MainWindow.Pager.Items.Remove(MainWindow.tb2);
        }

        private void AddDepth(object sender, object e)
        {
            try
            {
                (new AddDepth()).ShowDialog();
            }
            catch { }
            finally
            {
                DataTable dt = DatabaseWorker.SelectDepth().Tables[0];
                dt.Columns["Id"].ColumnName = "Идентификатор";
                dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
                dt.Columns["Description"].ColumnName = "Описание";
                dt.Columns["Date"].ColumnName = "Дата";
                dt.Columns["Suma"].ColumnName = "Сума";
                View.ItemsSource = dt.DefaultView;
            }
        }

        private void Pay_the_debt_off(object sender, object e)
        {

            string path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + @"\Base\Denta.db";
            if (View.SelectedItems.Count!=0)
            {
                //MessageBoxResult dialogResult = MessageBox.Show("Вы хотите полностью погасить этот долг?", "Подтверждение", MessageBoxButton.YesNoCancel);
                //if (dialogResult == MessageBoxResult.Yes)
                //{
                //    DataRowView row = (DataRowView)View.SelectedItems[0];
                //    DatabaseWorker.InsertTransaction(row["Suma"].ToString(), row["Description"].ToString(), row["id_Patient"].ToString(), row["Date"].ToString(), "Погашение долга");
                //    DatabaseWorker.DeleteDepth(row["id"].ToString());
                //    View.ItemsSource = DatabaseWorker.SelectDepth().Tables[0].DefaultView;
                //   
                //}
               // if (dialogResult == MessageBoxResult.No)
               // {
                    SQLiteConnection _con = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                    try {
                        DataRowView row = (DataRowView)View.SelectedItems[0];
                        (new Depther(double.Parse(row["Сума"].ToString()),row["Идентификатор"].ToString(),int.Parse(row["Ид.пациента"].ToString()))).ShowDialog();
                        }
                    catch { }
                    finally
                    {
                        try
                    {
                        DataTable dt = DatabaseWorker.SelectDepth().Tables[0];
                        dt.Columns["Id"].ColumnName = "Идентификатор";
                        dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
                        dt.Columns["Description"].ColumnName = "Описание";
                        dt.Columns["Date"].ColumnName = "Дата";
                        dt.Columns["Suma"].ColumnName = "Сума";
                        View.ItemsSource = dt.DefaultView;
                        dt = DatabaseWorker.SelectPered().Tables[0];
                        dt.Columns["Id"].ColumnName = "Идентификатор";
                        dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
                        dt.Columns["Description"].ColumnName = "Описание";
                        dt.Columns["Date"].ColumnName = "Дата";
                        dt.Columns["Suma"].ColumnName = "Сума";
                        View1.ItemsSource = dt.DefaultView;
                    }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                //}
            }
        }

        private void Add_Pered(object sender, object e)
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
                    DataTable dt = DatabaseWorker.SelectPered().Tables[0];
                    dt.Columns["Id"].ColumnName = "Идентификатор";
                    dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
                    dt.Columns["Description"].ColumnName = "Описание";
                    dt.Columns["Date"].ColumnName = "Дата";
                    dt.Columns["Suma"].ColumnName = "Сума";
                    View1.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Accept_prepayment(object sender, object e)
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
                            DatabaseWorker.InsertTransaction(row["Сума"].ToString(), row["Описание"].ToString(), row["Ид.пациента"].ToString(), row["Дата"].ToString(), "Принятие предоплаты");
                            DatabaseWorker.DeletePered(row["Идентификатор"].ToString());                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            try
                            {
                                DataTable dt = DatabaseWorker.SelectPered().Tables[0];
                                dt.Columns["Id"].ColumnName = "Идентификатор";
                                dt.Columns["id_Patient"].ColumnName = "Ид.пациента";
                                dt.Columns["Description"].ColumnName = "Описание";
                                dt.Columns["Date"].ColumnName = "Дата";
                                dt.Columns["Suma"].ColumnName = "Сума";
                                View1.ItemsSource = dt.DefaultView;
                             
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

        private void OpenPatient(object sender, object e)
        {
            Patient patient = DatabaseWorker.getPatient(((DataRowView)View.SelectedItems[0])["Ид.пациента"].ToString());            
            string tmp = string.Empty;
            tmp = "Карточка:" + patient.Name + " "+patient.Surname+" "+patient.FatherName;         
            TabItem tb = new TabItem() { Header = tmp, Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Ид.пациента"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }



        private void Open_Patient(object sender, object e)
        {
            Patient patient = DatabaseWorker.getPatient(((DataRowView)View1.SelectedItems[0])["Ид.пациента"].ToString());
          
            string tmp = string.Empty;
            tmp = "Карточка:" + patient.Name + " "+patient.Surname+" "+patient.FatherName;            
            TabItem tb = new TabItem() { Header = tmp, Content = new Frame() { Content = new Card(((DataRowView)View1.SelectedItems[0])["Ид.пациента"].ToString()) } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }

    }
}
