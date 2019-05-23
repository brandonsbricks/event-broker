using System;
using BRM.EventBrokers.Interfaces;
using BRM.DebugAdapter.Interfaces;

namespace BRM.EventBrokers
{
    /// <summary>
    /// Wrapper for <see cref="GenericEventBroker{TKey}"/> using <typeparamref name="Type"/> for keys
    /// </summary>
    public sealed class EventBroker : IBrokerEvents, IPublishEvents
    {
        private GenericEventBroker<Type> _broker;
        
        public EventBroker(IDebug debugger = null)
        {
            _broker = new GenericEventBroker<Type>(debugger);
        }

        public void Subscribe<TEventData>(Action<TEventData> onPublish) where TEventData : class
        {
            _broker.Subscribe(typeof(TEventData), onPublish);
        }

        public void Unsubscribe<TEventData>(Action<TEventData> onPublish) where TEventData : class
        {
            _broker.Unsubscribe(typeof(TEventData), onPublish);
        }

        public void Publish<TEventData>(TEventData data) where TEventData : class
        {
            _broker.Publish(typeof(TEventData), data);
        }

        public void ClearEvents()
        {
            _broker.ClearEvents();
        }
    }
}