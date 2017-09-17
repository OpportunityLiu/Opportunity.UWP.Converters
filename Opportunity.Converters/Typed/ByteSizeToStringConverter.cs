using System;
using Windows.UI.Xaml;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Convert a <see cref="long"/> value that presents a byte size to a <see cref="string"/>.
    /// <example>
    /// <para>
    /// <code>1000d => "1.000 KB"</code>; 
    /// <code>1024d => "1.000 KiB"</code>
    /// </para>
    /// </example>
    /// </summary>
    public sealed class ByteSizeToStringConverter : ValueConverter<long, string>
    {
        private static readonly string[] unitsMetric = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private static readonly string[] unitsBinary = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };

        /// <summary>
        /// Unit prefix used for convertion, the default value is <see cref="UnitPrefix.Binary"/>.
        /// </summary>
        public UnitPrefix UnitPrefix
        {
            get => (UnitPrefix)GetValue(UnitPrefixProperty);
            set => SetValue(UnitPrefixProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="UnitPrefix"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UnitPrefixProperty =
            DependencyProperty.Register(nameof(UnitPrefix), typeof(UnitPrefix), typeof(ByteSizeToStringConverter), new PropertyMetadata(UnitPrefix.Binary));

        /// <summary>
        /// Return <see cref="string"/> if the <see cref="long"/> value is too big or less thah 0.
        /// </summary>
        public string OutOfRangeValue
        {
            get => (string)GetValue(OutOfRangeValueProperty);
            set => SetValue(OutOfRangeValueProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="OutOfRangeValue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OutOfRangeValueProperty =
            DependencyProperty.Register("OutOfRangeValue", typeof(string), typeof(ByteSizeToStringConverter), new PropertyMetadata("???"));

        /// <inheritdoc />
        public override string Convert(long value, object parameter, string language)
        {
            try
            {
                return ByteSizeToString(value, this.UnitPrefix);
            }
            catch (ArgumentOutOfRangeException)
            {
                return this.OutOfRangeValue ?? "???";
            }
        }

        /// <inheritdoc />
        public override long ConvertBack(string value, object parameter, string language)
        {
            return StringToByteSize(value, this.UnitPrefix);
        }

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

        private static string sizeFormat = "0" + System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + "000";

        /// <summary>
        /// Convert a byte size to its <see cref="string"/> representation.
        /// </summary>
        /// <param name="size">The byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="Typed.UnitPrefix"/> used.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string ByteSizeToString(long size, UnitPrefix unitPrefix)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));
            getUnits(out var units, out var powerBase, unitPrefix);
            foreach (var unit in units)
            {
                if (size < 1000)
                {
                    return $"{size.ToString(sizeFormat).Substring(0, 5)} {unit}";
                }
                size /= powerBase;
            }
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="Typed.UnitPrefix"/> used.</param>
        /// <returns>The byte size.</returns>
        public static long StringToByteSize(string sizeStr, UnitPrefix unitPrefix)
        {
            if (TryStringToByteSize(sizeStr, unitPrefix, out var r))
                return r;
            throw new FormatException("Wrong format.");
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="Typed.UnitPrefix"/> used.</param>
        /// <param name="result">The byte size.</param>
        /// <returns>The conversion succeed or not.</returns>
        public static bool TryStringToByteSize(string sizeStr, UnitPrefix unitPrefix, out long result)
        {
            if (TryStringToByteSizeExact(sizeStr, unitPrefix, out result))
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
            if (TryStringToByteSizeExact(sizeStr, unitPrefix, out result))
                return true;
            if (long.TryParse(sizeStr, out result))
                return true;
            return false;
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="Typed.UnitPrefix"/> used.</param>
        /// <returns>The byte size.</returns>
        public static long StringToByteSizeExact(string sizeStr, UnitPrefix unitPrefix)
        {
            if (TryStringToByteSizeExact(sizeStr, unitPrefix, out var r))
                return r;
            throw new FormatException("Wrong format.");
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="long"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="Typed.UnitPrefix"/> used.</param>
        /// <param name="result">The byte size.</param>
        /// <returns>The conversion succeed or not.</returns>
        public static bool TryStringToByteSizeExact(string sizeStr, UnitPrefix unitPrefix, out long result)
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
            result = default(long);
            return false;
        }
    }

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
}
