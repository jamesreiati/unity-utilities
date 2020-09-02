using System;

namespace Reiati.Utilities
{
    /// <summary>
    /// A property which is being eased towards some value.
    /// </summary>
    public struct Eased<T>
    {
        /// <summary>
        /// Construct an Eased&lt;T&gt;.
        /// </summary>
        /// <param name="target">The target value.</param>
        /// <param name="actual">The actual value.</param>
        public Eased(T target, T actual)
        {
            this.Target = target;
            this.Actual = actual;
        }

        /// <summary>
        /// The target value.
        /// </summary>
        public T Target;

        /// <summary>
        /// The actual value.
        /// </summary>
        public T Actual;
    }

    #region Custom Type Serializing

    /// <summary>
    /// A float which is being eased towards some value.
    /// </summary>
    [Serializable]
    public struct EasedFloat
    {
        /// <summary>
        /// Construct an Eased&lt;float&gt;.
        /// </summary>
        /// <param name="target">The target value.</param>
        /// <param name="actual">The actual value.</param>
        public EasedFloat(float target, float actual)
        {
            this.Target = target;
            this.Actual = actual;
        }

        /// <summary>
        /// The target value.
        /// </summary>
        public float Target;

        /// <summary>
        /// The actual value.
        /// </summary>
        public float Actual;
    }

    #endregion
}
