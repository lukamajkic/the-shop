using System;

namespace TheShop
{
    internal class Program
    {
        static int supplier1Number = 1;
        static int supplier2Number = 1;
        static int supplier3Number = 1;
        static int supplier1ArticlePrice = 458;
        static int supplier2ArticlePrice = 459;
        static int supplier3ArticlePrice = 460;
        static int supplier1ArticleId = 1;
        static int supplier2ArticleId = 1;
        static int supplier3ArticleId = 1;
        static int firstOrderId = 1;
        static int firstOrderMaxPrice = 459;
        static int firstGetArticleId = 1;
        static int secondGetArticleId = 12;
        static int buyerId = 20;
        static DatabaseDriver databaseDriver = new DatabaseDriver();
        static Logger logger = new Logger();

        static Supplier supplier1 = new Supplier(supplier1Number, supplier1ArticlePrice, supplier1ArticleId);
        static Supplier supplier2 = new Supplier(supplier2Number, supplier2ArticlePrice, supplier2ArticleId);
        static Supplier supplier3 = new Supplier(supplier3Number, supplier3ArticlePrice, supplier3ArticleId);
        static ShopService shopService = new ShopService(databaseDriver, logger, supplier1, supplier2, supplier3);
        private static void Main(string[] args)
        {
            try
            {
                //order and sell
                var article = shopService.OrderArticle(firstOrderId, firstOrderMaxPrice);
                shopService.SellArticle(buyerId, article);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            PrintArticle(firstGetArticleId);
            PrintArticle(secondGetArticleId);

            Console.ReadKey();
        }

        private static void PrintArticle(int articleId)
        {
            try
            {
                //print article on console
                var article = shopService.GetById(articleId);
                Console.WriteLine("Found article with ID: " + article.ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find article with id: {articleId}. Exception: " + ex);
            }
        }
    }
}