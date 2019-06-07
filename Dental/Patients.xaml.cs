using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Dental
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page
    {
        public Patients()
        {
            InitializeComponent();
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Открить";
            menuItem.Click +=OpenPatient;
            contextMenu.Items.Add(menuItem);
            menuItem = new MenuItem();
            menuItem.Header = "Новое лечение";
            menuItem.Click += NewTreatment;
            contextMenu.Items.Add(menuItem);
            menuItem = new MenuItem();
            menuItem.Header = "Удалить";
            menuItem.Click += DeletePatient;
            contextMenu.Items.Add(menuItem);
            View.ContextMenu = contextMenu;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            View.ItemsSource = DatabaseWorker.SelectPatients().Tables[0].DefaultView;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Pager.Items.Remove(MainWindow.tb);
        }

        private void DeletePatient(object sender, object e)
        {
            if (View.SelectedItems.Count != 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Вы действительно хотите удалить этого пациента и все что с ним связано?", "Подтверждение", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    DataRowView row = (DataRowView)View.SelectedItems[0];
                    DatabaseWorker.DeletePatient(row["id"].ToString());
                    View.ItemsSource = DatabaseWorker.SelectPatients().Tables[0].DefaultView;                  
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
             try
             {
                 
                 View.ItemsSource = DatabaseWorker.FindPatient(textb.Text).DefaultView;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
        }

        private void OpenPatient(object sender, RoutedEventArgs e)
        {   
            try
            {
                TabItem tb = new TabItem() { Header = "Карточка: " + ((DataRowView)View.SelectedItems[0])["Name"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Surname"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString()) } };
                MainWindow.Pager.Items.Add(tb);
                MainWindow.Pager.SelectedItem = tb;
            }
            catch { }
        }

        private void NewTreatment(object sender, RoutedEventArgs e)
        {
            try
            {
                (new AddTreatment(((DataRowView)View.SelectedItems[0])["Id"].ToString())).ShowDialog();
            }
            catch { }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            textb.Text = string.Empty;
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TabItem tb = new TabItem() { Header= "Карточка: " + ((DataRowView)View.SelectedItems[0])["Name"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["Surname"].ToString() + " " + ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), Content = new Frame() { Content = new Card(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString()) } };
                MainWindow.Pager.Items.Add(tb);
                tb.IsSelected = true;
            }
            catch { }
        }

        private void AddCard_Click(object sender, RoutedEventArgs e)
        { 
            TabItem tb = new TabItem() { Header = "Новая карточка", Content = new Frame() { Content = new New_Card() } };
            MainWindow.Pager.Items.Add(tb);
            MainWindow.Pager.SelectedItem = tb;
        }



        private void View_SourceUpdated(object sender, EventArgs e)
        {
            
            try
            {
                DatabaseWorker.UpdatePatient(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(),
                    ((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString(), ((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString(), ((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString(),
                    ((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString(), ((DataRowView)View.SelectedItems[0])["Gender"].ToString(), ((DataRowView)View.SelectedItems[0])["Card_Num"].ToString(), ((DataRowView)View.SelectedItems[0])["Description"].ToString(),
                    ((DataRowView)View.SelectedItems[0])["Date"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString());
                
            }
            catch (Exception ex)
            {
                
            }
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F5)
            {
                try
                {
                    try
                    {
                        DatabaseWorker.UpdatePatient(((DataRowView)View.SelectedItems[0])["Name"].ToString(), ((DataRowView)View.SelectedItems[0])["Surname"].ToString(), ((DataRowView)View.SelectedItems[0])["FatherName"].ToString(),
                            ((DataRowView)View.SelectedItems[0])["Mobile_Phone"].ToString(), ((DataRowView)View.SelectedItems[0])["Home_Phone"].ToString(), ((DataRowView)View.SelectedItems[0])["Work_Phone"].ToString(),
                            ((DataRowView)View.SelectedItems[0])["Date_Birth"].ToString(), ((DataRowView)View.SelectedItems[0])["Gender"].ToString(), ((DataRowView)View.SelectedItems[0])["Card_Num"].ToString(), ((DataRowView)View.SelectedItems[0])["Description"].ToString(),
                            ((DataRowView)View.SelectedItems[0])["Date"].ToString(), ((DataRowView)View.SelectedItems[0])["Id"].ToString());

                    }
                    catch (Exception ex)
                    {

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else if(e.Key == Key.F5)
            {
                View.ItemsSource = DatabaseWorker.SelectPatients().Tables[0].DefaultView;
            }
        }
    }
}
