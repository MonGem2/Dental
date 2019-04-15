using System;
using System.Collections.Generic;
using System.Data;
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

namespace Denta
{
    /// <summary>
    /// Interaction logic for Пациенты.xaml
    /// </summary>
    public partial class Пациенты : Window
    {
        public Пациенты()
        {
            InitializeComponent();
        }
        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("If you have any questions, please contact me! Telegram: @programmer_smurf");
        }

        private void Label_MouseDoubleClick_1(object sender, MouseButtonEventArgs e) //Движение средств
        {
            ДвижениеСредств движениеСредств = new ДвижениеСредств();
            Hide();
            движениеСредств.ShowDialog();
            Close();
        }

        private void Label_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Вы в этой вкладке");
        }

        private void Label_MouseDoubleClick_3(object sender, MouseButtonEventArgs e)
        {
            НоваяКарточка новаяКарточка = new НоваяКарточка();
            Hide();
            новаяКарточка.ShowDialog();
            Show();
        }


        private void Label_MouseDoubleClick_5(object sender, MouseButtonEventArgs e)
        {
            ДолгиИлиПредоплата долги = new ДолгиИлиПредоплата();
            Hide();
            долги.ShowDialog();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            НоваяКарточка новая = new НоваяКарточка();
            Hide();
            новая.ShowDialog();
            Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //grid.ColumnWidth = 200;
            grid.ItemsSource = DbContext.selectFirtst();
           
            // grid.Column = 200;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView customer = (DataRowView)grid.SelectedItem;
                DbContext.Delete(customer[0].ToString());
                // MessageBox.Show(customer[0].ToString());
                MessageBox.Show("Удачно удалён");
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //update
        {

        }
    }
}
