using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace StirlingLabs.Buffers
{
    using X86Sse42 = System.Runtime.Intrinsics.X86.Sse42;
    using X64Sse42 = System.Runtime.Intrinsics.X86.Sse42.X64;
    using Arm32Crc32 = System.Runtime.Intrinsics.Arm.Crc32;
    using Arm64Crc32 = System.Runtime.Intrinsics.Arm.Crc32.Arm64;

    internal static class BufferHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode(ref byte first, uint length)
        {
            Debug.Assert(length % 4 == 0, "Buffer lengths should be clean multiples of 4.");

            var hc = new HashCode();
            var i = 0;

            if (Environment.Is64BitProcess)
            {
                var lm8 = length - 8;
                do
                {
                    hc.Add(Unsafe.As<byte, ulong>(ref Unsafe.Add(ref first, i)));
                    i += 8;
                } while (i <= lm8);

                if (i > length - 4)
                    return hc.ToHashCode();

                hc.Add(Unsafe.As<byte, uint>(ref Unsafe.Add(ref first, i)));
            }
            else
            {
                var lm4 = length - 4;
                do
                {
                    hc.Add(Unsafe.As<byte, uint>(ref Unsafe.Add(ref first, i)));
                    i += 4;
                } while (i <= lm4);
            }

            return hc.ToHashCode();
        }
    }
}
