using System;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert a <see cref="double"/> value that presents a byte size to a <see cref="string"/>.
    /// </summary>
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// <c>1000d => "1.000 KB"</c>
    /// <c>1024d => "1.000 KiB"</c>
    /// </item>
    /// </list>
    /// </example>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(InnerConverter))]
    public class ByteSizeToStringConverter : ChainConverter
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
        /// Return <see cref="string"/> if the <see cref="double"/> value is too big or not a number.
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
            DependencyProperty.Register("OutOfRangeValue", typeof(string), typeof(ByteSizeToStringConverter), new PropertyMetadata("???", OutOfRangeValuePropertyChangedCallback));

        private static void OutOfRangeValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue == null)
                throw new ArgumentNullException(nameof(OutOfRangeValue));
        }
        
        /// <inheritdoc />
        protected override object ConvertImpl(object value, Type targetType, object parameter, string language)
        {
            var size = System.Convert.ToDouble(value);
            try
            {
                return ByteSizeToString(size, this.UnitPrefix);
            }
            catch(ArgumentException)
            {
                return this.OutOfRangeValue;
            }
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, Type targetType, object parameter, string language)
        {
            var sizeStr = value.ToString();
            try
            {
                return StringToByteSize(sizeStr, this.UnitPrefix);
            }
            catch(Exception)
            {
                return DependencyProperty.UnsetValue;
            }
        }

        private static void getUnits(out string[] units, out double powerBase, UnitPrefix unitPrefix)
        {
            if(unitPrefix == UnitPrefix.Metric)
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
        /// <param name="unitPrefix"><see cref="Converters.UnitPrefix"/> used.</param>
        /// <returns>The <see cref="string"/> representation of byte size.</returns>
        public static string ByteSizeToString(double size, UnitPrefix unitPrefix)
        {
            if(size < 0 || double.IsNaN(size))
                throw new ArgumentOutOfRangeException(nameof(size));
            getUnits(out var units, out var powerBase, unitPrefix);
            foreach(var unit in units)
            {
                if(size < 1000)
                {
                    return $"{size.ToString(sizeFormat).Substring(0, 5)} {unit}";
                }
                size /= powerBase;
            }
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        /// <summary>
        /// Convert a <see cref="string"/> representation of byte size to a <see cref="double"/>.
        /// </summary>
        /// <param name="sizeStr">The <see cref="string"/> representation of byte size to convert.</param>
        /// <param name="unitPrefix"><see cref="Converters.UnitPrefix"/> used.</param>
        /// <returns>The byte size.</returns>
        public static double StringToByteSize(string sizeStr, UnitPrefix unitPrefix)
        {
            if(string.IsNullOrEmpty(sizeStr))
                throw new ArgumentNullException(nameof(sizeStr));
            sizeStr = sizeStr.Trim();
            getUnits(out var units, out var powerBase, unitPrefix);
            for(var i = 0; i < units.Length; i++)
            {
                if(sizeStr.EndsWith(units[i], StringComparison.OrdinalIgnoreCase))
                {
                    var sizeNumStr = sizeStr.Substring(0, sizeStr.Length - units[i].Length);
                    var sizeNum = double.Parse(sizeNumStr);
                    return sizeNum * Math.Pow(powerBase, i);
                }
            }
            throw new FormatException("Wrong format.");
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
