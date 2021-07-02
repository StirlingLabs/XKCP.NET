using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using NUnit.Framework;

namespace StirlingLabs
{
    public class XkcpTests
    {
        [Test, Order(-1)]
        public void Init()
            => Xkcp.Init();

        public static IEnumerable<object[]> Sha3_256Source
        {
            get {
                yield return new object[]
                {
                    // ReSharper disable once StringLiteralTypo
                    Encoding.UTF8.GetBytes("abc"),
                    new byte[]
                    {
                        0x3a, 0x98, 0x5d, 0xa7, 0x4f, 0xe2, 0x25, 0xb2,
                        0x04, 0x5c, 0x17, 0x2d, 0x6b, 0xd3, 0x90, 0xbd,
                        0x85, 0x5f, 0x08, 0x6e, 0x3e, 0x9d, 0x52, 0x5b,
                        0x46, 0xbf, 0xe2, 0x45, 0x11, 0x43, 0x15, 0x32
                    }
                };
                yield return new object[]
                {
                    Array.Empty<byte>(),
                    new byte[]
                    {
                        0xa7, 0xff, 0xc6, 0xf8, 0xbf, 0x1e, 0xd7, 0x66,
                        0x51, 0xc1, 0x47, 0x56, 0xa0, 0x61, 0xd6, 0x62,
                        0xf5, 0x80, 0xff, 0x4d, 0xe4, 0x3b, 0x49, 0xfa,
                        0x82, 0xd8, 0x0a, 0x4b, 0x80, 0xf8, 0x43, 0x4a
                    }
                };
                yield return new object[]
                {
                    // ReSharper disable once StringLiteralTypo
                    Encoding.UTF8.GetBytes("abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"),
                    new byte[]
                    {
                        0x41, 0xc0, 0xdb, 0xa2, 0xa9, 0xd6, 0x24, 0x08,
                        0x49, 0x10, 0x03, 0x76, 0xa8, 0x23, 0x5e, 0x2c,
                        0x82, 0xe1, 0xb9, 0x99, 0x8a, 0x99, 0x9e, 0x21,
                        0xdb, 0x32, 0xdd, 0x97, 0x49, 0x6d, 0x33, 0x76
                    }
                };
                yield return new object[]
                {
                    // ReSharper disable once StringLiteralTypo
                    Encoding.UTF8.GetBytes(
                        "abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmnoijklmnopjklmnopqklmnopqrlmnopqrsmnopqrstnopqrstu"),
                    new byte[]
                    {
                        0x91, 0x6f, 0x60, 0x61, 0xfe, 0x87, 0x97, 0x41,
                        0xca, 0x64, 0x69, 0xb4, 0x39, 0x71, 0xdf, 0xdb,
                        0x28, 0xb1, 0xa3, 0x2d, 0xc3, 0x6c, 0xb3, 0x25,
                        0x4e, 0x81, 0x2b, 0xe2, 0x7a, 0xad, 0x1d, 0x18
                    }
                };
#if !DEBUG
                var lottaA = new byte[1000000];
                Unsafe.InitBlock(ref lottaA[0], (byte)'a', 1000000);
                yield return new object[]
                {
                    // ReSharper disable once StringLiteralTypo
                    lottaA,
                    new byte[]
                    {
                        0x5c, 0x88, 0x75, 0xae, 0x47, 0x4a, 0x36, 0x34,
                        0xba, 0x4f, 0xd5, 0x5e, 0xc8, 0x5b, 0xff, 0xd6,
                        0x61, 0xf3, 0x2a, 0xca, 0x75, 0xc6, 0xd6, 0x99,
                        0xd0, 0xcd, 0xcb, 0x6c, 0x11, 0x58, 0x91, 0xc1
                    }
                };
                // ReSharper disable once StringLiteralTypo
                var derp = Encoding.UTF8.GetBytes("abcdefghbcdefghicdefghijdefghijkefghijklfghijklmghijklmnhijklmno");
                var derpLen = derp.Length;
                var arghLen = derpLen * 16777216;
                var argh = new byte[arghLen];
                for (var i = 0; i < arghLen; ++i)
                    argh[i] = derp[i % derpLen];
                yield return new object[]
                {
                    // ReSharper disable once StringLiteralTypo
                    argh,
                    new byte[]
                    {
                        0xec, 0xbb, 0xc4, 0x2c, 0xbf, 0x29, 0x66, 0x03,
                        0xac, 0xb2, 0xc6, 0xbc, 0x04, 0x10, 0xef, 0x43,
                        0x78, 0xba, 0xfb, 0x24, 0xb7, 0x10, 0x35, 0x7f,
                        0x12, 0xdf, 0x60, 0x77, 0x58, 0xb3, 0x3e, 0x2b
                    }
                };
#endif
            }
        }

        [TestCaseSource(nameof(Sha3_256Source))]
        public static void Sha3_256(byte[] message, byte[] digest)
        {

            var actual = new byte[32];

            var success = Xkcp.Sha3_256(actual, message);

            Assert.IsTrue(success);

            var expected = (ReadOnlySpan<byte>)digest;

            var expectedHex = Convert.ToHexString(expected);
            var actualHex = Convert.ToHexString(actual);

            Assert.IsTrue(expected.SequenceEqual((Span<byte>)actual));
        }
    }
}
