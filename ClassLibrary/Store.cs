using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class Store
    {
        public static Stock stock = new Stock();
        public static List<Product> products = new List<Product>()
            {
            new Product("Milk", 10.00M, 3),
            new Product("Kefir", 12.00M, 5),
            new Product("Yogurt", 15.00M, 7),
            new Product("Cheуse", 50.00M, 30),
            new Product("Butter", 20.00M, 21)
            };

        public static readonly List<Customer> customers = new List<Customer>();
        public static readonly List<Supplyer> supplyers = new List<Supplyer>();
        //public List<string> eventLog = new List<string>();
        public delegate void eventLogDelegate(string msg);
        public static event eventLogDelegate eventLog;

        static Store()
        {

        }

        public static void SelectBaseQtyProducts(List<Product> products, Stock stock)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Insert product and start qty:");
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{i + 1} { products[i].Name }");
                }

                //var ProductID = 1;
                Console.Write("Insert product: ");
                var ProductID = 0;
                var inputID = Console.ReadLine();
                int.TryParse(inputID, out ProductID);
                if (ProductID == 0)
                {
                    break;
                }

                //var qty = 50;

                Console.Write("Insert qty: ");
                var qty = 0;
                var inputQty = Console.ReadLine();
                int.TryParse(inputQty, out qty);
                if (qty == 0)
                {
                    break;
                }

                products[ProductID - 1].SetBaseQty(qty);

                stock.Add(products[ProductID - 1], qty, new DateTime(2019, 06, 23));
                products.Remove(products[ProductID - 1]);
            }
        }

        public static void FillCustomersAndSupplyers()
        {
            customers.Clear();
            for (int i = 1; i < Rnd.GetRandom(1, 16); i++)
            {
                customers.Add(new Customer());
            }

            supplyers.Clear();
            for (int i = 1; i < Rnd.GetRandom(1, 4); i++)
            {
                supplyers.Add(new Supplyer());
            }
        }

        public static async void StartDay(DateTime dateTime)
        {
            FillCustomersAndSupplyers();

            Task[] customersTask = new Task[customers.Count];
            for (int i = 0; i < customers.Count; i++)
            {
                int customerId = i;
                List<Product> listProduct = stock.Keys.ToList();
                customersTask[i] = Task.Factory.StartNew(() =>
                {

                    List<string> msg = customers[customerId].Buy(listProduct, Store.stock);
                    for (int n = 0; n < msg.Count; n++)
                    {
                        eventLog?.Invoke(msg[n]);
                    }
                });
            }
            Task.WaitAll(customersTask);

            Task[] supplayersTask = new Task[supplyers.Count];
            for (int i = 0; i < supplyers.Count; i++)
            {
                int supplayerId = i;
                supplayersTask[i] = Task.Factory.StartNew(() =>
                {
                    List<string> msg = supplyers[supplayerId].CheckQtyAndSupplyProduct(dateTime, Store.stock);
                    for (int n = 0; n < msg.Count; n++)
                    {
                        eventLog?.Invoke(msg[n]);
                    }
                });
            }
            Task.WaitAll(supplayersTask);
        }
    }
}
