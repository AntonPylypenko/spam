using System;
using System.Collections.Generic;
using WebStore.Managers;

namespace WebStore.Entities
{
    public class RegisteredUser : Account
    {
        public List<Order> orders = new List<Order>();

        static CommandInfo[] userCommandsArray = {
            new CommandInfo("Show all comodities", CommodityManager.ShowCommodities),
            new CommandInfo("Find comodity by the name", CommodityManager.FindCommodityByName),
            new CommandInfo("Create new order", OrderManager.CreateOrder),
            new CommandInfo("Ordering or cancelation", OrderManager.OrderOrCancelOrder),
            new CommandInfo("Show history of orders and status of delivery", OrderManager.ShowAllOrders),
            new CommandInfo("Make the status \"Delivered\"", OrderManager.MakeTheStatusReceived),
            new CommandInfo("Change personal info", ChangeMyPersonalData),
            new CommandInfo("Add money to balance", ChangeBalance),
            new CommandInfo("Show personal info", ShowPersonalInfo),
            new CommandInfo("Exit from account.", AccountManager.LogOut)
        };

        public RegisteredUser(int id, string name, string surname, int age, string password, string email, RoleTypes roleType)
           : base(id, name, surname, age, password, email, roleType)
        {}


        public override void ShowMenu()
        {
            RunUserMenu(userCommandsArray);
        }

        public static void ChangeMyPersonalData() 
        {
            int choice = GetTheChoice();
            Dictionary<int, Action> commandsToChange = new Dictionary<int, Action>(){
            { 1, ChangeUserName },
            { 2, ChangeUserSurname },
            { 3, ChangeUserAge },
            { 4, ChangeUserPassword },
            { 5, ChangeUserEmail }
            };
            commandsToChange[choice].Invoke();
        }

        public static int GetTheChoice() 
        {
            ShowChoicesToChange();
            int choice = 0;
            while (choice < 1 || choice > 5) 
            {
                choice = ConsoleManager.ReadInt();
                if (choice > 0 && choice < 6)
                {
                    return choice;
                }
                else
                {
                    Console.Clear();
                    ShowChoicesToChange();
                    Console.WriteLine("Please write number between 1 and 5");
                }
            }
            return 0;
        }

        public static void ShowChoicesToChange() 
        {
            Console.WriteLine("Please choose what exactly you want to change:");
            Console.WriteLine("1. Edit user name");
            Console.WriteLine("2. Edit user surname");
            Console.WriteLine("3. Edit user age");
            Console.WriteLine("4. Edit user's password");
            Console.WriteLine("5. Edit user's email");
        }

        private static void RunUserMenu(CommandInfo[] commandsArray)
        {
            int choice = -1;
            while (choice  != 9)
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

        public static void ShowPersonalInfo()
        {
            Console.WriteLine("My personal info: ");
            Console.WriteLine("ID: {0}", AccountManager.CurrentUser.ID);
            Console.WriteLine("Name: {0}", AccountManager.CurrentUser.Name);
            Console.WriteLine("Surname: {0}", AccountManager.CurrentUser.Surname);
            Console.WriteLine("Age: {0}", AccountManager.CurrentUser.Age);
            Console.WriteLine("Balance: {0}", AccountManager.CurrentUser.Balance);
            Console.WriteLine("Password: {0}", AccountManager.CurrentUser.Password);
            Console.WriteLine("E-mail: {0}", AccountManager.CurrentUser.Email);
            Console.WriteLine("Role: {0}", AccountManager.CurrentUser.Role);
            Console.WriteLine("Please, type enter to continue...)");
            Console.ReadKey();
        }

        public static void ChangeBalance()
        {
            Console.WriteLine("Please enter the sum of money you want to add:");
            double sum = ConsoleManager.ReadDouble();
            AccountManager.CurrentUser.Balance += sum;
        }

        public static void ChangeUserName()
        {
            Console.Clear();
            Console.WriteLine("Please, enter your new name: ");
            AccountManager.CurrentUser.Name = ConsoleManager.ReadString();
            Console.WriteLine("The name of user is changed to {0}", AccountManager.CurrentUser.Name);
        }

        public static void ChangeUserSurname()
        {
            Console.Clear();
            Console.WriteLine("Please, enter your new surname: ");
            AccountManager.CurrentUser.Surname = ConsoleManager.ReadString();
            Console.WriteLine("The surname of user is changed to {0}", AccountManager.CurrentUser.Surname);
        }

        public static void ChangeUserAge()
        {
            Console.Clear();
            Console.WriteLine("Please, enter your new age: ");
            int age = ConsoleManager.ReadInt();

            while (true)
            {
                if (age > 0)
                {
                    AccountManager.CurrentUser.Age = age;
                    Console.WriteLine("The age of user is changed to {0}", AccountManager.CurrentUser.Age);
                    break;
                }
                else
                {
                    Console.WriteLine("Please write correct age!");
                }
            }
        }

        public static void ChangeUserPassword()
        {
            Console.Clear();
            Console.WriteLine("Please enter your old password: ");
            if (AccountManager.CurrentUser.Password.Equals(ConsoleManager.ReadPassword()))
            {
                string passwordFirst;
                string passwordSecond;
                do
                {
                    Console.WriteLine("Please, enter your new password: ");
                    passwordFirst = ConsoleManager.ReadPassword();
                    Console.WriteLine("Please, enter your new password (2nd time): ");
                    passwordSecond = ConsoleManager.ReadPassword();
                    if (passwordFirst.Equals(passwordSecond))
                    {
                        AccountManager.CurrentUser.Password = passwordFirst;
                        Console.WriteLine("Password is changed.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Passwords are not equal, try one more time!");
                    }
                }
                while (true);
            }
        }

        public static void ChangeUserEmail()
        {
            Console.Clear();
            Console.WriteLine("Please, enter your new email: ");
            AccountManager.CurrentUser.Email = ConsoleManager.ReadString();
            Console.WriteLine("The email of user is changed to {0}", AccountManager.CurrentUser.Email);
        }

        private static void ShowCommandsMenu(CommandInfo[] commandsArray)
        {
            Console.WriteLine("***  ****  *****  Welcome to user's menu.  *****  ****  ***:\n");
            for (int i = 0; i < commandsArray.Length; i++)
            {
                Console.WriteLine("\t\t{0} - {1}", i, commandsArray[i].name);
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
