using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantOrderRouting.Logic.Models;
using RestaurantOrderRouting.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Controllers
{
    /// <summary>
    /// Controller for fetching and placing orders
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices) 
        {
            this._orderServices = orderServices;
        }

        /// <summary>
        /// Displays the current orders on the queue 
        /// </summary>
        /// <returns> Object containing all the orders on the queue </returns>
        [HttpGet]
        public async Task<IEnumerable<OrderSimple>> Get()
        {
            List<OrderSimple> result = await _orderServices.FetchAllOrders();
            return result;
        }
    }
}
