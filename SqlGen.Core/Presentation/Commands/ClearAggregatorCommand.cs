using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class ClearAggregatorCommand : ICommand
    {
        private readonly IEventAggregator _eventAggregator;

        public ClearAggregatorCommand(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public Guid Id { get; }
        public void Run()
        {
            _eventAggregator.Clear();
        }
    }
}
