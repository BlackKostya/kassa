using Microsoft.Win32;
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

namespace KassirManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
        private void OpenDateBaseButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Data Base file (*.db)|*.db";
            if (d.ShowDialog() == true)
            {
                Core.init(d.FileName);
                Check c = Core.core.getOpenCheck();
                if (c == null)
                {
                    CheckWindow a = new CheckWindow();
                    a.Show();
                }
                else
                {
                    NewCheckWindow a = new NewCheckWindow();
                    a.selectid = c;
                    a.Show();
                }
                this.Close();
            }
        }
    }
}
