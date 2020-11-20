using RestaurantOrderRouting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Logic.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime OrderDate { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
