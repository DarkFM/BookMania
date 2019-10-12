using BookMania.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Entities.OrderAggregate
{
    /// <summary>
    /// Represents an order item and the order it is associated with
    /// </summary>
    public class OrderItem //ValueObject
    {
        private OrderItem()
        {
        }

        public OrderItem(int quantity, decimal price, BookItemSnapshot bookOrdered)
        {
            bookOrdered.ThrowIfNull($"{nameof(bookOrdered)} cannot be null");

            Quantity = quantity;
            Price = price;
            BookOrdered = bookOrdered;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BookItemSnapshot BookOrdered { get; set; }
    }
}
