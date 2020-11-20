using RestaurantOrderRouting.Data;
using RestaurantOrderRouting.Data.Models;
using RestaurantOrderRouting.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RestaurantOrderRouting.Common;

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

            IEnumerable<OrderModel> ordersInQueue = await queue.List();

            return this.ConvertOrderToOrderSimple(ordersInQueue);
        }

        public async Task<Order> GetNextOrder()
        {
            IQueue<OrderModel> queue = this.GetQueue();

            OrderModel nextOrder = await queue.Dequeue();

            return this.ConvertOrderModelToOrder(nextOrder);
        }

        public async Task<Order> CreateNewOrder(Order order)
        {
            IQueue<OrderModel> queue = this.GetQueue();

            this.ValidateOrder(order);

            OrderModel created = await queue.Enqueue(this.ConvertOrderToOrderModel(order));

            return this.ConvertOrderModelToOrder(created);
        }

        private IQueue<OrderModel> GetQueue()
        {
            IQueueContext queueContext = this._queueContextFactory.CreateNew();
            IQueue<OrderModel> queue = queueContext.GetQueue<OrderModel>();

            return queue;
        }

        private List<OrderSimple> ConvertOrderToOrderSimple(IEnumerable<OrderModel> orders) 
        {
            if (orders == null)
                return null;

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
            if (order == null)
                return null;

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

        private OrderModel ConvertOrderToOrderModel(Order order) 
        {
            if (order == null)
                return null;

            return new OrderModel
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                PaymentType = order.PaymentType,
                TotalValue = order.TotalValue,
                Items = order.Items.Select(item => new OrderItemModel 
                { 
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Value = item.Value
                })
                .ToList()
            };
        }

        private void ValidateOrder(Order order) 
        {
            if (order == null) 
            {
                throw new BusinessException("Order cannot be empty");
            }

            if (order.Items == null || order.Items.Count == 0) 
            {
                throw new BusinessException("The order has no items");
            }

            if (order.TotalValue <= 0) 
            {
                throw new BusinessException("Order value cannot be less or equal to 0");
            }
        }
    }
}
