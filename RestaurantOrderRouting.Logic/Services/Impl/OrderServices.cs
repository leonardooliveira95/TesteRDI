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
            IQueue<OrderModel> queue = this.GetQueue();

            //FOR TESTING PURPOSES ONLY
            int queueCount = await queue.Count();
            if (queueCount == 0)
            {
                this.SeedQueue(queue);
            }

            IEnumerable<OrderModel> ordersInQueue = await queue.List();

            return this.ConvertOrderToOrderSimple(ordersInQueue);
        }

        public async Task<Order> GetNextOrder()
        {
            IQueue<OrderModel> queue = this.GetQueue();

            OrderModel nextOrder = await queue.Dequeue();

            return this.ConvertOrderModelToOrder(nextOrder);
        }

        private IQueue<OrderModel> GetQueue()
        {
            IQueueContext queueContext = this._queueContextFactory.CreateNew();
            IQueue<OrderModel> queue = queueContext.GetQueue<OrderModel>();

            return queue;
        }

        private List<OrderSimple> ConvertOrderToOrderSimple(IEnumerable<OrderModel> orders) 
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

        private Order ConvertOrderModelToOrder(OrderModel order) 
        {
            return new Order
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                PaymentType = order.PaymentType,
                TotalValue = order.TotalValue,
                Items = order.Items.Select(item => new OrderItem 
                { 
                    Id = item.Id,
                    Name = item.Name,
                    Value = item.Value

                }).ToList()
            };
        }

        //FOR TESTING PURPOSES ONLY
        private void SeedQueue(IQueue<OrderModel> queue)
        {
            for (int i = 0; i < 100; ++i)
            {
                queue.Enqueue(new OrderModel
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    PaymentType = (PaymentTypeEnum)(i % 4),
                    TotalValue = i * 100,
                    Items = new List<OrderItemModel>()
                    {
                        new OrderItemModel
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
