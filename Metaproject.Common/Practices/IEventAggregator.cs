using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasuryIS.Practices
{
    public interface IEventAggregator
    {
        void PublishEvent<TEventType>(TEventType eventToPublish);
        void SubsribeEvent(object subscriber);
        void Clear();
    }
}
