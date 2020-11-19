using RestaurantOrderRouting.Data;
using RestaurantOrderRouting.Data.Models;
using RestaurantOrderRouting.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RestaurantOrderRouting.Logic.Services.Impl
{
    public class OrderServices : IOrderServices
    {
        private IQueueContextFactory _queueContextFactory;

        public OrderServices(IQueueContextFactory queueContextFactory) 
        {
            this._queueContextFactory = queueContextFactory;
        }

        public async Task<List<OrderSimple>> FetchAllOrders()
        {
            var queueContext = this._queueContextFactory.CreateNew();
            var queue = queueContext.GetQueue<Order>();

            //FOR TESTING PURPOSES ONLY
            int queueCount = await queue.Count();
            if (queueCount == 0) 
            {
                this.SeedQueue(queue);
            }
            
            var ordersInQueue = await queue.List();

            return this.ConvertOrderToOrderSimple(ordersInQueue);
        }

        private List<OrderSimple> ConvertOrderToOrderSimple(IEnumerable<Order> orders) 
        {
            return orders.Select(order => new OrderSimple
            {
                TotalValue = order.TotalValue,
                OrderDate = order.OrderDate,
                ItemsCount = order.Items.Count(),
                Items = string.Join(", ", order.Items.Select(item => item.Name)),
            })
            .ToList();
        }

        //FOR TESTING PURPOSES ONLY
        private void SeedQueue(IQueue<Order> queue)
        {
            for (int i = 0; i < 100; ++i)
            {
                queue.Enqueue(new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    PaymentType = (PaymentTypeEnum)(i % 4),
                    TotalValue = i * 100,
                    Items = new List<OrderItem>() 
                    { 
                        new OrderItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Item " + i,
                            Value = i * 100
                        }
                    }
                });
            }
        }
    }
}
