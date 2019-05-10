using System;

namespace BRM.EventBroker.Interfaces
{
    public interface IBrokerKeyedEvents
    {
        /// <summary>
        /// Adds a listener to events publishing data of type <see cref="TEventData"/> to <paramref name="key"/>
        /// Subscribing anonymous functions is NOT advised, as the objects captured in the anonymous function closure
        /// persist in memory and introduce a memory leak. If you do subscribe an anonymous function, make sure to keep
        /// a reference to this function for proper unsubscription, or unsubscribe with the Clear method
        /// </summary>
        void Subscribe<TEventData>(string key, Action<TEventData> onPublish);

        /// <summary>
        /// Removes a listener from the event tied to <paramref name="key"/>
        /// </summary>
        void Unsubscribe<TEventData>(string key, Action<TEventData> onPublish);

        /// <summary>
        /// Removes all subscriptions.
        /// Useful when memory cleanup and object deferencing for later garbage collection
        /// </summary>
        void ClearEvents();
    }
    
    public interface IPublishKeyedEvents
    {
        /// <summary>
        /// Publishes data of type <see cref="TEventData"/> to all subscribers of type <see cref="TEventData"/> to <paramref name="key"/>
        /// </summary>
        void Publish<TEventData>(string key, TEventData data);
    }
}