using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Supplyer
    {
        public List<string> CheckQtyAndSupplyProduct(DateTime dateTime, Stock stock)
        {
            List<string> msg = new List<string>();
            foreach (var product in stock)
            {
                if (stock[product.Key].Count < (decimal)product.Key.BaseQty / 5) // (50 / 5 = 20%)
                {
                    stock.Add(product.Key, product.Key.BaseQty, dateTime);
                    msg.Add($"Supply {product.Key.Name} - {product.Key.BaseQty}");
                }
            }
            return msg;
        }
    }
}
