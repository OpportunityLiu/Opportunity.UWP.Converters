using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opportunity.Converters.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Markup;

namespace Opportunity.Converters.Test
{
    [TestClass]
    public class ConvertHelperTest
    {
        [TestMethod]
        public async Task ConvertNullToValue()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                Assert.IsNull(ConvertHelper.ChangeType<string>(null));
                Assert.IsNull(ConvertHelper.ChangeType<int?>(null));
                Assert.ThrowsException<InvalidCastException>(() => ConvertHelper.ChangeType<int>(null));
                Assert.IsNull(ConvertHelper.ChangeType(null, typeof(string)));
                Assert.IsNull(ConvertHelper.ChangeType(null, typeof(int?)));
                Assert.ThrowsException<InvalidCastException>(() => ConvertHelper.ChangeType(null, typeof(int)));
            });
        }

        [TestMethod]
        public async Task ConvertToInt()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                Assert.AreEqual(1, ConvertHelper.ChangeType<int>((StringComparison)1), "enum");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>(123.0), "double");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>(123.1), "double + 0.1");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>(123U), "uint");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>(123UL), "ulong");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>(123L), "long");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>((byte)123), "byte");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>((sbyte)123), "sbyte");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>(123), "int");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>("123"), "string");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>("123.0"), "string + '.0'");
                Assert.AreEqual(123, ConvertHelper.ChangeType<int>("123.1"), "string + 0.1");
            });
        }

        [TestMethod]
        public async Task ConvertToByte()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                Assert.AreEqual((byte)1, ConvertHelper.ChangeType<byte>((StringComparison)1), "enum");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>(123.0), "double");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>(123.1), "double + 0.1");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>(123U), "uint");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>(123UL), "ulong");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>(123L), "long");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>((byte)123), "byte");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>((sbyte)123), "sbyte");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>(123), "int");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>("123"), "string");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>("123.0"), "string + '.0'");
                Assert.AreEqual((byte)123, ConvertHelper.ChangeType<byte>("123.1"), "string + 0.1");
            });
        }

        public enum IntEnum : int
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Four = 4
        }

        [TestMethod]
        public async Task ConvertToIntEnum()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(IntEnum.Four), "enum");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>((StringComparison)4), "another enum");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(ByteEnum.Four), "another byte enum");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(4.0), "double");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(4.1), "double + 0.1");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(4U), "uint");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(4UL), "ulong");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(4L), "long");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>((byte)4), "byte");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>((sbyte)4), "sbyte");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(4), "int");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>("Four"), "string");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>("four"), "string ignore case");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>(" Four "), "string with empty");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>("4"), "string number");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>("4.0"), "string number + '.0'");
                Assert.AreEqual(IntEnum.Four, ConvertHelper.ChangeType<IntEnum>("4.1"), "string number + 0.1");
            });
        }

        public enum ByteEnum : byte
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Four = 4
        }

        public enum AnotherByteEnum : byte
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Four = 4
        }

        [TestMethod]
        public async Task ConvertToByteEnum()
        {
            await TestHelper.RunAtUIThread(() =>
            {
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(ByteEnum.Four), "enum");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(AnotherByteEnum.Four), "another enum");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(IntEnum.Four), "another int enum");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(4.0), "double");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(4.1), "double + 0.1");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(4U), "uint");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(4UL), "ulong");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(4L), "long");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>((byte)4), "byte");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>((sbyte)4), "sbyte");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(4), "int");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>("Four"), "string");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>("four"), "string ignore case");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>(" Four "), "string with empty");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>("4"), "string number");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>("4.0"), "string number + '.0'");
                Assert.AreEqual(ByteEnum.Four, ConvertHelper.ChangeType<ByteEnum>("4.1"), "string number + 0.1");
            });
        }
    }
}
