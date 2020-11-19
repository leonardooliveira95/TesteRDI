using RestaurantOrderRouting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Data
{
    public interface IQueueContext
    {
        IQueue<TModel> GetQueue<TModel>() where TModel : AbstractModel;
    }
}
