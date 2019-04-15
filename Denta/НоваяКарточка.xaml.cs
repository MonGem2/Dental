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
using System.Windows.Shapes;

namespace Denta
{
    /// <summary>
    /// Interaction logic for НоваяКарточка.xaml
    /// </summary>
    public partial class НоваяКарточка : Window
    {
        public НоваяКарточка()
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
            Пациенты пациенты = new Пациенты();
            Hide();
            пациенты.ShowDialog();
            Close();
        }

        private void Label_MouseDoubleClick_3(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Вы в этой вкладке");
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
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string Surname, Name, FatherName, Genderrr, DobTel, RabTel, MobTel, Zametki;
            DateTime date = new DateTime();
            DateTime date1 = new DateTime();
            if (SurnameT.Text=="")
            {
                Surname = "-";
            }
            else
            {
                Surname = SurnameT.Text;
            }
            if (NameT.Text == "")
            {
                Name = "-";
            }
            else
            {
                Name = NameT.Text;
            }
            if (FatherT.Text == "")
            {
                FatherName = "-";
            }
            else
            {
                FatherName = FatherT.Text;
            }
            if (Genderr.Text == "")
            {
                Genderrr = "-";
            }
            else
            {
                Genderrr = Genderr.Text;
            }
            if (DomT.Text == "")
            {
                DobTel = "-";
            }
            else
            {
                DobTel = DomT.Text;
            }
            if (RabT.Text == "")
            {
                RabTel = "-";
            }
            else
            {
                RabTel = RabT.Text;
            }
            if (MobT.Text == "")
            {
                MobTel = "-";
            }
            else
            {
                MobTel = MobT.Text;
            }
            if (Zamet.Text == "")
            {
                Zametki = "-";
            }
            else
            {
                Zametki = Zamet.Text;
            }
            if (Bith.Text == "")
            {
                date = DateTime.MinValue;
            }
            else
            {
                try
                {
                    date = DateTime.Parse(Bith.Text);
                }
                catch
                {

                }
            }
            if (Today.Text == "")
            {
                date1 = DateTime.Now;
            }
            else
            {
                try
                {
                    date1 = DateTime.Parse(Today.Text);
                }
                catch
                {

                }
            }
            DbContext.Add(Surname, Name, FatherName, Genderrr, DobTel, RabTel, MobTel, Zametki, date, date1);
            MessageBox.Show("Добавлено успешно!");
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Today.Text = DateTime.Today.ToShortDateString();
        }
    }
}
