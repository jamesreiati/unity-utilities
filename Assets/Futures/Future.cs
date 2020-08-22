using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// A Future which may be fulfilled by any user with the reference.
    /// </summary>
    public class Future : BaseFuture
    {
        /// <summary>
        /// Constructs a Future.
        /// </summary>
        /// <param name="alreadyFulfilled">
        /// Whether the future should be considered fulfilled upon construction.
        /// </param>
        public Future(bool alreadyFulfilled = false)
            : base(alreadyFulfilled)
        { }

        /// <summary>
        /// Mark this future as fulfilled. Will throw if already fulfilled.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when this future has already been fulfilled.
        /// </exception>
        public new void Fulfill()
        {
            base.Fulfill();
        }

        /// <summary>
        /// If the referenced Future is not null, the Future will be fulfilled.
        /// The reference will be nulled.
        /// </summary>
        /// <param name="future">A reference to the Future.</param>
        /// <returns>True if the Future was updated. False if it wasn't.</returns>
        public static bool ClearAndFulfillIfUnfulfilled(ref Future future)
        {
            if (future != null)
            {
                var referencedTask = future;
                // Set null first so that if Fulfill throws, at least we have removed the operation
                future = null;
                if (referencedTask.HasFulfilled)
                {
                    referencedTask.Fulfill();
                    return true;
                }
            }

            return false;
        }
    }
}
