using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using iTin.Hardware.Specification;
using iTin.Hardware.Specification.Cpuid;
using JetBrains.Annotations;

namespace StirlingLabs
{
    [PublicAPI]
    [SuppressMessage("Interoperability", "CA1401", Justification = "Desired.")]
    public static unsafe class Xkcp
    {
        private static readonly bool Avx512IsSupported
            = CPUID.Instance.IsAvailable
            && CPUID.Instance.Leafs.GetProperty(LeafProperty.ExtendedFeatures.AVX512_F).Success;
        static Xkcp()
            => NativeLibrary.SetDllImportResolver(typeof(Xkcp).Assembly, (name, assembly, path) => {
                nint lib;
                if (name != "XKCP")
                    return default;

                // try agnostic library names first, may load from system's impl
                if (Avx512IsSupported)
                    if (NativeLibrary.TryLoad("XKCP-AVX512", out lib))
                        return lib;
                if (Avx2.IsSupported)
                    if (NativeLibrary.TryLoad("XKCP-AVX2", out lib))
                        return lib;
                if (Sse3.IsSupported)
                    if (NativeLibrary.TryLoad("XKCP-SSE3", out lib))
                        return lib;
                if (NativeLibrary.TryLoad("XKCP", out lib))
                    return lib;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    if (Avx512IsSupported)
                    {
                        if (NativeLibrary.TryLoad("XKCP-AVX512", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/win-x64/native/XKCP-AVX512", out lib))
                            return lib;
                    }
                    if (Avx2.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("XKCP-AVX2", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/win-x64/native/XKCP-AVX2", out lib))
                            return lib;
                    }
                    if (Sse3.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("XKCP-SSE3", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/win-x64/native/XKCP-SSE3", out lib))
                            return lib;
                    }
                    if (NativeLibrary.TryLoad("XKCP", out lib))
                        return lib;
                    if (NativeLibrary.TryLoad("runtimes/win-x64/native/XKCP", out lib))
                        return lib;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (Avx512IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-AVX512.so", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/linux-x64/native/libXKCP-AVX512.so", out lib))
                            return lib;
                    }
                    if (Avx2.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-AVX2.so", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/linux-x64/native/libXKCP-AVX2.so", out lib))
                            return lib;
                    }
                    if (Sse3.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-SSE3.so", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/linux-x64/native/libXKCP-SSE3.so", out lib))
                            return lib;
                    }
                    if (NativeLibrary.TryLoad("libXKCP.so", out lib))
                        return lib;
                    if (NativeLibrary.TryLoad("runtimes/win-x64/native/libXKCP.so", out lib))
                        return lib;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (Avx512IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-AVX512.dylib", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/linux-x64/native/libXKCP-AVX512.dylib", out lib))
                            return lib;
                    }
                    if (Avx2.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-AVX2.dylib", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/linux-x64/native/libXKCP-AVX2.dylib", out lib))
                            return lib;
                    }
                    if (Sse3.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-SSE3.dylib", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/linux-x64/native/libXKCP-SSE3.dylib", out lib))
                            return lib;
                    }
                    if (NativeLibrary.TryLoad("libXKCP.dylib", out lib))
                        return lib;
                    if (NativeLibrary.TryLoad("runtimes/win-x64/native/libXKCP.dylib", out lib))
                        return lib;
                }

                throw new DllNotFoundException("XKCP native library not found.");
            });

        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer.</param>
        /// <param name="outputByteLen">The desired number of output bytes.</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHAKE128")]
        public static extern int Shake128(byte* output, nuint outputByteLen, byte* input, nuint inputByteLen);

        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer.</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Shake128(Span<byte> output, ReadOnlySpan<byte> input)
        {
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Shake128(pOutput, (nuint)output.Length, pInput, (nuint)input.Length);
        }

        /// <summary>
        /// Implementation of the SHAKE256 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer.</param>
        /// <param name="outputByteLen">The desired number of output bytes.</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHAKE256")]
        public static extern int Shake256(byte* output, nuint outputByteLen, byte* input, nuint inputByteLen);

        /// <summary>
        /// Implementation of the SHAKE128 extendable output function (XOF) [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer.</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Shake256(Span<byte> output, ReadOnlySpan<byte> input)
        {
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Shake256(pOutput, (nuint)output.Length, pInput, (nuint)input.Length);
        }
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
        /// Implementation of SHA3-256 [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer (32 bytes).</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHA3_256")]
        public static extern int Sha3_256(byte* output, byte* input, nuint inputByteLen);

        /// <summary>
        /// Implementation of SHA3-256 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (32 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_256(Span<byte> output, ReadOnlySpan<byte> input)
        {
            const int bytesRequired = 32;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
        }

        /// <summary>
        /// Implementation of SHA3-384 [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer (48 bytes).</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHA3_384")]
        public static extern int Sha3_384(byte* output, byte* input, nuint inputByteLen);

        /// <summary>
        /// Implementation of SHA3-384 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (48 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_384(Span<byte> output, ReadOnlySpan<byte> input)
        {
            const int bytesRequired = 48;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_384(pOutput, pInput, (nuint)input.Length);
        }

        /// <summary>
        /// Implementation of SHA3-512 [FIPS 202].
        /// </summary>
        /// <param name="output">Pointer to the output buffer (64 bytes).</param>
        /// <param name="input">Pointer to the input message.</param>
        /// <param name="inputByteLen">The length of the input message in bytes.</param>
        /// <returns>0 if successful, 1 otherwise.</returns>
        [DllImport("XKCP", EntryPoint = "SHA3_512")]
        public static extern int Sha3_512(byte* output, byte* input, nuint inputByteLen);

        /// <summary>
        /// Implementation of SHA3-512 [FIPS 202].
        /// </summary>
        /// <param name="output">The output buffer (64 bytes).</param>
        /// <param name="input">The input message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool Sha3_512(Span<byte> output, ReadOnlySpan<byte> input)
        {
            const int bytesRequired = 64;
            if (output.Length < bytesRequired) throw new ArgumentException("Requires at least " + bytesRequired + " bytes.", nameof(output));
            fixed (byte* pOutput = output)
            fixed (byte* pInput = input)
                return 0 == Sha3_256(pOutput, pInput, (nuint)input.Length);
        }
    }
}
