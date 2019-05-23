using System;
using BRM.EventBrokers.Interfaces;
using BRM.DebugAdapter.Interfaces;

namespace BRM.EventBrokers
{
    /// <summary>
    /// Wrapper to a static instance of an <see cref="EventBroker"/>
    /// </summary>
    public sealed class StaticEventBroker : IBrokerEvents, IPublishEvents
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