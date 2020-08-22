using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// A read only view of an object representing a Future which may be fulfilled with some value.
    /// </summary>
    public interface IObservableFuture<T>
    {
        /// <summary>
        /// True if the future has been fulfilled. False if it has not.
        /// </summary>
        bool HasFulfilled { get; }

        /// <summary>
        /// An event invoked when the value has been fulfilled.
        /// </summary>
        event FulfillmentHandler<T> OnFulfilled;

        /// <summary>
        /// The value of the future if it has fulfilled. Will throw if it has not fulfilled.
        /// </summary>
        /// <value>Any value defined by the Future provider.</value>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if HasFulfilled is false when accessing this field.
        /// </exception>
        T Value { get; }

        /// <summary>
        /// The value of the future if it has fulfilled, or the default value for type T if it has not.
        /// </summary>
        /// <returns>Any value defined by the Future provider, or the default value for type T.</returns>
        /// <remarks>
        /// Use this if you're certain HasFulfilled has already been checked, and you don't want to
        /// incur the cost of an extra access of IsRunning.
        /// </remarks>
        T GetValueOrDefault();
    }
}
