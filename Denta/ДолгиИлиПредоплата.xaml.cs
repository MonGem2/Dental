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
    /// Interaction logic for ДолгиИлиПредоплата.xaml
    /// </summary>
    public partial class ДолгиИлиПредоплата : Window
    {
        public ДолгиИлиПредоплата()
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
            НоваяКарточка новаяКарточка = new НоваяКарточка();
            Hide();
            новаяКарточка.ShowDialog();
            Close();
        }


        private void Label_MouseDoubleClick_5(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Вы в этой вкладке");
        }
    }
}
