using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore.Entities;
using WebStore.Managers;

namespace WebStore.Tests
{
    [TestClass]
    public class ManagersTest
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
            commodity = new Commodity(1, "HP", 250.0, DateTime.MaxValue, CommodityTypes.Computer);
            order = new Order(1, user, commodity, Statuses.New);
           
            AccountManager.accounts.Add(user);
            CommodityManager.commodities.Add(commodity);
        }

        
        [TestMethod]
        public void FindCommodityByID_Test()
        {
            var expected = commodity;
            var actual = CommodityManager.FindCommodityByID(1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindCommodityByName_Test()
        {
            var expected = commodity;
            var actual = CommodityManager.FindCommodityByName("HP");

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void FindUserByEmail_Test()
        {
            var expected = user;
            var actual = AccountManager.FindUserByEmail("anton.pylypenko@gmail.com");

            Assert.AreEqual(expected, actual);
        }
    }
}
