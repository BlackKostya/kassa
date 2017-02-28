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

namespace KassirManagerBase
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

        private void NewBaseButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Data Base file (*.db)|*.db";
            if (saveFileDialog.ShowDialog() == true)
            {
                Core.init(saveFileDialog.FileName);
                EditWindow a = new EditWindow();
                a.Show();
                this.Close();
            }
        
        }
        private void OpenDateBaseButtonClick(object sender, RoutedEventArgs e)
        {

            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "Data Base file (*.db)|*.db";
            if (d.ShowDialog() == true)
            {
                Core.init(d.FileName);
                EditWindow a = new EditWindow();
                a.Show();
                this.Close();
            }
        }
    }
}
