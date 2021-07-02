using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace StirlingLabs.Buffers
{
    [PublicAPI]
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct BufferOf28Bytes
    {
        public const int ConstLength = 28;

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

        [FieldOffset(0)]
        private byte _0;
    }
}
