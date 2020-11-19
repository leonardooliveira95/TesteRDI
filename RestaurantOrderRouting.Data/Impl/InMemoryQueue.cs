using RestaurantOrderRouting.Data.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RestaurantOrderRouting.Data.Impl
{
    public class InMemoryQueue<TModel> : IQueue<TModel> where TModel : AbstractModel
    {
        private readonly ConcurrentQueue<TModel> _queue;

        public InMemoryQueue() 
        {
            this._queue = new ConcurrentQueue<TModel>();
        }

        public Task<TModel> Enqueue(TModel item)
        {
            this._queue.Enqueue(item);
            return Task.FromResult(item);
        }

        public Task<TModel> Dequeue(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> List()
        {
            List<TModel> result = new List<TModel>();

            foreach (TModel model in _queue) 
            {
                result.Add(model);
            }

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<TModel> Peek(TModel item)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            return Task.FromResult(this._queue.Count());
        }
    }
}
