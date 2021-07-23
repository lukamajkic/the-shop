using System;
using System.Collections.Generic;
using System.Linq;

namespace TheShop
{
    public class ShopService
    {
        private DatabaseDriver _databaseDriver;
        private Logger _logger;

        private Supplier _supplier1;
        private Supplier _supplier2;
        private Supplier _supplier3;

        public ShopService(DatabaseDriver databaseDriver, Logger logger, Supplier supplier1, Supplier supplier2, Supplier supplier3)
        {
            _databaseDriver = databaseDriver;
            _logger = logger;
            _supplier1 = supplier1;
            _supplier2 = supplier2;
            _supplier3 = supplier3;
        }

        public Article OrderArticle(int id, int maxExpectedPrice)
        {
            var suppliers = new List<Supplier>{ _supplier1, _supplier2, _supplier3 };         
            foreach(var supplier in suppliers)
            {
                var article = GetArticleIfAppropriate(supplier, id, maxExpectedPrice);
                if (article != null)
                    return article;
            }
            return null;
        }

        public void SellArticle(int buyerId, Article article)
        {
            if (article is null)
            {
                throw new Exception("Could not order article");
            }
            _logger.Debug("Trying to sell article with id=" + article.ID);
            article.Sell(buyerId);
            _databaseDriver.Save(article);
            _logger.Info("Article with id=" + article.ID + " is sold.");
        }

        private Article GetArticleIfAppropriate(Supplier supplier, int id, int maxExpectedPrice)
        {
            var tempArticle = supplier.GetArticle(id);
            if (tempArticle is null)
                return null;
            return tempArticle.ArticlePrice <= maxExpectedPrice ? tempArticle : null;
        }

        public Article GetById(int id)
        {
            return _databaseDriver.GetById(id);
        }
    }
}
