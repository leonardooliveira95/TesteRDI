using RestaurantOrderRouting.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Logic.Services
{
    public interface IOrderServices
    {
        Task<List<OrderSimple>> FetchAllOrders();
    }
}
