using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common;

namespace SqlGen
{
    public class OptionsCommitedAggEvent : AggregatedEvent
    {
        public Guid CommandId { get; set; }
        
    }
}
