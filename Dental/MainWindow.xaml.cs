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

namespace Dental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Patients_Click(object sender, RoutedEventArgs e)
        {
            Pager.Content = new Patients();
        }
        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            Pager.Content = new Depth();
        }
        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            Pager.Content = new Transactions();
        }
    }
}
