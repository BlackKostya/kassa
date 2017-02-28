using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassirManagerBase.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KassirManagerBase
{
    public class Core: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnNotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ProductEntity> allProductEntity
        {
            get { return BDManager.manager.getProductList(); }
        }
        private static Core _core = null;


        private Core(string path)
        {
            BDManager.init(path);
        }
        public static Core core
        {
            get { return _core; }
        }
        public static void init(string path)
        {
            _core = new Core(path);
        }
        public void updateProduct(ProductEntity p)
        {
            BDManager.manager.updateProduct(p);
            OnNotifyPropertyChanged("allProductEntity");
        }
        public void delateProduct(ProductEntity p)
        {
            BDManager.manager.deleteProduct(p);
            OnNotifyPropertyChanged("allProductEntity");
        }
        public void newProduct(ProductEntity p)
        {
            BDManager.manager.addProduct(p);
            OnNotifyPropertyChanged("allProductEntity");
        }
    }
}
