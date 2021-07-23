﻿using System;

namespace TheShop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var databaseDriver = new DatabaseDriver();
            var logger = new Logger();
            var shopService = new ShopService(databaseDriver, logger);

            try
            {
                //order and sell
                var article = shopService.OrderArticle(1, 20);
                shopService.SellArticle(10, article);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            try
            {
                //print article on console
                var article = shopService.GetById(1);
                Console.WriteLine("Found article with ID: " + article.ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Article not found: " + ex);
            }

            try
            {
                //print article on console				
                var article = shopService.GetById(12);
                Console.WriteLine("Found article with ID: " + article.ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Article not found: " + ex);
            }

            Console.ReadKey();
        }
    }
}