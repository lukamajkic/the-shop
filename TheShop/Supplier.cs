using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop
{
    public class Supplier
    {
        private readonly int _supplierNumber;
        private readonly int _articlePrice;
        public Supplier(int supplierNumber, int articlePrice)
        {
            _supplierNumber = supplierNumber;
            _articlePrice = articlePrice;
        }
        public bool ArticleInInventory(int id)
        {
            return true;
        }

        public Article GetArticle(int id)
        {
            return new Article()
            {
                ID = 1,
                Name_of_article = "Article from supplier" + _supplierNumber,
                ArticlePrice = _articlePrice
            };
        }
    }
}
