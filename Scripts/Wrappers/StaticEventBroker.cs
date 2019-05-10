using System;
using BRM.EventBroker.Interfaces;
using BRM.DebugAdapter.Interfaces;

namespace BRM.EventBroker.Implementations.V1
{
    /// <summary>
    /// Wrapper to a static instance of an <see cref="EventBroker"/>
    /// </summary>
    public class StaticEventBroker : IBrokerEvents, IPublishEvents
    {
        private static EventBroker _broker;

        public StaticEventBroker(IDebug debugger = null)
        {
            _broker = _broker ?? new EventBroker(debugger);
        }

        public void Subscribe<TEventData>(Action<TEventData> onPublish) where TEventData : class
        {
            _broker.Subscribe(onPublish);
        }

        public void Unsubscribe<TEventData>(Action<TEventData> onPublish) where TEventData : class
        {
            _broker.Unsubscribe(onPublish);
        }

        public void Publish<TEventData>(TEventData data) where TEventData : class
        {
            _broker.Publish(data);
        }

        public void ClearEvents()
        {
            _broker.ClearEvents();
        }
    }
}