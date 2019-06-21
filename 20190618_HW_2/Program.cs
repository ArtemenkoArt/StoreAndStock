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

        static void Main(string[] args)
        {
            Store.stock = new Stock();
            Store.SelectBaseQtyProducts(Store.products, Store.stock);
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
