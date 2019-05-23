using System;
using BRM.EventBrokers.Interfaces;
using BRM.DebugAdapter.Interfaces;

namespace BRM.EventBrokers
{
    /// <summary>
    /// Wrapper for <see cref="GenericEventBroker{TKey}"/> using <typeparamref name="string"/> for keys
    /// </summary>
    public sealed class KeyedEventBroker : IBrokerKeyedEvents, IPublishKeyedEvents
    {
        private GenericEventBroker<string> _broker;
        
        public KeyedEventBroker(IDebug debugger = null)
        {
            _broker = new GenericEventBroker<string>(debugger);
        }

        public void Subscribe<TEventData>(string key, Action<TEventData> onPublish)
        {
            _broker.Subscribe(key, onPublish);
        }

        public void Unsubscribe<TEventData>(string key, Action<TEventData> onPublish)
        {
            _broker.Unsubscribe(key, onPublish);
        }

        public void Publish<TEventData>(string key, TEventData data)
        {
            _broker.Publish(key, data);
        }

        public void ClearEvents()
        {
            _broker.ClearEvents();
        }
    }
}