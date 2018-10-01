using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreasuryIS.Practices;

namespace Metaproject.Common.Practices
{
    public class EventAggregator : IEventAggregator
    {


        private Dictionary<Type, List<object>> eventSubsribers = new Dictionary<Type, List<object>>();

        private readonly object lockSubscriberDictionary = new object();

        public EventAggregator
            ()
        {

        }

        #region IEventAggregator Members              

        /// <summary>
        /// Publish an event
        /// </summary>
        /// <typeparam name="TEventType"></typeparam>
        /// <param name="eventToPublish"></param>
        public void PublishEvent<TEventType>(TEventType eventToPublish)
        {

            if (eventToPublish is AggregatedEvent)
            {
                AggregatedEvent aggEvent = eventToPublish as AggregatedEvent;
                if (aggEvent.IsHandled)
                    return;
            }

            var subsriberType = typeof(ISubscriber<>).MakeGenericType(typeof(TEventType));

            var subscribers = GetSubscriberList(subsriberType);

            var subsribersToBeRemoved = new List<object>();

            foreach (var weakSubsriber in subscribers)
            {
                if (weakSubsriber != null)
                {
                    var subscriber = (ISubscriber<TEventType>) weakSubsriber;
                    InvokeSubscriberEvent<TEventType>(eventToPublish, subscriber);
                }
                else
                {
                

                }

            } 

            if (subsribersToBeRemoved.Any())
            {
                lock (lockSubscriberDictionary)
                {
                    foreach (var remove in subsribersToBeRemoved)
                    {
                        subscribers.Remove(remove);

                    } //End-for-each (var remove in subsribersToBeRemoved)

                } //End-lock (lockSubscriberDictionary)

            } //End-if (subsribersToBeRemoved.Any())
        }

        /// <summary>
        /// Subribe to an event.
        /// </summary>
        /// <param name="subscriber"></param>
        public void SubsribeEvent(object subscriber)
        {
            lock (lockSubscriberDictionary)
            {
                var subsriberTypes = subscriber.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscriber<>));


                foreach (var subsriberType in subsriberTypes)
                {
                    List<object> subscribers = GetSubscriberList(subsriberType);
                    if (!subscribers.Contains(subscriber))
                    {
                        subscribers.Add(subscriber);
                    }

                } //End-for-each (var subsriberType in subsriberTypes)

            } //End-lock (lockSubscriberDictionary)
        }

        public void Clear()
        {
            eventSubsribers.Clear();
        }

        #endregion

        private void InvokeSubscriberEvent<TEventType>(TEventType eventToPublish, ISubscriber<TEventType> subscriber)
        {
            //Synchronize the invocation of method 

            SynchronizationContext syncContext = SynchronizationContext.Current;

            if (syncContext == null)
            {
                syncContext = new SynchronizationContext();

            } //End-if (syncContext == null)

            if (eventToPublish is AggregatedEvent)
            {
                AggregatedEvent aggEvent = eventToPublish as AggregatedEvent;
                if (aggEvent.IsHandled)
                    return;
            }

            subscriber.OnEventHandler(eventToPublish);

            //syncContext.Post(s => subscriber.OnEventHandler(eventToPublish), null);
        }

        private List<object> GetSubscriberList(Type subsriberType)
        {
            List<object> subsribersList = null;

            lock (lockSubscriberDictionary)
            {
                bool found = this.eventSubsribers.TryGetValue(subsriberType, out subsribersList);

                if (!found)
                {
                    //First time create the list.

                    subsribersList = new List<object>();

                    this.eventSubsribers.Add(subsriberType, subsribersList);

                } //End-if (!found)

            } //End-lock (lockSubscriberDictionary)

            return subsribersList;
        }
    }
}
   


