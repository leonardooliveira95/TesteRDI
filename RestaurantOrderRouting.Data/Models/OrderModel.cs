using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Data.Models
{
    public class OrderModel : AbstractModel
    {
        public decimal TotalValue { get; set; }
        public DateTime OrderDate { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
}
