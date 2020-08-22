using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// A read only view of an object representing a Future which may be fulfilled.
    /// </summary>
    public interface IObservableFuture
    {
        /// <summary>
        /// True if the future has been fulfilled. False if it has not.
        /// </summary>
        bool HasFulfilled { get; }

        /// <summary>
        /// An event invoked when the value has been fulfilled.
        /// </summary>
        event FulfillmentHandler OnFulfilled;
    }
}
