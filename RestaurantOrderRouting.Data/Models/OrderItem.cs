using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Data.Models
{
    public class OrderItem : AbstractModel
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
