using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20190618_HW_2
{
    class Program
    {
        public static List<string> eventLog = new List<string>();

        public static List<Product> products = new List<Product>()
            {
            new Product("Milk", 10.00M, 3),
            new Product("Kefir", 12.00M, 5),
            new Product("Yogurt", 15.00M, 7),
            new Product("Cheese", 50.00M, 30),
            new Product("Butter", 20.00M, 21)
            };

        public static void SelectBaseQtyProducts(List<Product> products, Stock stock)
        {
            Store.products = products;
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

        static void Main(string[] args)
        {
            Store.stock = new Stock();
            SelectBaseQtyProducts(products, Store.stock);
            Store.eventLog += AddEvent;

            DateTime dateTime = new DateTime(2019, 6, 1);

            /* 
             * - Проверяем дату годности товаров и если нужно списываем
             * - Создаем рандомное кол-во покупателей от 5 до 15
             * - Создаем рандомное кол-во поставщиков от 1 до 3
             * - Запускаем покупателей и поставщиков в потоке в рамках дня
             * - Пауза для возможности просмотра результатов по дню (остатки и просмотр журнала событий)
             */
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                dateTime = dateTime.AddDays(1);

                Store.StartDay(dateTime);

                var result = 0;
                while (result == 0)
                {
                    Console.WriteLine($"Current date: {dateTime}");
                    Console.WriteLine("1. Next day");
                    Console.WriteLine("2. View balance");
                    Console.WriteLine("3. View log");
                    Console.WriteLine("4. Exit");

                    var input = Console.ReadLine();
                    int.TryParse(input, out result);
                    if (result == 0)
                    {
                        Console.ReadKey();
                    }

                    switch (result)
                    {
                        case 1:
                            break;
                        case 2:
                            ViewBalance();
                            break;
                        case 3:
                            ViewEvent();
                            break;
                        case 4:
                            exit = true;
                            break;
                        default:
                            break;
                    }
                }
                
            }
            Console.ReadLine();
        }

        static public void AddEvent(string msg) => eventLog.Add(msg);

        static public void ViewEvent()
        {
            Console.Clear();
            foreach (var msg in eventLog)
            {
                Console.WriteLine(msg);
            }
            Console.ReadLine();
            eventLog.Clear();
        }

        static public void ViewBalance()
        {
            Console.Clear();
            foreach (var item in Store.stock.GetBalanceReport())
            {
                Console.WriteLine($"{item.Key.Name} - {item.Value}");
            }
            Console.ReadLine();
        }
    }
}
