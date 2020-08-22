using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// A Future which may be fulfilled by any user with the reference.
    /// </summary>
    public class Future<T> : BaseFuture<T>
    {
        /// <summary>
        /// Constructs a Future.
        /// </summary>
        public Future() : base()
        { }

        /// <summary>
        /// Constructs a BaseFuture which has already been fulfilled.
        /// </summary>
        /// <param name="value">The value of the future.</param>
        public Future(T value) : base(value)
        { }

        /// <summary>
        /// Fulfill this future with a value. Will throw if already fulfilled.
        /// </summary>
        /// <param name="value">Any value of T.</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when this future has already been fulfilled.
        /// </exception>
        public new void Fulfill(T value)
        {
            base.Fulfill(value);
        }

        /// <summary>
        /// If the referenced Future is not null, the Future will be fulfilled.
        /// The reference will be nulled.
        /// </summary>
        /// <param name="future">A reference to the Future.</param>
        /// <returns>True if the Future was updated. False if it wasn't.</returns>
        public static bool ClearAndFulfillIfUnfulfilled(ref Future<T> future, T result)
        {
            if (future != null)
            {
                var referencedTask = future;
                // Set null first so that if Fulfill throws, at least we have removed the operation
                future = null;
                if (!referencedTask.HasFulfilled)
                {
                    referencedTask.Fulfill(result);
                    return true;
                }
            }

            return false;
        }
    }
}
