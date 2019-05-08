using System;
using System.Collections.Generic;
using BRM.TheDebugAdapter.Interfaces.V1;

namespace BRM.EventBroker.Implementations.V1
{
    internal class GenericEventBroker<TKey>
    {
        private readonly Dictionary<TKey, List<object>> _events = new Dictionary<TKey, List<object>>();
        private readonly IDebug _debugger;
        
        public GenericEventBroker(IDebug debugger = null)
        {
            _debugger = debugger;
        }

        public void Subscribe<T>(TKey key, Action<T> onPublish) where T : class
        {
            List<object> subscriptions;
            if (!_events.TryGetValue(key, out subscriptions))
            {
                subscriptions = new List<object>();
                _events.Add(key, subscriptions);
            }
            subscriptions.Add(onPublish);
        }

        public void Unsubscribe<T>(TKey key, Action<T> onPublish) where T : class
        {
            List<object> subscriptions;
            if (_events.TryGetValue(key, out subscriptions))
            {
                subscriptions.Remove(onPublish);
            }
        }

        public void Publish<T>(TKey key, T data) where T : class
        {
            List<object> subscriptions;
            if (!_events.TryGetValue(key, out subscriptions))
            {
                _debugger?.LogWarningFormat("No subscriptions found for event type: {0}", typeof(T).ToString());
                return;
            }
            
            _debugger?.LogFormat("Publishing {0}", typeof(T).ToString());
            for (int i = subscriptions.Count - 1; i >= 0; i--)
            {
                if (i >= subscriptions.Count)
                {
                    continue;
                }

                var sub = subscriptions[i] as Action<T>;
                sub?.Invoke(data);
            }
        }
        
        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}