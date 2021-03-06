using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShop
{
    public class Article
    {
        public int ID { get; set; }

        public string Name_of_article { get; set; }

        public int ArticlePrice { get; set; }
        public bool IsSold { get; set; }

        public DateTime SoldDate { get; set; }
        public int BuyerUserId { get; set; }
        public void Sell(int buyerId)
        {
            SoldDate = DateTime.Now;
            IsSold = true;
            BuyerUserId = buyerId;
        }
    }
}
