using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        /// <summary>
        /// return all events for a given aggregateId
        /// </summary>
        /// <param name="aggregateId">Id of the aggregate</param>
        /// <returns>List of events for the requested aggregate</returns>
        Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
    }
}
