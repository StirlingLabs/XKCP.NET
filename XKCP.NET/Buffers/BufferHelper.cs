using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StirlingLabs.Buffers;

internal static class BufferHelper {

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetHashCode(ref byte first, uint length) {
    Debug.Assert(length % 4 == 0, "Buffer lengths should be clean multiples of 4.");

    var hc = new HashCode();
    var i = 0;

    if (Environment.Is64BitProcess) {
      var lm8 = length - 8;
      do {
        hc.Add(Unsafe.As<byte, ulong>(ref Unsafe.Add(ref first, i)));
        i += 8;
      } while (i <= lm8);

      if (i > length - 4)
        return hc.ToHashCode();

      hc.Add(Unsafe.As<byte, uint>(ref Unsafe.Add(ref first, i)));
    }
    else {
      var lm4 = length - 4;
      do {
        hc.Add(Unsafe.As<byte, uint>(ref Unsafe.Add(ref first, i)));
        i += 4;
      } while (i <= lm4);
    }

    return hc.ToHashCode();
  }

#if !NET5_0_OR_GREATER
  internal static unsafe string ToHexString(ReadOnlySpan<byte> bytes) {
    Span<char> chars = stackalloc char[bytes.Length * 2];
    for (var i = 0; i < bytes.Length; i++) {
      var b = bytes[i];
      var hi = b & 0xF0;
      var lo = b & 0x0F;
      chars[i * 2] = (char)(hi >= 10 ? hi + ('A' - 10) : hi + '0');
      chars[i * 2 + 1] = (char)(lo >= 10 ? lo + ('A' - 10) : lo + '0');
    }

    fixed (char* pChars = chars)
      return new(pChars);
  }
#endif

}