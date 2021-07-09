using System.Collections.Generic;

namespace StirlingLabs.Buffers
{
    public sealed class BufferComparer
        : IEqualityComparer<BufferOf28Bytes>,
            IEqualityComparer<BufferOf32Bytes>,
            IEqualityComparer<BufferOf48Bytes>,
            IEqualityComparer<BufferOf64Bytes>,
            IComparer<BufferOf28Bytes>,
            IComparer<BufferOf32Bytes>,
            IComparer<BufferOf48Bytes>,
            IComparer<BufferOf64Bytes>
    {
        public static readonly BufferComparer Instance = new();

        public bool Equals(BufferOf28Bytes x, BufferOf28Bytes y)
            => x == y;

        public int GetHashCode(BufferOf28Bytes obj)
            => obj.GetHashCode();

        public int Compare(BufferOf28Bytes x, BufferOf28Bytes y)
            => x.CompareTo(y);

        public bool Equals(BufferOf32Bytes x, BufferOf32Bytes y)
            => x == y;

        public int GetHashCode(BufferOf32Bytes obj)
            => obj.GetHashCode();

        public int Compare(BufferOf32Bytes x, BufferOf32Bytes y)
            => x.CompareTo(y);

        public bool Equals(BufferOf48Bytes x, BufferOf48Bytes y)
            => x == y;

        public int GetHashCode(BufferOf48Bytes obj)
            => obj.GetHashCode();

        public int Compare(BufferOf48Bytes x, BufferOf48Bytes y)
            => x.CompareTo(y);

        public bool Equals(BufferOf64Bytes x, BufferOf64Bytes y)
            => x == y;

        public int GetHashCode(BufferOf64Bytes obj)
            => obj.GetHashCode();

        public int Compare(BufferOf64Bytes x, BufferOf64Bytes y)
            => x.CompareTo(y);
    }
}
