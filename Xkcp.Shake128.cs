using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StirlingLabs
{
    public static unsafe partial class Xkcp
    {
        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer.</param>
        /// <param name="outputByteLen">The desired number of output bytes.</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHAKE128"), SuppressGCTransition]
        public static extern int Shake128(byte* output, nuint outputByteLen, byte* input, nuint inputByteLen);
        
        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer.</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Shake128(Span<byte> output, ReadOnlySpan<byte> input)
        {
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Shake128(pOutput, (nuint)output.Length, pInput, (nuint)input.Length);
        }
        
        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer.</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Shake128(Span<byte> output, byte[] input)
        {
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Shake128(pOutput, (nuint)output.Length, pInput, (nuint)input.LongLength);
        }
        
        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer.</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Shake128(byte[] output, ReadOnlySpan<byte> input)
        {
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Shake128(pOutput, (nuint)output.LongLength, pInput, (nuint)input.Length);
        }

        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer.</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Shake128(byte[] output, byte[] input)
        {
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Shake128(pOutput, (nuint)output.LongLength, pInput, (nuint)input.LongLength);
        }
    }
}
