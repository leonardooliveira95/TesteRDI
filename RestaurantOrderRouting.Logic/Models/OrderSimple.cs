using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Logic.Models
{
    public class OrderSimple
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalValue { get; set; }
        public string Items { get; set; }
        public int ItemsCount { get; set; }
    }
}
