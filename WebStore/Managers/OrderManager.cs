using System;
using System.Collections.Generic;
using WebStore.Entities;

namespace WebStore.Managers
{
    public class OrderManager
    {
        public static int Count { get; private set; } = 0;

        public static void CreateOrder()
        {
            Count++;
            CommodityManager.ShowCommodities();
            var currentUser = AccountManager.CurrentUser as RegisteredUser;
            if (currentUser != null)
            {
                ((RegisteredUser)AccountManager.CurrentUser).orders.Add(new Order(Count, AccountManager.CurrentUser, CommodityManager.FindCommodityByID(ConsoleManager.GetID("commodity")), Statuses.New));
            }
            else
            {
                ((Administrator)AccountManager.CurrentUser).orders.Add(new Order(Count, AccountManager.CurrentUser, CommodityManager.FindCommodityByID(ConsoleManager.GetID("commodity")), Statuses.New));
            }
            Console.WriteLine("New order is created.");
            Console.WriteLine("Please, type enter to continue...)");
            Console.ReadKey();
        }
        public static void ShowAllOrders()
        {
            Console.WriteLine("History and status of orders: ");
            ConsoleManager.ShowOrdersOfUser(GetAllOrdersOfUser());
            Console.WriteLine("Please, type enter to continue...)");
            Console.ReadKey();
        }

        public static void ChangeTheStatus() 
        {
            Console.WriteLine("Enter ID of user:");
            int choiceOfUser = ConsoleManager.ReadInt();
            if (AccountManager.FindUserById(choiceOfUser) != null)
            {
                Console.WriteLine("Enter ID of order:");
                int idOfOrder = ConsoleManager.ReadInt();
                if (FindOrderByID(idOfOrder, AccountManager.FindUserById(choiceOfUser)) != null)
                {
                    FindOrderByID(idOfOrder, AccountManager.FindUserById(choiceOfUser)).Status = ChoosenStatus();
                }
                else
                {
                    Console.WriteLine("There is no such order!");
                    Console.WriteLine("Please, type the enter to continue...)");
                }
            }
            else
            {
                Console.WriteLine("There is no such user, \nplease, create it first!");
            }
        }


        public static Statuses ChoosenStatus()
        {
            ConsoleManager.ShowAllStatusesForAdmin();
            Console.WriteLine("Please choose the status:");
            int choiceOfUser = ConsoleManager.ReadInt();
            switch (choiceOfUser)
            {
                case 1: return Statuses.New;
                    break;
                case 2: return Statuses.PaymentIsReceived;
                    break;
                case 3: return Statuses.Received;
                    break;
                case 4: return Statuses.Dispatched;
                    break;
                case 5: return Statuses.Finished;
                    break;
                case 6: return Statuses.CanceledByAdmin;
                    break;
                default:
                    Console.WriteLine("Please, enter number between 1 and 6");
                    break; 
            }
            return Statuses.New;
        }


        public static void MakeTheStatusReceived() 
        {
            Console.Clear();
            OrderManager.ShowAllOrders();
            Order order  = FindOrderByID(ConsoleManager.GetID("order"), AccountManager.CurrentUser);
            if (order == null)
            {
                Console.WriteLine("You don't have orders, please add them first.");
                Console.ReadKey();
                return;
            }
            order.Status = Statuses.Received;
            Console.WriteLine("The status of order with ID #{0}, is changed to \"Received\"", order.ID);
            Console.WriteLine("Press any key to go back.");
            Console.ReadKey();
        }

        public static Order FindOrderByID(int Id, Account currentUser) 
        {
            foreach (var item in ((RegisteredUser)currentUser).orders)
            {
                if (item.ID.Equals(Id))
                {
                    return item;
                }
            }
            Console.WriteLine("There is no order with such ID.");
            return null;
        }

        public static void OrderOrCancelOrder() 
        {
            Console.WriteLine("Do you approve your orders?");
            Console.WriteLine("All your orders: ");
            ConsoleManager.ShowOrdersOfUser(GetAllNewOrders());
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Checkout();
                    break;
                case "2":
                    MakeAllNewOrdersCanceled();
                    break;
                default:
                    Console.WriteLine("Please enter 1 or 2.");
                    break;
            }
        }

        public static void Checkout() 
        {
            double sumToMinus = 0;
            foreach (var item in GetAllNewOrders())
            {
                sumToMinus += item.Commodity.Price;
            }

            if (sumToMinus <= AccountManager.CurrentUser.Balance)
            {
                AccountManager.CurrentUser.Balance -= sumToMinus;
                Console.WriteLine("Your commodity is ordered and now we are sending it to you!");
                Console.WriteLine("Please, type enter to continue...)");
            }
            else
            {
                Console.WriteLine("You don't have enough of money " +
                    "\nPlease, take less commodity or add some money " +
                    "\nIn order to do it, please, choose #7 of user's menu");
                Console.WriteLine("Please, type enter to continue...)");
            }
            Console.ReadKey();
        }

        public static void MakeAllNewOrdersCanceled() 
        {
            foreach (var item in ((RegisteredUser)AccountManager.CurrentUser).orders)
            {
                if (AccountManager.CurrentUser.ID == item.Account.ID && item.Status == Statuses.New)
                {
                    if (AccountManager.CurrentUser is RegisteredUser)
                    {
                        item.Status = Statuses.CanceledByUser;
                    }
                    else if (AccountManager.CurrentUser is Administrator)
                    {
                        item.Status = Statuses.CanceledByAdmin;
                    }
                }
            }
            Console.WriteLine("Please, type enter to continue...)");
        }

        public static List<Order> GetAllNewOrders()
        {
            List<Order> ordersList = new List<Order>();
            foreach (var item in ((RegisteredUser)AccountManager.CurrentUser).orders)
            {
                if (AccountManager.CurrentUser.ID == item.Account.ID && item.Status == Statuses.New)
                {
                    ordersList.Add(item);
                }
            }
            return ordersList;
        }

        public static List<Order> GetAllOrdersOfUser() 
        {
            List<Order> ordersList = new List<Order>();
            if (((RegisteredUser)AccountManager.CurrentUser).orders.Count != 0)
            {
                foreach (var item in ((RegisteredUser)AccountManager.CurrentUser).orders)
                {
                    if (AccountManager.CurrentUser.ID == item.Account.ID)
                    {
                        ordersList.Add(item);
                    }
                }
            }
            else
            {
                Console.WriteLine("There no orders ((");
            }
            return ordersList;
        }
    }
}
