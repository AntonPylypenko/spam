using System;
using System.Collections.Generic;
using WebStore.Entities;

namespace WebStore.Managers
{
    public static class CommodityManager
    {
        private static int CountID = 0;
        static Action<List<Commodity>> ShowAllCommodities = ConsoleManager.ShowTheItems;
        public static List<Commodity> commodities = new List<Commodity>();

        public static void AddCommodity() 
        {
            (string name, double price, DateTime timeOfExpiration, CommodityTypes type) = ConsoleManager.TakeDataAboutCommodity();
            CountID = commodities.Count;
            commodities.Add(new Commodity(CountID, name, price, timeOfExpiration, type));
        }        

        public static void EditCommodityData()
        {
            CommodityManager.ShowCommodities();
            int choice = 0;
            int commodityID = ConsoleManager.GetID("commodity");
            Dictionary<int, Action<int>> commandsToChange = new Dictionary<int, Action<int>>(){
            {1, ChangeCommodityName},
            {2, ChangeCommodityPrice},
            {3, ChangeCommodityWarranty},
            {4, ChangeCommodityType}
            };

            while (choice != 5)
            {
                choice = ReadChoice();
                if (choice != 5)
                {
                    commandsToChange[choice](commodityID);
                }
            }
            
        }

        public static int ReadChoice() 
        {
            Console.WriteLine("Please, enter the number of menu:");
            Console.WriteLine("1. Edit commodity name");
            Console.WriteLine("2. Edit commodity price");
            Console.WriteLine("3. Edit commodity time of expiration");
            Console.WriteLine("4. Edit commodity type"); 
            Console.WriteLine("5. Exit");
            int choice = ConsoleManager.ReadInt();
            return choice;
        }


        public static void ChangeCommodityName(int commodityID)
        {
            commodities[commodityID].Name = TakeNameOfCommodity();
            Console.WriteLine("The name of user is changed to {0}", commodities[commodityID].Name);
        }

        public static string TakeNameOfCommodity()
        {
            Console.WriteLine("Please, enter commodity new name: ");
            string nameOfCommodity = Console.ReadLine();
            return nameOfCommodity;
        }

        public static void ChangeCommodityPrice(int commodityID)
        {
            Console.WriteLine("Please, enter commodity new price: ");
            commodities[commodityID].Price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("The price of commodity is changed to {0}", commodities[commodityID].Price);
        }

        public static void ChangeCommodityWarranty(int commodityID)
        {
            Console.WriteLine("Enter a date of waaranty expiration (e.g. 10/22/2020): ");
            commodities[commodityID].TimeOfExpiration = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("The date of waaranty expiration changed to {0}", commodities[commodityID].TimeOfExpiration);
        }

        public static void ChangeCommodityType(int commodityID)
        {
            Console.WriteLine("Enter a date of waaranty expiration (e.g. 10/22/2020): ");
            commodities[commodityID].Type = ConsoleManager.GetTheType();
            Console.WriteLine("The time of expiration is changed to {0}", commodities[commodityID].Type);
        }

        public static void ShowCommodities() 
        {
            ShowAllCommodities(commodities);
            Console.ReadKey();
        }

        public static void FindCommodityByName()
        {
            Console.WriteLine("Please enter the name of commodity: ");
            string nameOfCommodity = ConsoleManager.ReadString();
            foreach (var item in commodities)
            {
                if (nameOfCommodity.Equals(item.Name))
                {
                    Console.WriteLine("Your commodity is: {0}", item.Name);
                    Console.WriteLine("It's price is: {0}", item.Price);
                    Console.WriteLine("Date of waranty expiration: {0}", item.TimeOfExpiration.ToString());
                    Console.WriteLine("Type of commodity is: {0}", item.Type.ToString());
                    Console.WriteLine("Please, type enter to continue...)");
                    Console.ReadKey();
                    return;
                }
            }
        }

        public static Commodity FindCommodityByID(int Id)
        {
            foreach (var item in commodities)
            {
                if (item.ID.Equals(Id))
                {
                    return item;
                }
            }
            Console.WriteLine("There is no commodity with such ID.");
            return null;
        }

        public static Commodity FindCommodityByName(string name)
        {
            foreach (var item in commodities)
            {
                if (item.Name.Equals(name))
                {
                    return item;
                }
            }

            throw new ArgumentException("There is no such element.");
        }
    }
}
