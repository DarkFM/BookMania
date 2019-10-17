using BookMania.Core.Entities.BookAggregate;
using BookMania.Core.Entities.UserAggregate;
using BookMania.Core.Extensions;
using BookMania.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookMania.Core.Entities.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// Using a private collection allows for better encapsulation
        /// This allows the aggregate to control access to its internal state
        /// </summary>
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        private Order()
        {
        }

        public Order(int userId, Address shipToAddress, List<OrderItem> orderItems)
        {
            shipToAddress.ThrowIfNull($"{nameof(shipToAddress)} cannot be null");
            orderItems.ThrowIfNullOrEmpty($"{nameof(orderItems)} cannot be null/empty");

            UserId = userId;
            ShipToAddress = shipToAddress;
            _orderItems = orderItems;
        }

        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        // Not mapped
        public decimal TotalPrice => OrderItems.Sum(item => item.Price * item.Quantity);

        public void AddOrderItem(Book book, int quantity, decimal price)
        {
            if (!OrderItems.Any(oi => oi.BookOrdered.BookId == book.Id))
            {
                _orderItems.Add(new OrderItem(
                    quantity,
                    price,
                    new BookItemSnapshot(book.Id, book.Title, book.ImageUrl))
                );
                return;
            }
            var existingItem = OrderItems.First(oi => oi.BookOrdered.BookId == book.Id);
            existingItem.Quantity += quantity;
        }

        public void RemoveEmptyItems()
        {
            _orderItems.RemoveAll(oi => oi.Quantity == 0);
        }
    }
}
