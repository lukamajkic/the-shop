using System;
using System.Collections.Generic;
using System.Linq;

namespace TheShop
{
	public class ShopService
	{
		private DatabaseDriver DatabaseDriver;
		private Logger logger;

		private Supplier Supplier1;
		private Supplier Supplier2;
		private Supplier Supplier3;
		
		public ShopService()
		{
			DatabaseDriver = new DatabaseDriver();
			logger = new Logger();
			Supplier1 = new Supplier(1,458);
			Supplier2 = new Supplier(1,459);
			Supplier3 = new Supplier(1,460);
		}

		public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
		{
			#region ordering article

			Article article = null;
			Article tempArticle = null;
			var articleExists = Supplier1.ArticleInInventory(id);
			if (articleExists)
			{
				tempArticle = Supplier1.GetArticle(id);
				if (maxExpectedPrice < tempArticle.ArticlePrice)
				{
					articleExists = Supplier2.ArticleInInventory(id);
					if (articleExists)
					{
						tempArticle = Supplier2.GetArticle(id);
						if (maxExpectedPrice < tempArticle.ArticlePrice)
						{
							articleExists = Supplier3.ArticleInInventory(id);
							if (articleExists)
							{
								tempArticle = Supplier3.GetArticle(id);
								if (maxExpectedPrice < tempArticle.ArticlePrice)
								{
									article = tempArticle;
								}
							}
						}
					}
				}
			}
			
			article = tempArticle;
			#endregion

			#region selling article

			if (article == null)
			{
				throw new Exception("Could not order article");
			}

			logger.Debug("Trying to sell article with id=" + id);

			article.IsSold = true;
			article.SoldDate = DateTime.Now;
			article.BuyerUserId = buyerId;
			
			try
			{
				DatabaseDriver.Save(article);
				logger.Info("Article with id=" + id + " is sold.");
			}
			catch (ArgumentNullException ex)
			{
				logger.Error("Could not save article with id=" + id);
				throw new Exception("Could not save article with id");
			}
			catch (Exception)
			{
			}

			#endregion
		}

		public Article GetById(int id)
		{
			return DatabaseDriver.GetById(id);
		}
	}

}
