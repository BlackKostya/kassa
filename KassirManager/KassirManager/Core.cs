using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassirManagerBase.Entity;

namespace KassirManager
{ 
    public class Check
    {
        public CheckEntity entity;
        public int id { get { return entity.checkId; } }
        public bool clouse { get { return entity.clouse; } }
        public double price {
            get
            {
                double r = 0;
                foreach (PurchaseEntity p in entity.Purchases)
                {
                    r += p.bayCount * p.product.price;
                }
                return r;
            }
        }
        public double count
        {
            get
            {
                return entity.Purchases.Count;
            }
        }
        public List<PurchaseEntity> purchase { get { return entity.Purchases; } }
        public Check(CheckEntity entity)
        {
            this.entity = entity;
        }
    }
    public class Core
    {
        public List<Check> allChecks
        {
            get {
                List<Check> r = new List<Check>();
                BDManager.manager.beginTranc();
                List<CheckEntity> l = BDManager.manager.getCheckList();
                foreach(CheckEntity o in l)
                {
                    BDManager.manager.getChildren(o);
                    r.Add(new Check(o));
                }
                BDManager.manager.Commit();
                return r;
            }
        }
        public List<ProductEntity> existProducts
        {
            get
            {
                return BDManager.manager.getExistProductList();
            }
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
        public Check getOpenCheck()
        {
            List<CheckEntity> l = BDManager.manager.getOpenCheckList();
            if (l.Count == 0) return null;
            CheckEntity o = l.First();
            BDManager.manager.getChildren(o);
            return new Check(o);
        }
        public bool bay(ProductEntity p, int count, Check c)
        {
            if (count > p.count) return false;
            else
            {
                BDManager.manager.beginTranc();
                p.count -= count;
                updateProduct(p);
                PurchaseEntity o = new PurchaseEntity
                {
                    bayCount = count,
                    check = c.entity,
                    checkId = c.entity.checkId,
                    product = p,
                    productId = p.productId
                };
                BDManager.manager.addPurchase(o);
                BDManager.manager.Commit();
                BDManager.manager.getChildren(c.entity);
                return true;
            }
        }
        public void updateProduct(ProductEntity p)
        {
            BDManager.manager.updateProduct(p);
        }
        public void delateProduct(ProductEntity p)
        {
            BDManager.manager.deleteProduct(p);
        }
        public Check newCheck()
        {
            CheckEntity e = new CheckEntity { clouse = false };
            BDManager.manager.addCheck(e);
            BDManager.manager.getChildren(e);
            Check c = new Check(e);
            return c;
        }
        public void ClouseCheck(Check c)
        {
            c.entity.clouse = true;
            BDManager.manager.updateCheck(c.entity);
        }
    }

}
