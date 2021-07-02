using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace StirlingLabs.Buffers
{
    using BufferType = BufferOf48Bytes;

    [PublicAPI]
    [StructLayout(LayoutKind.Explicit, Size = ConstLength)]
    public struct BufferOf48Bytes
    {
        public static BufferComparer Comparer => BufferComparer.Instance;

        public const int ConstLength = 48;

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ConstLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe ref byte GetPinnableReference()
            => ref Unsafe.AsRef<byte>(Unsafe.AsPointer(ref Unsafe.AsRef(_0)));

        public ref byte this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                if (index >= ConstLength) throw new IndexOutOfRangeException();
                return ref Unsafe.Add(ref GetPinnableReference(), (nint)index);
            }
        }

        public ref byte this[Index indexExpr]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                var index = indexExpr.Value;
                if (indexExpr.IsFromEnd) index = ConstLength - index;
                if (index >= ConstLength) throw new IndexOutOfRangeException();
                return ref Unsafe.Add(ref GetPinnableReference(), (nint)index);
            }
        }

        public Span<byte> this[Range rangeExpr]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                var start = rangeExpr.Start.Value;
                if (rangeExpr.Start.IsFromEnd) start = ConstLength - start;
                var end = rangeExpr.End.Value;
                if (rangeExpr.End.IsFromEnd) end = ConstLength - end;
                if (start >= Length) throw new IndexOutOfRangeException();
                if (end >= Length || end > start) throw new IndexOutOfRangeException();
                var length = end - start;
                ref var startRef = ref Unsafe.Add(ref GetPinnableReference(), (nint)start);
                return MemoryMarshal.CreateSpan(ref startRef, length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Span<byte>(BufferType buf)
            => MemoryMarshal.CreateSpan(ref buf.GetPinnableReference(), ConstLength);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator ReadOnlySpan<byte>(BufferType buf)
            => MemoryMarshal.CreateReadOnlySpan(ref buf.GetPinnableReference(), ConstLength);

        public override string ToString()
            => Convert.ToHexString(this);

        public bool Equals(BufferType other)
            => ((ReadOnlySpan<byte>)this).SequenceEqual(other);

        public override bool Equals(object obj)
            => obj is BufferType other && Equals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => BufferHelper.GetHashCode(ref this[0u], ConstLength);

        public static bool operator ==(BufferType left, BufferType right)
            => left.Equals(right);

        public static bool operator !=(BufferType left, BufferType right)
            => !left.Equals(right);
        public int CompareTo(BufferType other)
            => ((ReadOnlySpan<byte>)this).SequenceCompareTo(other);

        public static bool operator <(BufferType left, BufferType right)
            => ((ReadOnlySpan<byte>)left).SequenceCompareTo(right) < 0;

        public static bool operator >(BufferType left, BufferType right)
            => ((ReadOnlySpan<byte>)left).SequenceCompareTo(right) > 0;

        public static bool operator <=(BufferType left, BufferType right)
            => ((ReadOnlySpan<byte>)left).SequenceCompareTo(right) <= 0;

        public static bool operator >=(BufferType left, BufferType right)
            => ((ReadOnlySpan<byte>)left).SequenceCompareTo(right) >= 0;

        [FieldOffset(0)]
        private byte _0;
    }
}
