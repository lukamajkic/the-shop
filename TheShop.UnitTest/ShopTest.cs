using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheShop.UnitTest
{
    [TestClass]
    public class ShopTest
    {
        ShopService shopService;
        List<Supplier> suppliers = new List<Supplier>();

        [TestMethod]
        public void OrderSuccessfulTest()
        {
            InitializeConfiguration();
            int orderId = 1;
            int orderMaxPrice = 500;
            var article = shopService.OrderArticle(orderId, orderMaxPrice);
            Assert.IsNotNull(article,"Should not be null");
        }

         [TestMethod]
        public void OrderFailedTest()
        {
            InitializeConfiguration();
            int orderId = 1;
            int orderMaxPrice = 400;
            var article = shopService.OrderArticle(orderId, orderMaxPrice);
            Assert.IsNull(article,"Should be null");
        }

        [TestMethod]
        public void SellTest()
        {
            InitializeConfiguration();
            var article = new Article { ID = 1, ArticlePrice = 10 };
            int buyerId = 10;
            shopService.SellArticle(buyerId, article);
            var resultArticle = shopService.GetById(article.ID);
            Assert.IsTrue(resultArticle.IsSold, "Should be true");
        }

        private void InitializeConfiguration()
        {
            int supplier1Number = 1;
            int supplier2Number = 1;
            int supplier3Number = 1;
            int supplier1ArticlePrice = 458;
            int supplier2ArticlePrice = 459;
            int supplier3ArticlePrice = 460;
            int supplier1ArticleId = 1;
            int supplier2ArticleId = 1;
            int supplier3ArticleId = 1;

            var supplier1 = new Supplier(supplier1Number, supplier1ArticlePrice, supplier1ArticleId);
            var supplier2 = new Supplier(supplier2Number, supplier2ArticlePrice, supplier2ArticleId);
            var supplier3 = new Supplier(supplier3Number, supplier3ArticlePrice, supplier3ArticleId);
            suppliers.Add(supplier1);
            suppliers.Add(supplier2);
            suppliers.Add(supplier3);
            var databaseDriver = new DatabaseDriver();
            var logger = new Logger();

            shopService = new ShopService(databaseDriver, logger, supplier1, supplier2, supplier3);
        }
    }
}
