using System;
using WebStore.Entities;
using WebStore.Managers;

namespace WebStore
{
    class Program
    {

        static void Main(string[] args)
        {
            AccountManager.CreateAdmin();
            CommodityManager.commodities.Add(new Commodity(1, "aser", 5, DateTime.MaxValue, CommodityTypes.Computer));
            CommodityManager.commodities.Add(new Commodity(2, "hp", 5, DateTime.MaxValue, CommodityTypes.Keyboard));
            CommodityManager.commodities.Add(new Commodity(3, "dell", 5, DateTime.MaxValue, CommodityTypes.Monitor));
            CommodityManager.commodities.Add(new Commodity(4, "asus", 5, DateTime.MaxValue, CommodityTypes.Mouse));
            CommodityManager.commodities.Add(new Commodity(5, "lenovo", 5, DateTime.MaxValue, CommodityTypes.Camera));
            ConsoleManager.Run();
        }        
    }
}
