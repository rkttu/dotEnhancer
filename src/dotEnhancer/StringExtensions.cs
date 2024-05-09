namespace dotEnhancer
{
#if NETSTANDARD1_0_OR_GREATER
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    partial class StringExtensions
    {
        public static TCharArray EnsureNotNullOrEmpty<TCharArray>(this TCharArray value)
            where TCharArray : IEnumerable<char>
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.Count() < 1)
                throw new ArgumentException("The string is empty.", nameof(value));

            return value;
        }

        public static StringBuilder AsStringBuilder<TCharArray>(this TCharArray value)
            where TCharArray : IEnumerable<char>
            => new StringBuilder(value as string ?? new string(value.ToArray()));

        public static StringBuilder AsStringBuilder<TCharArray>(this TCharArray value, int capacity)
            where TCharArray : IEnumerable<char>
            => new StringBuilder(value as string ?? new string(value.ToArray()), capacity);

        public static StringBuilder AsStringBuilder<TCharArray>(this TCharArray value, int startIndex, int length, int capacity)
            where TCharArray : IEnumerable<char>
            => new StringBuilder(value as string ?? new string(value.ToArray()), startIndex, length, capacity);

        public static string AsString<TCharArray>(this TCharArray value)
            where TCharArray : IEnumerable<char>
            => new string(value.ToArray());

        public static unsafe string AsString(this IntPtr value)
            => new string((char*)value.ToPointer());

        public static unsafe string AsString(this UIntPtr value)
            => new string((char*)value.ToPointer());
    }
#endif // NETSTANDARD1_0_OR_GREATER

    public static partial class StringExtensions { }
}
