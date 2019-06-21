using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Customer
    {
        public List<string> Buy(List<Product> products, Stock stock)
        {
            int productId = Rnd.GetRandom(1, stock.Count);
            int qty = Rnd.GetRandom(1, 4);
            return stock.Remove(products[productId], qty);
        }
    }
}
