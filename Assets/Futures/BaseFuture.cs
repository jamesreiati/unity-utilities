using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// Base functionality for all Futures. Responsible for ensuring the event
    /// gets invoked when the future has been fulfilled.
    /// </summary>
    public abstract class BaseFuture : IObservableFuture
    {
        /// <summary>
        /// The event invoked when the value gets fulfilled.
        /// </summary>
        private event FulfillmentHandler onFulfilled;

        /// <summary>
        /// Constructs a BaseFuture.
        /// </summary>
        /// <param name="alreadyFulfilled">
        /// Whether the future should be considered fulfilled upon construction.
        /// </param>
        public BaseFuture(bool alreadyFulfilled = false)
        {
            this.HasFulfilled = alreadyFulfilled;
        }

        /// <summary>
        /// True if the future has been fulfilled. False if it has not.
        /// </summary>
        public bool HasFulfilled { get; private set; }

        /// <summary>
        /// An event invoked when the value has been fulfilled.
        /// </summary>
        public event FulfillmentHandler OnFulfilled
        {
            add
            {
                if (this.HasFulfilled)
                {
                    value.Invoke();
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
        /// Mark this future as fulfilled. Will throw if already fulfilled.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when this future has already been fulfilled.
        /// </exception>
        protected void Fulfill()
        {
            if (this.HasFulfilled)
            {
                throw new InvalidOperationException("Future has already been fulfilled.");
            }

            this.HasFulfilled = true;

            if (this.onFulfilled != null)
            {
                this.onFulfilled.Invoke();
                this.onFulfilled = null;
            }
        }
    }
}
