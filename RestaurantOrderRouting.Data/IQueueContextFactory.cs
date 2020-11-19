using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Data
{
    public interface IQueueContextFactory
    {
        IQueueContext CreateNew();
    }
}
