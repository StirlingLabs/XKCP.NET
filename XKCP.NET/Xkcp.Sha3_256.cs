using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using StirlingLabs.Buffers;

namespace StirlingLabs; 

public static unsafe partial class Xkcp
{
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">Pointer to the output buffer (32 bytes).</param>
    /// <param name="input">Pointer to the input message.</param>
    /// <param name="inputByteLen">The length of the input message in bytes.</param>
    /// <returns>0 if successful, 1 otherwise.</returns>
#if NET5_0_OR_GREATER
    [SuppressGCTransition]
#endif
    [DllImport("XKCP", EntryPoint = "SHA3_256")]
    public static extern int Sha3_256(byte* output, byte* input, nuint inputByteLen);
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (32 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_256(Span<byte> output, ReadOnlySpan<byte> input)
    {
        const int bytesRequired = 32;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (32 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_256(Span<byte> output, byte[] input)
    {
        const int bytesRequired = 32;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.LongLength);
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (32 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_256(byte[] output, ReadOnlySpan<byte> input)
    {
        const int bytesRequired = 32;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (32 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_256(byte[] output, byte[] input)
    {
        const int bytesRequired = 32;
        if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.LongLength);
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="input">The input message.</param>
    /// <returns>The output buffer (32 bytes).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] Sha3_256(ReadOnlySpan<byte> input)
    {
        var output = new byte[32];
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
        {
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length)
                ? output
                : throw new NotImplementedException("Hashing failed.");
        }
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="input">The input message.</param>
    /// <returns>The output buffer (32 bytes).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] Sha3_256(byte[] input)
    {
        var output = new byte[32];
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
        {
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.LongLength)
                ? output
                : throw new NotImplementedException("Hashing failed.");
        }
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (32 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_256(out BufferOf32Bytes output, ReadOnlySpan<byte> input)
    {
        Unsafe.SkipInit(out output);
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
    }
        
    /// <summary>
    /// Implementation of SHA3-256 [FIPS 202].
    /// </summary>
    /// <param name="output">The output buffer (32 bytes).</param>
    /// <param name="input">The input message.</param>
    /// <returns>True if successful, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Sha3_256(out BufferOf32Bytes output, byte[] input)
    {
        Unsafe.SkipInit(out output);
        fixed (byte* pOutput = output)
        fixed (byte* pInput = input)
            return 0 == Sha3_256(pOutput, pInput, (nuint)input.LongLength);
    }
}