using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Globalization;
using Web.Models;
using WebApplication2.Utils;
using Xunit.Sdk;

namespace UnitTest
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TestReadXML() 
        {
            List<ModelTransaction> transactions = Helpers.GetTransactionsXML();
            Assert.AreNotEqual(transactions.Count, 0);
        }

        [TestMethod]
        public void TestTotalAmount()
        {
            List<ModelTransaction> list = new List<ModelTransaction>();
            list.Add(new ModelTransaction { SKU = "E234", Currency = "EUR", Amount = 242 });
            list.Add(new ModelTransaction { SKU = "E634", Currency = "EUR", Amount = 23 });
            list.Add(new ModelTransaction { SKU = "E734", Currency = "EUR", Amount = 1.34 });
            list.Add(new ModelTransaction { SKU = "E134", Currency = "EUR", Amount = 1.03 });
            double number = 267.37;
            Assert.IsTrue(Helpers.CalculateTotalAmount(list) == number);
        }
    }
}
