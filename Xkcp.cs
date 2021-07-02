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
    public static partial class Xkcp
    {
        private static readonly bool Avx512IsSupported;

        public static void Init() {}
    }
}
