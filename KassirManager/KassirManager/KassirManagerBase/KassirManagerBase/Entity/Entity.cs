using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassirManagerBase.Entity
{
    public class CheckEntity
    {
        [PrimaryKey, AutoIncrement]
        public int checkId { get; set; }
        public bool clouse { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
        public List<PurchaseEntity> Purchases { get; set; }
    }

    public class ProductEntity
    {
        [PrimaryKey, AutoIncrement]
        public int productId { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]      // One to many relationship with Valuation
        public List<PurchaseEntity> Purchases { get; set; }
    }

    public class PurchaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int PurchaseId { get; set; }
        public int bayCount { get; set; }

        [ForeignKey(typeof(CheckEntity))]
        public int checkId { get; set; }
        [ManyToOne]
        public CheckEntity check { get; set; }

        [ForeignKey(typeof(ProductEntity))]
        public int productId { get; set; }
        [ManyToOne]
        public ProductEntity product { get; set; }
    }
}
