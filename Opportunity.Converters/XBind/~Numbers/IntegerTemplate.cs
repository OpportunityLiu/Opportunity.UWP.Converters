﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Method of <see cref="sbyte"/> conversion.
    /// </summary>
    public static partial class SByte
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static sbyte Increase(sbyte value) => (sbyte)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static sbyte Decrease(sbyte value) => (sbyte)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(sbyte value, sbyte addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(sbyte value, sbyte subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(sbyte value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(sbyte value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(sbyte value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(sbyte value, sbyte test) => value == test;
    }

    /// <summary>
    /// Method of <see cref="byte"/> conversion.
    /// </summary>
    public static partial class Byte
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static byte Increase(byte value) => (byte)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static byte Decrease(byte value) => (byte)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(byte value, sbyte addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(byte value, sbyte subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(byte value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(byte value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(byte value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(byte value, byte test) => value == test;
    }

    public static partial class SByte
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static sbyte Add(sbyte value, sbyte addition) => (sbyte)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static sbyte Subtract(sbyte value, sbyte subtraction) => (sbyte)(value - subtraction);
    }

    public static partial class Byte
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static byte Add(byte value, sbyte addition)
        {
            if (addition >= 0)
                return (byte)(value + (byte)addition);
            else
                return (byte)(value - (byte)(-addition));
        }
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static byte Subtract(byte value, sbyte subtraction)
        {
            if (subtraction >= 0)
                return (byte)(value - (byte)subtraction);
            else
                return (byte)(value + (byte)(-subtraction));
        }
    }
    /// <summary>
    /// Method of <see cref="short"/> conversion.
    /// </summary>
    public static partial class Int16
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static short Increase(short value) => (short)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static short Decrease(short value) => (short)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(short value, short addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(short value, short subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(short value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(short value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(short value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(short value, short test) => value == test;
    }

    /// <summary>
    /// Method of <see cref="ushort"/> conversion.
    /// </summary>
    public static partial class UInt16
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static ushort Increase(ushort value) => (ushort)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static ushort Decrease(ushort value) => (ushort)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(ushort value, short addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(ushort value, short subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(ushort value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(ushort value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(ushort value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(ushort value, ushort test) => value == test;
    }

    public static partial class Int16
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static short Add(short value, short addition) => (short)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static short Subtract(short value, short subtraction) => (short)(value - subtraction);
    }

    public static partial class UInt16
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static ushort Add(ushort value, short addition)
        {
            if (addition >= 0)
                return (ushort)(value + (ushort)addition);
            else
                return (ushort)(value - (ushort)(-addition));
        }
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static ushort Subtract(ushort value, short subtraction)
        {
            if (subtraction >= 0)
                return (ushort)(value - (ushort)subtraction);
            else
                return (ushort)(value + (ushort)(-subtraction));
        }
    }
    /// <summary>
    /// Method of <see cref="int"/> conversion.
    /// </summary>
    public static partial class Int32
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static int Increase(int value) => (int)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static int Decrease(int value) => (int)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(int value, int addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(int value, int subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(int value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(int value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(int value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(int value, int test) => value == test;
    }

    /// <summary>
    /// Method of <see cref="uint"/> conversion.
    /// </summary>
    public static partial class UInt32
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static uint Increase(uint value) => (uint)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static uint Decrease(uint value) => (uint)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(uint value, int addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(uint value, int subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(uint value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(uint value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(uint value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(uint value, uint test) => value == test;
    }

    public static partial class Int32
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static int Add(int value, int addition) => (int)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static int Subtract(int value, int subtraction) => (int)(value - subtraction);
    }

    public static partial class UInt32
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static uint Add(uint value, int addition)
        {
            if (addition >= 0)
                return (uint)(value + (uint)addition);
            else
                return (uint)(value - (uint)(-addition));
        }
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static uint Subtract(uint value, int subtraction)
        {
            if (subtraction >= 0)
                return (uint)(value - (uint)subtraction);
            else
                return (uint)(value + (uint)(-subtraction));
        }
    }
    /// <summary>
    /// Method of <see cref="long"/> conversion.
    /// </summary>
    public static partial class Int64
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static long Increase(long value) => (long)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static long Decrease(long value) => (long)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(long value, long addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(long value, long subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(long value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(long value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(long value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(long value, long test) => value == test;
    }

    /// <summary>
    /// Method of <see cref="ulong"/> conversion.
    /// </summary>
    public static partial class UInt64
    {
        /// <summary>
        /// Add 1 to <paramref name="value"/>.
        /// </summary>
        public static ulong Increase(ulong value) => (ulong)(value + 1);
        /// <summary>
        /// Subtract 1 from <paramref name="value"/>.
        /// </summary>
        public static ulong Decrease(ulong value) => (ulong)(value - 1);

        
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/> and to string.
        /// </summary>
        public static string AddToString(ulong value, long addition) => Add(value, addition).ToString();
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/> and to string.
        /// </summary>
        public static string SubtractToString(ulong value, long subtraction) => Subtract(value, subtraction).ToString();
        /// <summary>
        /// Add 1 to <paramref name="value"/> and to string.
        /// </summary>
        public static string IncreaseToString(ulong value) => Increase(value).ToString();
        /// <summary>
        /// Subtract 1 from <paramref name="value"/> and to string.
        /// </summary>
        public static string DecreaseToString(ulong value) => Decrease(value).ToString();

        /// <summary>
        /// Test <paramref name="value"/> is 0 or not.
        /// </summary>
        public static bool IsZero(ulong value) => value == 0;
        /// <summary>
        /// Test <paramref name="value"/> is <paramref name="test"/> or not.
        /// </summary>
        public static bool IsValue(ulong value, ulong test) => value == test;
    }

    public static partial class Int64
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static long Add(long value, long addition) => (long)(value + addition);
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static long Subtract(long value, long subtraction) => (long)(value - subtraction);
    }

    public static partial class UInt64
    {
        /// <summary>
        /// Add <paramref name="addition"/> to <paramref name="value"/>.
        /// </summary>
        public static ulong Add(ulong value, long addition)
        {
            if (addition >= 0)
                return (ulong)(value + (ulong)addition);
            else
                return (ulong)(value - (ulong)(-addition));
        }
        /// <summary>
        /// Subtract <paramref name="subtraction"/> from <paramref name="value"/>.
        /// </summary>
        public static ulong Subtract(ulong value, long subtraction)
        {
            if (subtraction >= 0)
                return (ulong)(value - (ulong)subtraction);
            else
                return (ulong)(value + (ulong)(-subtraction));
        }
    }
}
