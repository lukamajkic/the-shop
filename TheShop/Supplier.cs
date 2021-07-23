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
        private readonly int _articleId;
        public Supplier(int supplierNumber, int articlePrice, int articleId)
        {
            _supplierNumber = supplierNumber;
            _articlePrice = articlePrice;
            _articleId = articleId;
        }

        public Article GetArticle(int id)
        {
            if (_articleId == id)
            {
                return new Article()
                {
                    ID = _articleId,
                    Name_of_article = "Article from supplier" + _supplierNumber,
                    ArticlePrice = _articlePrice
                };
            }
            return null;
        }
    }
}
