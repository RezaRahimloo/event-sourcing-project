﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Commands
{
    public abstract class Message
    {
        public Guid Id { get; set; }
    }
}
