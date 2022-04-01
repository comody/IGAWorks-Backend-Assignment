using System;
using dfinery.backend.assignment.Models;

namespace dfinery.backend.assignment.bot.Models
{
    public class Product
    {
        public string product_id { get; set; }
        public string product_name { get; set; }
        public int stock { get; set; }
        public int price { get; set; }

        public Product(string product_name, int stock, int price)
        {
            product_id = Guid.NewGuid().ToString();
            this.product_name = product_name;
            this.stock = stock;
            this.price = price;
        }

        public void Purchase(int count)
        {
            if (count > stock)
            {
                throw new OutOfStockException(EVENT_TYPE.OUT_OF_STOCK.ToString());
            }

            return;
        }
    }
}
