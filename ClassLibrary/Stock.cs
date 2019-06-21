using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Stock : SortedDictionary<Product, Queue<DateTime>>
    {
        static object lockObj = new object();
        public decimal Value { get; private set; } = 0;

        public void Add(Product product, int qty, DateTime dateTime)
        {
            lock (lockObj)
            {
                if (!this.ContainsKey(product))
                {
                    Add(product, new Queue<DateTime>());
                }
                for (int i = 1; i < qty; i++)
                {
                    Value++;
                    this[product].Enqueue(dateTime);
                }
            }
        }

        public List<string> Remove(Product product, int qty)
        {
            List<string> msg = new List<string>();
            lock (lockObj)
            {
                if (ContainsKey(product))
                {
                    for (int i = 1; i < qty; i++)
                    {
                        if (this[product].Count > 0)
                        {
                            this[product].Dequeue();
                            Value--;
                        }
                        else
                        {
                            msg.Add($"Not enough {product.Name} - {qty - i}");
                            break;
                        }
                    }
                }
            }
            return msg;
        }

        public List<string> CheckExpDateProduct(DateTime currentDate)
        {
            List<string> msg = new List<string>();
            foreach (var product in this.Keys)
            {
                int expDay = product.ExpDay;
                while (true)
                {
                    if (this[product].Peek().Date.AddDays(expDay) > currentDate)
                    {
                        break;
                    }
                    msg.Add($"ExpDate {product.Name}");

                    this[product].Dequeue();
                }
            }
            return msg;
        }

        public SortedDictionary<Product, int> GetBalanceReport()
        {
            SortedDictionary<Product, int> balance = new SortedDictionary<Product, int>();
            lock (lockObj)
            {
                foreach (var item in this)
                {
                    balance.Add(item.Key, item.Value.Count);
                }
            }
            return balance;
        }

        public new void Clear()
        {
            Value = 0;
            this.Clear();
        }


    }
}
