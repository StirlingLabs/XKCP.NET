using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace StirlingLabs
{
    public static partial class Xkcp
    {
        private static readonly bool Avx512IsSupported = ((X86Base.CpuId(7, 0).Ebx >> 16) & 1) != 0;

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
                        if (NativeLibrary.TryLoad("runtimes/osx-x64/native/libXKCP-AVX512.dylib", out lib))
                            return lib;
                    }
                    if (Avx2.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-AVX2.dylib", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/osx-x64/native/libXKCP-AVX2.dylib", out lib))
                            return lib;
                    }
                    if (Sse3.IsSupported)
                    {
                        if (NativeLibrary.TryLoad("libXKCP-SSE3.dylib", out lib))
                            return lib;
                        if (NativeLibrary.TryLoad("runtimes/osx-x64/native/libXKCP-SSE3.dylib", out lib))
                            return lib;
                    }
                    if (NativeLibrary.TryLoad("libXKCP.dylib", out lib))
                        return lib;
                    if (NativeLibrary.TryLoad("runtimes/osx-x64/native/libXKCP.dylib", out lib))
                        return lib;
                }

                throw new DllNotFoundException("XKCP native library not found.");
            });
    }
}
