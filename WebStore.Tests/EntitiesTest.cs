using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Entities;

namespace WebStore.Tests
{
    [TestClass]
    public class EntitiesTest
    {
        RegisteredUser user;
        Administrator admin;
        Commodity commodity;
        Order order;

        [TestInitialize]
        public void Setup()
        {
            user = new RegisteredUser(1, "Anton", "Pylypenko", 22, "adubih", "anton.pylypenko@gmail.com", RoleTypes.RegisteredUser);
            admin = new Administrator(1, "Anton", "Pylypenko", 22, "adubih", "anton.pylypenko@gmail.com", RoleTypes.Administrator);
            commodity = new Commodity(1, "HP", 250.0, DateTime.Today, CommodityTypes.Computer);
            order = new Order(1, user, commodity, Statuses.New);
        }
     
        [TestMethod]
        public void Administrator_Constructor_Test()
        {
            var expectedTuple = (1, "Anton", "Pylypenko", 22, "adubih", "anton.pylypenko@gmail.com", RoleTypes.Administrator);
            var actualTuple = (admin.ID, admin.Name, admin.Surname, admin.Age, admin.Password, admin.Email, admin.Role);

            Assert.AreEqual(expectedTuple, actualTuple);
        }

        [TestMethod]
        public void RegisteredUser_Constructor_Test()
        {
            var expectedTuple = (1, "Anton", "Pylypenko", 22, "adubih", "anton.pylypenko@gmail.com", RoleTypes.RegisteredUser);
            var actualTuple = (user.ID, user.Name, user.Surname, user.Age, user.Password, user.Email, user.Role);

            Assert.AreEqual(expectedTuple, actualTuple);
        }

        [TestMethod]
        public void Commodity_Constructor_Test()
        {
            var expectedTuple = (1, "HP", 250.0, DateTime.Today, CommodityTypes.Computer);
            var actualTuple = (commodity.ID, commodity.Name, commodity.Price, commodity.TimeOfExpiration, commodity.Type);

            Assert.AreEqual(expectedTuple, actualTuple);
        }

        [TestMethod]
        public void Order_Constructor_Test()
        {
            var expectedTuple = (1, user, commodity, Statuses.New);
            var actualTuple = (order.ID, user, commodity, order.Status);

            Assert.AreEqual(expectedTuple, actualTuple);
        }
    }
}
