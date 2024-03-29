using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using StirlingLabs.Buffers;

namespace StirlingLabs; 

public static unsafe partial class Xkcp
{
    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">Pointer to the output buffer (64 bytes).</param>
    /// <param name="input">Pointer to the input message.</param>
    /// <param name="inputByteLen">The length of the input message in bytes.</param>
    /// <returns>0 if successful, 1 otherwise.</returns>
#if NET5_0_OR_GREATER
    [SuppressGCTransition]
#endif
    [DllImport("XKCP", EntryPoint = "SHA3_512")]
    public static extern int Sha3_512(byte* output, byte* input, nuint inputByteLen);

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (64 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_512(Span<byte> output, ReadOnlySpan<byte> input)
    {
        const int bytesRequired = 64;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (64 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_512(Span<byte> output, byte[] input)
    {
        const int bytesRequired = 64;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.LongLength);
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (64 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_512(byte[] output, ReadOnlySpan<byte> input)
    {
        const int bytesRequired = 64;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (64 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_512(byte[] output, byte[] input)
    {
        const int bytesRequired = 64;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.LongLength);
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="input">The input message.</param>
    /// <returns>The output buffer (64 bytes).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] Sha3_512(ReadOnlySpan<byte> input)
    {
        var output = new byte[64];
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
        {
            return 0 == Sha3_512(pOutput, pInput, (nuint)input.Length)
                ? output
                : throw new NotImplementedException("Hashing failed.");
        }
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="input">The input message.</param>
    /// <returns>The output buffer (64 bytes).</returns>
    public static byte[] Sha3_512(byte[] input)
    {
        var output = new byte[64];
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
        {
            return 0 == Sha3_512(pOutput, pInput, (nuint)input.LongLength)
                ? output
                : throw new NotImplementedException("Hashing failed.");
        }
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (64 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_512(out BufferOf64Bytes output, ReadOnlySpan<byte> input)
    {
        Unsafe.SkipInit(out output);
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_512(pOutput, pInput, (nuint)input.Length);
    }

    /// <summary>
    /// Implementation of SHA3-512 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (64 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_512(out BufferOf64Bytes output, byte[] input)
    {
        Unsafe.SkipInit(out output);
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_512(pOutput, pInput, (nuint)input.LongLength);
    }
}