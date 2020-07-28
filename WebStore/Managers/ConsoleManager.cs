using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Entities;
using WebStore.Managers;

namespace WebStore
{
    static class ConsoleManager
    {
        public static void ShowMainMenu() 
        {
            Console.WriteLine("***  ****  *****  Welcome to our main menu.  *****  ****  ***\n");
            Console.WriteLine("\t\t  1. Show all comodities");
            Console.WriteLine("\t\t  2. Find comodity by the name");
            Console.WriteLine("\t\t  3. Create account");
            Console.WriteLine("\t\t  4. Enter the store with account");
            Console.WriteLine("\t\t  5. Switch to light design");
        }

        internal static (string email, string password) TakeUserDataToEnter() 
        {
            (string email, string password) UserDataTuple;
            Console.WriteLine("Enter your email: ");
            UserDataTuple.email = ConsoleManager.ReadString();
            Console.WriteLine("Enter your password: ");
            UserDataTuple.password = ReadPassword();
            return UserDataTuple;
        }

        internal static (string, string, int, string, string) TakeDataAboutAccount()
        {
            (string name, string surname, int age, string password, string email) accountTuple;
            Console.WriteLine("Enter your name: ");
            accountTuple.name = ReadString();
            
            Console.WriteLine("Enter your surname: ");
            accountTuple.surname = ReadString();

            Console.WriteLine("Enter your age: ");            
            accountTuple.age = ReadInt();

            Console.WriteLine("Enter your password: ");
            accountTuple.password = ReadPassword();

            Console.WriteLine("Enter your email: ");
            accountTuple.email = ReadString();
            
            return accountTuple;
        }

        public static int ReadInt()
        {
            int number;
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Please, write correct number!");
                return ReadInt();
            }
            return number;
        }

        public static double ReadDouble()
        {
            double number;
            if (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Please, write correct number!");
                return ReadDouble();
            }
            return number;
        }


        public static string ReadString() 
        {
            string str = Console.ReadLine();
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("Please, fill the line!");
                return ReadString();
            }
            return str;
        }


        internal static (string name, double price, DateTime timeWarrantyExpiration, CommodityTypes type) TakeDataAboutCommodity()
        {
            (string name, double price, DateTime timeWarrantyExpiration, CommodityTypes type) commodityTuple;
            Console.WriteLine("Enter commodity's name: ");
            commodityTuple.name = Console.ReadLine();

            Console.WriteLine("Enter commodity's price: ");
            commodityTuple.price = Double.Parse(Console.ReadLine());

            Console.WriteLine("Enter a date of waaranty expiration (e.g. 10/22/2020): ");
            commodityTuple.timeWarrantyExpiration = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Choose kind of commodity: ");
            commodityTuple.type = GetTheType();

            return commodityTuple;
        }

        public static CommodityTypes GetTheType() 
        {
            Console.WriteLine("1. Computer;");
            Console.WriteLine("2. Monitor;");
            Console.WriteLine("3. Camera;");
            Console.WriteLine("4. Mouse;");
            Console.WriteLine("5. Keyboard;");

            int choice = Convert.ToInt32(Console.ReadLine()) - 1;

            if (choice < 1 || choice > 5)
            {
                throw new ArgumentException("Please enter correct value.");
            }
            Console.WriteLine();
            return (CommodityTypes)Enum.GetValues(typeof(CommodityTypes)).GetValue(choice);
        }

        public static string ReadPassword() 
        {
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                    break;
                else
                {
                    Console.Write("*");
                    password.Append(cki.KeyChar.ToString());
                }
            }

            if (string.IsNullOrEmpty(password.ToString()))
            {
                Console.WriteLine("Please, type password, empty line is not allowed!");
                return ReadPassword();
            }

            Console.WriteLine();
            return password.ToString();
        }


        internal static void ShowTheItems(List<Commodity> commodities) 
        {
            foreach (var item in commodities)
            {
                Console.WriteLine("ID: {0}, Name: {1}, Type: {2}, Price: {3}", item.ID, item.Name, item.Type.ToString(), item.Price);
            }
            Console.WriteLine("Please, type enter to continue...)");
        }

        public static void ShowOrdersOfUser(List<Order> ordersList) 
        {
            if (ordersList.Count != 0)
            {
                foreach (var item in ordersList)
                {
                    Console.WriteLine("ID: {0}, name: {1}, type: {2}, date of ordering: {3}\n Status of order {4}; \n\n",
                           item.Commodity.ID, item.Commodity.Name, item.Commodity.Type.ToString(), item.Date.ToString(), item.Status.ToString());
                }
            }
        }

        public static void SwitchToLightDesign()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
        }

        public static void SwitchToDarkDesign()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
        }

        public static int GetID(string typeOfEntity)
        {
            Console.WriteLine("Please type the Id of your {0}: ", typeOfEntity);
            return ConsoleManager.ReadInt();
        }

        public static void Run()
        {
            //AccountManager accMng = new AccountManager();
            while (true)
            {
                Console.Clear();
                if (AccountManager.CurrentUser != null)
                {
                    AccountManager.CurrentUser.ShowMenu();
                }
                else
                {
                    ShowCommandsMenu();

                    Command command;
                    try
                    {
                        command = EnterCommand();
                        if (command == null)
                        {
                            Environment.Exit(0);
                        }
                        command();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
            }
        }

        static CommandInfo[] commandInfoArray = {
            new CommandInfo("Exit", null),
            new CommandInfo("Show all comodities", CommodityManager.ShowCommodities),
            new CommandInfo("Find comodity by the name", CommodityManager.FindCommodityByName),
            new CommandInfo("Create account", AccountManager.CreateAccount),
            new CommandInfo("Enter the store with account",AccountManager.EnterTheAccount),
            new CommandInfo("Switch to light design", ConsoleManager.SwitchToLightDesign),
            new CommandInfo("Switch to dark design", ConsoleManager.SwitchToDarkDesign)
        };

        private static void ShowCommandsMenu()
        {
            Console.WriteLine("***  ****  *****  Welcome to our main menu.  *****  ****  ***:\n");
            for (int i = 0; i < commandInfoArray.Length; i++)
            {
                Console.WriteLine("\t\t{0} - {1}", i, commandInfoArray[i].name);
            }
        }

        private static Command EnterCommand()
        {
            Console.Write("\nEnter the number of command:\n");
            int number = ReadInt();
            if (number < commandInfoArray.Length && number >= 0)
            {
                return commandInfoArray[number].command; 
            }
            else if (number < 0)
            {
                throw new ArgumentException("Number cannot be less than zero.");
            }
            else
            {
                string exceptionStr = String.Format("Number must be less than {0}.", commandInfoArray.Length);
                throw new ArgumentException(exceptionStr);
            }
        }


        public static void ShowAllStatusesForAdmin()
        {
            Console.WriteLine("Your statuses: \n{0}, \n{1}, \n{2}, \n{3}, \n{4}, \n{5}, \n{6}. ",
             Statuses.New,
             Statuses.CanceledByAdmin,
             Statuses.Dispatched,
             Statuses.Received,
             Statuses.PaymentIsReceived,
             Statuses.Finished);
        }
    }
}
