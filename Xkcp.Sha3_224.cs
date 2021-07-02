using System;
using System.Runtime.InteropServices;

namespace StirlingLabs
{
    public static unsafe partial class Xkcp
    {
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer (28 bytes).</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHA3_224")]
        public static extern int Sha3_224(byte* output, byte* input, nuint inputByteLen);
        
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (28 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_224(Span<byte> output, ReadOnlySpan<byte> input)
        {
            const int bytesRequired = 28;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_224(pOutput, pInput, (nuint)input.Length);
        }
        
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (28 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_224(Span<byte> output, byte[] input)
        {
            const int bytesRequired = 28;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_224(pOutput, pInput, (nuint)input.LongLength);
        }
        
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (28 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_224(byte[] output, ReadOnlySpan<byte> input)
        {
            const int bytesRequired = 28;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_224(pOutput, pInput, (nuint)input.Length);
        }
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (28 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_224(byte[] output, byte[] input)
        {
            const int bytesRequired = 28;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_224(pOutput, pInput, (nuint)input.LongLength);
        }
        
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="input">The input message.</param>
        /// <returns>The output buffer (28 bytes).</returns>
        public static byte[] Sha3_224(ReadOnlySpan<byte> input)
        {
            var output = new byte[28];
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
            {
                return 0 == Sha3_224(pOutput, pInput, (nuint)input.Length)
                    ? output
                    : throw new NotImplementedException("Hashing failed.");
            }
        }
        
        /// <summary>
        /// Implementation of SHA3-224 [FIPS 202].
        /// </summary>
        /// <param name="input">The input message.</param>
        /// <returns>The output buffer (28 bytes).</returns>
        public static byte[] Sha3_224(byte[] input)
        {
            var output = new byte[28];
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
            {
                return 0 == Sha3_224(pOutput, pInput, (nuint)input.LongLength)
                    ? output
                    : throw new NotImplementedException("Hashing failed.");
            }
        }
    }
}
