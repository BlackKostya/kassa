using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassirManagerBase.Entity
{
    public class BDManager
    {
        private string path;
        private static BDManager _manager = null;
        private bool transaction;
        private SQLiteConnection connect;
        private BDManager(string path)
        {
            this.path = path;
            SQLiteConnection con = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            con.BeginTransaction();
            var tableQuery = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='CheckEntity'";
            bool tableExists = con.ExecuteScalar<int>(tableQuery) == 1;
            if (!tableExists)
            {
                con.CreateTable<CheckEntity>();
            }
            tableQuery = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='ProductEntity'";
            tableExists = con.ExecuteScalar<int>(tableQuery) == 1;
            if (!tableExists)
            {
                con.CreateTable<ProductEntity>();
            }
            tableQuery = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='PurchaseEntity'";
            tableExists = con.ExecuteScalar<int>(tableQuery) == 1;
            if (!tableExists)
            {
                con.CreateTable<PurchaseEntity>();
            }
            con.Commit();
            con.Close();
            this.transaction = false;
        }
        public static BDManager manager
        {
            get { return _manager; }
        }
        public static void init(string path)
        {
            _manager = new BDManager(path);
        }
        public void beginTranc()
        {
            connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            connect.BeginTransaction();
            transaction = true;
        }
        public void Commit()
        {
            connect?.Commit();
            connect?.Close();
            connect = null;
            transaction = false;
        }
        public void addProduct(ProductEntity p)
        {
            if(!transaction)connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            connect.Insert(p);
            if(!transaction)connect.Close();
        }
        public void updateProduct(ProductEntity p)
        {
            if(!transaction)connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            connect.Update(p);
            if(!transaction)connect.Close();
        }

        public void deleteProduct(ProductEntity p)
        {
            if (!transaction)
            {
                beginTranc();
            }
            connect.GetChildren<ProductEntity>(p);
            connect.DeleteAll(p.Purchases);
            connect.Delete(p);
            if (!transaction)
            {
                Commit();
            }
        }
        public List<ProductEntity> getExistProductList()
        {
            if (!transaction)
            {
                connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            }
            List<ProductEntity> l =connect.Query<ProductEntity>("SELECT * FROM ProductEntity WHERE count <> 0");
            if (!transaction)
            {
                connect.Close();
            }
            return l;
        }
        public List<ProductEntity> getProductList()
        {
            if (!transaction)
            {
                connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            }
            List<ProductEntity> l = connect.Table<ProductEntity>().ToList();
            if (!transaction)
            {
                connect.Close();
            }
            return l;
        }
        //------------------------------------------------------------------------//
        public void addCheck(CheckEntity p)
        {
            if (!transaction) connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            connect.Insert(p);
            if (!transaction) connect.Close();
        }
        public void updateCheck(CheckEntity p)
        {
            if (!transaction) connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            connect.UpdateWithChildren(p);
            if (!transaction) connect.Close();
        }

        public void deleteCheck(CheckEntity p)
        {
            if (!transaction)
            {
                beginTranc();
            }
            connect.GetChildren<CheckEntity>(p);
            connect.DeleteAll(p.Purchases);
            connect.Delete(p);
            if (!transaction)
            {
                Commit();
            }
        }
        public List<CheckEntity> getOpenCheckList()
        {
            if (!transaction)
            {
                connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            }
            List<CheckEntity> l = connect.Query<CheckEntity>("SELECT * FROM ProductEntity WHERE clouse = 0");
            if (!transaction)
            {
                connect.Close();
            }
            return l;
        }
        public List<CheckEntity> getCheckList()
        {
            if (!transaction)
            {
                connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            }
            List<CheckEntity> l = connect.Table<CheckEntity>().ToList();
            if (!transaction)
            {
                connect.Close();
            }
            return l;
        }
        public void getChildren(CheckEntity p)
        {
            if (!transaction)
            {
                connect = new SQLiteConnection(new SQLite.Net.Platform.Generic.SQLitePlatformGeneric(), path);
            }

            connect.GetChildren(p);
            if (!transaction)
            {
                connect.Close();
            }
        }
    }
}
