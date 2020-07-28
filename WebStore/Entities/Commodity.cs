using System;

namespace WebStore.Entities
{
    public class Commodity : IHaveID
    {
        double price;
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime TimeOfExpiration { get; set; }
        public CommodityTypes Type { get; set; }
        public double Price 
        {
            get { return price; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("The price cannot be less than 0.");
                }
                else
                {
                    price = value;
                }
            } 
        }

        public Commodity(int id, string name, double price, DateTime timeOfExpiration, CommodityTypes type)
        {
            ID = id;
            Name = name;
            Price = price;
            TimeOfExpiration = timeOfExpiration;
            Type = type;
        }
    }

    public enum CommodityTypes 
    {
        Computer,
        Monitor,
        Camera,
        Mouse,
        Keyboard,
    }
}
