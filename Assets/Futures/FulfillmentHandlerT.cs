using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// A handler to be invoked upon fulfillment.
    /// </summary>
    /// <typeparam name="T">The type of the object being returned when this future is fulfilled.</typeparam>
    public delegate void FulfillmentHandler<T>(T value);
}
