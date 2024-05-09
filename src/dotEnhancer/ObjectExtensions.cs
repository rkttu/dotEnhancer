namespace dotEnhancer
{
#if NETSTANDARD1_0_OR_GREATER
    using System;

    /// <summary>
    /// Extension methods for objects.
    /// </summary>
    partial class ObjectExtensions
    {
        /// <summary>
        /// Ensures that the specified value is not null.
        /// </summary>
        /// <typeparam name="TObject">The type of the value.</typeparam>
        /// <param name="value">The value to check.</param>
        /// <returns>The original value if it is not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        public static TObject EnsureNotNull<TObject>(this TObject value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return value;
        }
    }
#endif // NETSTANDARD1_0_OR_GREATER

    public static partial class ObjectExtensions { }
}
