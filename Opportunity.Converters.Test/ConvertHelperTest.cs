using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
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
        [UITestMethod]
        public void ConvertNullToValue()
        {
            Assert.IsNull(ConvertHelper.ChangeType<string>(null));
            Assert.IsNull(ConvertHelper.ChangeType<int?>(null));
            Assert.AreEqual(0, ConvertHelper.ChangeType<int>(null));
            Assert.AreEqual(false, ConvertHelper.ChangeType<bool>(null));
            Assert.AreEqual(null, ConvertHelper.ChangeType<bool?>(null));
            Assert.AreEqual(ByteEnum.Zero, ConvertHelper.ChangeType<ByteEnum>(null));
            Assert.AreEqual(null, ConvertHelper.ChangeType<ByteEnum?>(null));
            Assert.AreEqual(default(DateTime), ConvertHelper.ChangeType<DateTime>(null));
            Assert.AreEqual(null, ConvertHelper.ChangeType<DateTime?>(null));
        }

        [UITestMethod]
        public void ConvertToInt()
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
        }

        [UITestMethod]
        public void ConvertToByte()
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
        }

        [UITestMethod]
        public void ConvertToDouble()
        {
            Assert.AreEqual(1d, ConvertHelper.ChangeType<double>((StringComparison)1), "enum");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>(123.0), "double");
            Assert.AreEqual(123.1d, ConvertHelper.ChangeType<double>(123.1), "double + 0.1");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>(123U), "uint");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>(123UL), "ulong");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>(123L), "long");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>((byte)123), "byte");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>((sbyte)123), "sbyte");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>(123), "int");
            Assert.AreEqual(123d, ConvertHelper.ChangeType<double>("123"), "string");
            Assert.AreEqual(123.0d, ConvertHelper.ChangeType<double>("123.0"), "string + '.0'");
            Assert.AreEqual(123.1d, ConvertHelper.ChangeType<double>("123.1"), "string + 0.1");
        }

        public enum IntEnum : int
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Four = 4
        }

        [UITestMethod]
        public void ConvertToIntEnum()
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

        [UITestMethod]
        public void ConvertToByteEnum()
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
        }
    }
}
