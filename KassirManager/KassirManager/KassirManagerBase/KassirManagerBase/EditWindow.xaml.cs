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
using KassirManagerBase.Entity;
using System.Collections.ObjectModel;

namespace KassirManagerBase
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public List<ProductEntity> allProductEntity
        {
            get {return Core.core.allProductEntity; }
        }
        public EditWindow()
        {
            InitializeComponent();
        }

        private void updateButtonClick(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                ProductEntity p = listView.SelectedItem as ProductEntity;
                if (string.IsNullOrEmpty(nameTextBox.Text))
                {
                    nameTextBox.Focus();
                    return;
                }
                p.name = nameTextBox.Text;
                int u;
                if (int.TryParse(countTextBox.Text, out u))
                {
                    p.count = u;
                }else
                {
                    countTextBox.Focus();
                    return;
                }
                Core.core.updateProduct(p);
                listView.ItemsSource = Core.core.allProductEntity;
            }
        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
            {
                ProductEntity p = listView.SelectedItem as ProductEntity;
                Core.core.delateProduct(p);
                listView.ItemsSource = Core.core.allProductEntity;
            }
        }

        private void newButtonClick(object sender, RoutedEventArgs e)
        {
            ProductEntity p = new ProductEntity();
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                nameTextBox.Focus();
                return;
            }
            p.name = nameTextBox.Text;
            int u;
            if (int.TryParse(countTextBox.Text, out u))
            {
                p.count = u;
            }
            else
            {
                countTextBox.Focus();
                return;
            }
            Core.core.newProduct(p);
            listView.ItemsSource = Core.core.allProductEntity;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listView.ItemsSource = Core.core.allProductEntity;
        }
    }
}
