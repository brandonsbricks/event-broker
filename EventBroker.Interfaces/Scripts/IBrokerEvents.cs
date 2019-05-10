using System;

namespace BRM.EventBroker.Interfaces
{
    /// <summary>
    /// Decouples event processing from event publishing
    /// Although the processor and publisher are loosely coupled via the event data, neither the processor nor the
    /// publisher need to know about the other.
    /// </summary>
    public interface IBrokerEvents
    {
        /// <summary>
        /// Adds a listener to events publishing data of type <see cref="TEventData"/>
        /// Subscribing anonymous functions is NOT advised, as the objects captured in the anonymous function closure
        /// persist in memory and introduce a memory leak. If you do subscribe an anonymous function, make sure to keep
        /// a reference to this function for proper unsubscription, or unsubscribe with the Clear method
        /// </summary>
        void Subscribe<TEventData>(Action<TEventData> onPublish) where TEventData : class;
        
        /// <summary>
        /// Removes a listener to events publishing data of type <see cref="TEventData"/>
        /// </summary>
        void Unsubscribe<TEventData>(Action<TEventData> onPublish) where TEventData : class;
        
        /// <summary>
        /// Removes all listeners.
        /// Useful when memory cleanup and object deferencing for later garbage collection
        /// </summary>
        void ClearEvents();
    }
    
    public interface IPublishEvents
    {
        /// <summary>
        /// Publishes data of type <see cref="TEventData"/> to all subscribers of type <see cref="TEventData"/>
        /// </summary>
        void Publish<TEventData>(TEventData data) where TEventData : class;
    }
}