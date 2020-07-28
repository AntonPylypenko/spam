using System;
using System.Runtime.Serialization;

namespace WebStore.Entities
{
    public class Order : IHaveID
    {
        public int ID { get; set ; }
        public Account Account { get; set; }
        public Commodity Commodity { get; set; }
        public Statuses Status { get; set; }
        public DateTime Date { get; set; }

        public Order(int id, Account account, Commodity commodity, Statuses status) 
        {
            ID = id;
            Account = account;
            Commodity = commodity;
            Status = status;
            Date = DateTime.Now;
        }
    }

    public enum Statuses
    {
        New,
        CanceledByAdmin,
        CanceledByUser,
        Dispatched,
        Received,
        PaymentIsReceived,
        Finished
    }
}
