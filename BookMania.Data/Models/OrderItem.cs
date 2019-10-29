namespace BookMania.Data.Models
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
            Quantity = quantity;
            Price = price;
            BookOrdered = bookOrdered;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public BookItemSnapshot BookOrdered { get; set; }
    }
}
