using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Core.consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}