using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// Base functionality for all Futures. Responsible for ensuring the event
    /// gets invoked when the future has been fulfilled.
    /// </summary>
    public abstract class BaseFuture<T> : IObservableFuture<T>
    {
        /// <summary>
        /// The event invoked when the value gets fulfilled.
        /// </summary>
        private event FulfillmentHandler<T> onFulfilled;

        /// <summary>
        /// The value of the Future.
        /// </summary>
        private T value = default;

        /// <summary>
        /// Constructs a BaseFuture.
        /// </summary>
        public BaseFuture()
        {
            this.HasFulfilled = false;
        }

        /// <summary>
        /// Constructs a BaseFuture which has already been fulfilled.
        /// </summary>
        /// <param name="value">The value of the future.</param>
        public BaseFuture(T value)
        {
            this.HasFulfilled = true;
            this.value = value;
        }

        /// <summary>
        /// True if the future has been fulfilled. False if it has not.
        /// </summary>
        public bool HasFulfilled { get; private set; }

        /// <summary>
        /// An event invoked when the value has been fulfilled.
        /// </summary>
        public event FulfillmentHandler<T> OnFulfilled
        {
            add
            {
                if (this.HasFulfilled)
                {
                    value.Invoke(this.value);
                }
                else
                {
                    this.onFulfilled += value;
                }
            }
            remove
            {
                if (!this.HasFulfilled)
                {
                    this.onFulfilled -= value;
                }
                // no-op if it's completed.
            }
        }

        /// <summary>
        /// The value of the future if it has fulfilled. Will throw if it has not fulfilled.
        /// </summary>
        /// <value>Any value defined by the Future provider.</value>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if HasFulfilled is false when accessing this field.
        /// </exception>
        public T Value
        {
            get
            {
                if (!this.HasFulfilled)
                {
                    throw new InvalidOperationException("Future has not been fulfilled.");
                }
                return this.value;
            }
        }

        /// <summary>
        /// The value of the future if it has fulfilled, or the default value for type T if it has not.
        /// </summary>
        /// <returns>Any value defined by the Future provider, or the default value for type T.</returns>
        /// <remarks>
        /// Use this if you're certain HasFulfilled has already been checked, and you don't want to
        /// incur the cost of an extra access of IsRunning.
        /// </remarks>
        public T GetValueOrDefault()
        {
            return this.value;
        }

        /// <summary>
        /// Fulfill this future with a value. Will throw if already fulfilled.
        /// </summary>
        /// <param name="value">Any value of T.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when this future has already been fulfilled.
        /// </exception>
        protected void Fulfill(T value)
        {
            if (this.HasFulfilled)
            {
                throw new InvalidOperationException("Future has already been fulfilled.");
            }

            this.HasFulfilled = true;
            this.value = value;

            if (this.onFulfilled != null)
            {
                this.onFulfilled.Invoke(value);
                this.onFulfilled = null;
            }
        }
    }
}
