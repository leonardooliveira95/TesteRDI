using RestaurantOrderRouting.Common;
using RestaurantOrderRouting.Data;
using RestaurantOrderRouting.Data.Impl;
using RestaurantOrderRouting.Data.Models;
using RestaurantOrderRouting.Logic.Models;
using RestaurantOrderRouting.Logic.Services;
using RestaurantOrderRouting.Logic.Services.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantOrderRouting.Tests
{
    public class OrderTests
    {
        private IOrderServices _orderServices;
        private IQueueContextFactory _contextFactory;

        public OrderTests() 
        {
            this._contextFactory = new InMemoryQueueContextFactory();
            this._orderServices = new OrderServices(this._contextFactory);
        }

        [Fact]
        public async void ShouldListAllQueueOrders()
        {
            await this.SeedQueue();

            var orders = await this._orderServices.FetchAllOrders();
            
            Assert.NotNull(orders);
            Assert.Equal(100, orders.Count);
        }

        [Fact]
        public async void ShouldCreateOrder() 
        {
            Order order = await this._orderServices.CreateNewOrder(new Order 
            {
                PaymentType = PaymentTypeEnum.Cash,
                TotalValue = 100,
                Items = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Name = "Item 1",
                        Value = 100
                    }
                }
            });

            Assert.NotNull(order);
        }

        [Fact]
        public async Task ShouldNotCreateEmptyOrder()
        {
            var exception = await Assert.ThrowsAsync<BusinessException>(() => this._orderServices.CreateNewOrder(null));

            Assert.IsType<BusinessException>(exception);
            Assert.Equal("Order cannot be empty", exception.Message);
        }

        [Fact]
        public async Task ShouldNotCreateOrderWithEmptyItems()
        {
            var order = new Order
            {
                Items = null
            };

            var exception = await Assert.ThrowsAsync<BusinessException>(() => this._orderServices.CreateNewOrder(order));

            Assert.IsType<BusinessException>(exception);
            Assert.Equal("The order has no items", exception.Message);
        }

        [Fact]
        public async Task ShouldNotCreateOrderZeroValue()
        {
            var order = new Order
            {
                Items = new List<OrderItem>() 
                { 
                    new OrderItem()
                },
                TotalValue = 0
            };

            var exception = await Assert.ThrowsAsync<BusinessException>(() => this._orderServices.CreateNewOrder(order));

            Assert.IsType<BusinessException>(exception);
            Assert.Equal("Order value cannot be less or equal to 0", exception.Message);
        }

        private async Task SeedQueue()
        {
            for (int i = 1; i <= 100; ++i)
            {
                await this._orderServices.CreateNewOrder(new Order
                {
                    PaymentType = (PaymentTypeEnum)(i % 4),
                    TotalValue = i * 100,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Name = "Item " + i,
                            Value = i * 100
                        }
                    }
                });
            }
        }
    }
}
