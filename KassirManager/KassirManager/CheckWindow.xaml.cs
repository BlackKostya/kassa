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

namespace KassirManager
{
    /// <summary>
    /// Логика взаимодействия для CheckWindow.xaml
    /// </summary>
    public partial class CheckWindow : Window
    {
        public void reloadList()
        {
            listView.ItemsSource = Core.core.allChecks;
        }
        public CheckWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            reloadList();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            Check c = Core.core.newCheck();
            NewCheckWindow w = new NewCheckWindow();
            w.selectid = c;
            w.Show();
            this.Close();
        }
    }
}
