using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Logic.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
