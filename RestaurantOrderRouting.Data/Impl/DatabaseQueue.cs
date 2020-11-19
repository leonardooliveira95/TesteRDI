using RestaurantOrderRouting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderRouting.Data.Impl
{
    //Example implementation of a queue that could be saved/retrieved from a database
    public class DatabaseQueue<TModel> : IQueue<TModel> where TModel : AbstractModel
    {
        public Task<TModel> Enqueue(TModel item)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> Dequeue(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> List()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> Peek(TModel item)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }
    }
}
