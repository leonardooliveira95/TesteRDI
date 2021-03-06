using RestaurantOrderRouting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Data
{
    public interface IQueue<TModel> where TModel : AbstractModel
    {
        Task<IEnumerable<TModel>> List();
        Task<TModel> Dequeue();
        Task<TModel> Enqueue(TModel item);
        Task<TModel> Peek();
        Task<int> Count();
    }
}
