using WebStore.Entities;

namespace WebStore
{
    public abstract class Account : IHaveID
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public string Email { get; set; }
        public RoleTypes Role { get; private set; }
        public Account() { }

        public Account(int id, string name, string surname, int age, string password, string email, RoleTypes roleType)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Age = age;
            Password = password;
            Email = email;
            Role = roleType;
        }

        public abstract void ShowMenu();
    }

    public enum RoleTypes
    {
         Administrator,
         RegisteredUser
    }
}
