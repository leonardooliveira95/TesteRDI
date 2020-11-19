﻿using RestaurantOrderRouting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantOrderRouting.Data.Impl
{
    public class InMemoryQueueContextFactory : IQueueContextFactory
    {
        private IQueueContext _context;

        public InMemoryQueueContextFactory() 
        {
            this._context = new InMemoryQueueContext();
        }

        public IQueueContext CreateNew() 
        {
            return this._context;
        }
    }
}
