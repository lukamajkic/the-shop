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

        public ShopService(DatabaseDriver databaseDriver, Logger logger)
        {
            _databaseDriver = databaseDriver;
            _logger = logger;
            _supplier1 = new Supplier(1, 458, 1);
            _supplier2 = new Supplier(1, 459, 1);
            _supplier3 = new Supplier(1, 460, 1);
        }

        public Article OrderArticle(int id, int maxExpectedPrice)
        {
            var article = GetArticleIfAppropriate(_supplier1, id, maxExpectedPrice);
            if (article != null)
                return article;
            article = GetArticleIfAppropriate(_supplier2, id, maxExpectedPrice);
            if (article != null)
                return article;
            article = GetArticleIfAppropriate(_supplier3, id, maxExpectedPrice);
            return article;
        }

        public void SellArticle(int buyerId, Article article)
        {
            if (article is null)
            {
                throw new Exception("Could not order article");
            }

            _logger.Debug("Trying to sell article with id=" + article.ID);

            SetSaleInformation(buyerId, article);

            try
            {
                _databaseDriver.Save(article);
                _logger.Info("Article with id=" + article.ID + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                _logger.Error("Could not save article with id=" + article.ID);
                throw new Exception("Could not save article with id");
            }
            catch (Exception)
            {
            }
        }

        private Article GetArticleIfAppropriate(Supplier supplier, int id, int maxExpectedPrice)
        {
            var articleExists = supplier.ArticleInInventory(id);
            if (!articleExists)
                return null;

            var tempArticle = supplier.GetArticle(id);
            return tempArticle.ArticlePrice <= maxExpectedPrice ? tempArticle : null;
        }

        private void SetSaleInformation(int buyerId, Article article)
        {
            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;
        }

        public Article GetById(int id)
        {
            return _databaseDriver.GetById(id);
        }
    }

}
