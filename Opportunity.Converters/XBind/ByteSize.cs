using Opportunity.Converters.Typed;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Unit prefix of byte size values.
    /// </summary>
    public enum UnitPrefix
    {
        /// <summary>
        /// Binary prefixes such as <c>KiB</c>, base 1024.
        /// </summary>
        Binary,
        /// <summary>
        /// Metric prefixes such as <c>KB</c>, base 1000.
        /// </summary>
        Metric
    }

    /// <summary>
    /// Convert byte size value to its string representation.
    /// </summary>
    public static class ByteSize
    {
        private static readonly string[] unitsMetric = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private static readonly string[] unitsBinary = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };


        private static void getUnits(out string[] units, out int powerBase, UnitPrefix unitPrefix)
        {
            if (unitPrefix == UnitPrefix.Metric)
            {
                units = unitsMetric;
                powerBase = 1000;
            }
            else
            {
                units = unitsBinary;
                powerBase = 1024;
            }
        }

        /// <summary>
        /// Convert a byte size to its <see cref="string"/> representation.
        /// </summary>
        /// <param name="size">The byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="UnitPrefix"/> used.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string OfByteSize(long size, UnitPrefix unitPrefix)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            getUnits(out var units, out var powerBase, unitPrefix);
            if (size < 1000)
                return size.ToString() + " " + units[0];
            var p = (double)size;
            foreach (var unit in units)
            {
                if (p < 10)
                {
                    var pStr = p.ToString("0.0##", CultureInfo.CurrentUICulture);
                    return pStr + " " + unit;
                }
                else if (p < 100)
                {
                    var pStr = p.ToString("00.0#", CultureInfo.CurrentUICulture);
                    return pStr + " " + unit;
                }
                else if (p < 1000)
                {
                    var pStr = p.ToString("000.0", CultureInfo.CurrentUICulture);
                    return pStr + " " + unit;
                }
                p /= powerBase;
            }
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="UnitPrefix"/> used.</param>
        /// <returns>The byte size.</returns>
        public static long ToByteSize(string sizeStr, UnitPrefix unitPrefix)
        {
            if (TryToByteSize(sizeStr, unitPrefix, out var r))
                return r;
            throw new FormatException("Wrong format.");
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="UnitPrefix"/> used.</param>
        /// <param name="result">The byte size.</param>
        /// <returns>The conversion succeed or not.</returns>
        public static bool TryToByteSize(string sizeStr, UnitPrefix unitPrefix, out long result)
        {
            if (TryToByteSizeExact(sizeStr, unitPrefix, out result))
                return true;
            switch (unitPrefix)
            {
            case UnitPrefix.Metric:
                unitPrefix = UnitPrefix.Binary;
                break;
            case UnitPrefix.Binary:
            default:
                unitPrefix = UnitPrefix.Metric;
                break;
            }
            if (TryToByteSizeExact(sizeStr, unitPrefix, out result))
                return true;
            if (long.TryParse(sizeStr, out result))
                return true;
            return false;
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="UnitPrefix"/> used.</param>
        /// <returns>The byte size.</returns>
        public static long ToByteSizeExact(string sizeStr, UnitPrefix unitPrefix)
        {
            if (TryToByteSizeExact(sizeStr, unitPrefix, out var r))
                return r;
            throw new FormatException("Wrong format.");
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="UnitPrefix"/> used.</param>
        /// <param name="result">The byte size.</param>
        /// <returns>The conversion succeed or not.</returns>
        public static bool TryToByteSizeExact(string sizeStr, UnitPrefix unitPrefix, out long result)
        {
            if (string.IsNullOrEmpty(sizeStr))
                throw new ArgumentNullException(nameof(sizeStr));
            sizeStr = sizeStr.Trim();
            getUnits(out var units, out var powerBase, unitPrefix);
            for (var i = 0; i < units.Length; i++)
            {
                if (sizeStr.EndsWith(units[i], StringComparison.OrdinalIgnoreCase))
                {
                    var sizeNumStr = sizeStr.Substring(0, sizeStr.Length - units[i].Length);
                    if (!double.TryParse(sizeNumStr, out var sizeNum))
                        continue;
                    result = (long)(sizeNum * Math.Pow(powerBase, i));
                    return true;
                }
            }
            result = default;
            return false;
        }


        /// <summary>
        /// Convert a byte size to its <see cref="string"/> representation.
        /// </summary>
        /// <param name="size">The byte size to convert.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string OfByteSizeBinary(long size)
            => OfByteSize(size, UnitPrefix.Binary);

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <returns>The byte size.</returns>
        public static long ToByteSizeBinary(string sizeStr)
            => ToByteSize(sizeStr, UnitPrefix.Binary);
        /// <summary>
        /// Convert a byte size to its <see cref="string"/> representation.
        /// </summary>
        /// <param name="size">The byte size to convert.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string OfByteSizeMetric(long size)
            => OfByteSize(size, UnitPrefix.Metric);

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <returns>The byte size.</returns>
        public static long ToByteSizeMetric(string sizeStr)
            => ToByteSize(sizeStr, UnitPrefix.Metric);
    }
}
