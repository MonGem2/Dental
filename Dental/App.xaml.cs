using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dental
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            if (DateTime.Now >= new DateTime(2019,6,24))
            {
                MessageBox.Show("Time is over!!!");
                return;
            }
            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
