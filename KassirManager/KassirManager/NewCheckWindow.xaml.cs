using KassirManagerBase.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для NewCheckWindow.xaml
    /// </summary>
    public partial class NewCheckWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Check _selectid;
        public Check selectid
        {
            get { return _selectid; }
            set
            {
                _selectid = value;
                OnPropertyChanged("selectid");
            }
        }
        public List<ProductEntity> list
        {
            get
            {
                return Core.core.existProducts;
            }
        }

        public NewCheckWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void addClickButton(object sender, RoutedEventArgs e)
        {
            int u;
            if(!int.TryParse(productCountText.Text,out u)){
                productComboBox.Focus();
            }
            if (!Core.core.bay(productComboBox.SelectedItem as ProductEntity, u, selectid))
            {
                productCountText.Focus();
            }
            else
            {
                OnPropertyChanged("selectid");
            }
        }

        private void ClouseButtonClick(object sender, RoutedEventArgs e)
        {
            Core.core.ClouseCheck(selectid);
            CheckWindow w = new CheckWindow();
            w.reloadList();
            w.Show();
            this.Close();
        }
    }
}
