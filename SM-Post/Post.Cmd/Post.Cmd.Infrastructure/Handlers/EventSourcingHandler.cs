using CQRS.Core.Domain;
using CQRS.Core.Handlres;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Cmd.Infrastructure.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<PostAggreagate>
    {
        private readonly IEventStore _eventStore;
        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<PostAggreagate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new PostAggreagate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            if(events == null || !events.Any()) 
            {
                return aggregate;
            }

            aggregate.ReplayEvents(events);
            aggregate.Version = events.Select(e => e.Version).Max();

            return aggregate;

        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUnCommitedChanges(), aggregate.Version);

            aggregate.MarkChangesAsCommited();
        }
    }
}
