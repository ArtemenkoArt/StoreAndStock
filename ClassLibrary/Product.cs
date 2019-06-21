using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Product : IComparable<Product>
    {
        public string Name { get; }
        public decimal Cost { get; }
        public int ExpDay { get; }
        public int BaseQty { get; private set; }
        public Product(string name, decimal cost, int expDay)
        {
            this.Name = name;
            this.Cost = cost;
            this.ExpDay = expDay;
        }

        public void SetBaseQty(int qty)
        {
            this.BaseQty = qty;
        }
        public int CompareTo(Product product)
        {
            return this.Name.CompareTo(product.Name);
        }
    }

}
