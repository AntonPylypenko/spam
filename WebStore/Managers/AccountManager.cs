using System;
using System.Collections.Generic;
using WebStore.Entities;

namespace WebStore.Managers
{
    
    public class AccountManager
    {
        private static int CountID = 0;

        public static Account CurrentUser { get; set; } = null;

        public static List<Account> accounts = new List<Account>();

        public static void CreateAdmin() 
        {
            accounts.Add(new Administrator(CountID, "Anton", "Pylypenko", 22, "admin", "admin", RoleTypes.Administrator));
        }

        public static void CreateAccount()
        {
            (string name, string surname, int age, string password, string email) = ConsoleManager.TakeDataAboutAccount();
            if (AccountManager.FindUserByEmail(email) != null)
            {
                Console.WriteLine("We have account with such email, \nplease register user with another email!");
                CreateAccount();
            }
            CountID++;
            accounts.Add(new RegisteredUser(CountID, name, surname, age, password, email, RoleTypes.RegisteredUser));
            EnterTheAccount();
        }

        public static RegisteredUser FindUserById(int id) 
        {
            foreach (var user in accounts)
            {
                if (user.ID == id && user.Role.Equals(RoleTypes.RegisteredUser))
                {
                    return (RegisteredUser)user;
                }
            }
            Console.WriteLine("No user with such ID!");
            Console.WriteLine("Please, type the enter to continue...)");
            Console.ReadKey();
            return null;
        }


        public static void EnterTheAccount()
        {
            Console.Clear();
            (string email, string password) = ConsoleManager.TakeUserDataToEnter();
            Account user = FindUserByEmail(email);
            if (user!= null && user.Password.Equals(password))  
            {
                Console.Clear();
                ShowAccountInfo(email);
                Console.WriteLine();
                if (user.Role == RoleTypes.Administrator)
                {
                    CurrentUser = user as Administrator;
                } 
                else
                {
                    CurrentUser = user as RegisteredUser;
                }
                CurrentUser.ShowMenu();
            }
            else
            {
                Console.WriteLine("Please, press enter and try one more time!");
                Console.ReadKey();
                EnterTheAccount();
            }
        }

        public static void LogOut() 
        {
            CurrentUser = null;
        }

        public static void ShowAccountInfo(string email) 
        {
            Account account = FindUserByEmail(email);
            Console.WriteLine("Your ID: {0}", account.ID);
            Console.WriteLine("Your name: {0}", account.Name);
            Console.WriteLine("Your surname: {0}", account.Surname);
            Console.WriteLine("Your age: {0}", account.Age);
            Console.WriteLine("Your email: {0}", account.Email);
            Console.WriteLine("Your balance: {0}", account.Balance);
        }

        public static Account FindUserByEmail(string email)
        {
            foreach (var item in accounts)
            {
                if (item.Email.Equals(email))
                {
                    return item;
                }
            }
            Console.WriteLine("No such account");
            //throw new ArgumentException("There is no account with such email.");
            return null;
        }
        
    }
}
