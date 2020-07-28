using System;
using System.Collections.Generic;
using WebStore.Managers;

namespace WebStore.Entities
{
    public class Administrator : Account
    {
        public Administrator(int id, string name, string surname, int age, string password, string email, RoleTypes roleType) 
            : base(id, name, surname, age, password, email, roleType) 
        { }
        public List<Order> orders = new List<Order>();
        public static List<Order> approvedOrders = new List<Order>();
        
        public override void ShowMenu()
        {
            RunAdminMenu(commandInfoArray);
        }

        private static void RunAdminMenu(CommandInfo[] commandsArray)
        {
            int choice = -1;
            while (choice != 8)
            {
                Console.Clear();
                ShowCommandsMenu(commandsArray);

                choice = EnterCommand(commandsArray);
                try
                {
                    commandsArray[choice].command();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }

        static CommandInfo[] commandInfoArray = {
            new CommandInfo("Show all comodities", CommodityManager.ShowCommodities),
            new CommandInfo("Find comodity by the name", CommodityManager.FindCommodityByName),
            new CommandInfo("Create new order", OrderManager.CreateOrder),
            new CommandInfo("Checkout", OrderManager.Checkout),
            new CommandInfo("Review and editing user's personal data",AccountManager.EnterTheAccount),
            new CommandInfo("Adding a new commodity (name, category, description, price)", CommodityManager.AddCommodity),
            new CommandInfo("Editing commodity data",CommodityManager.EditCommodityData),
            new CommandInfo("Change the status of order", OrderManager.ChangeTheStatus),
            new CommandInfo("Exit from account.", AccountManager.LogOut),
        };

        private static void ShowCommandsMenu(CommandInfo[] commandsArray)
        {
            Console.WriteLine("***  ****  *****  Welcome to admin's menu.  *****  ****  ***:\n");
            for (int i = 0; i < commandsArray.Length; i++)
            {
                Console.WriteLine("\t\t{0} - {1}", i, commandInfoArray[i].name);
            }
        }

        private static int EnterCommand(CommandInfo[] commandsArray)
        {
            Console.Write("\nEnter the number of command:\n");
            int number = ConsoleManager.ReadInt();
            if (number < commandsArray.Length)
            {
                return number;
            }
            else if (number < 0)
            {
                throw new ArgumentException("Number cannot be less than zero.");
            }
            else
            {
                string exceptionStr = String.Format("Number must be less than {0}.", commandsArray.Length);
                throw new ArgumentException(exceptionStr);
            }
        }
    }
}
