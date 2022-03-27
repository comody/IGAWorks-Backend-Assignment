using System;
using System.Collections.Generic;
using dfinery.backend.assignment.Models.search;

namespace dfinery.backend.assignment.store.IEventStoreService
{
    public interface IEventStoreService
    {
        List<EventSearchModel> GetEventModels(string user_id);
    }
}
