
using System;
using System.Collections.Generic;

namespace PEESEE_GAMING.Models
{
    public class StoreOrder
    {
        public string Id { get; set; }

        public string CustomerName { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<StoreOrderItem> Items { get; set; }

        public StoreOrder()
        {
            Items = new List<StoreOrderItem>();
        }
    }

    public class StoreOrderItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal { get; set; }
    }
}
