using Opportunity.UWP.Converters.XBind;
using System;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
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
                return ByteSize.ToString(value, this.UnitPrefix);
            }
            catch (ArgumentOutOfRangeException)
            {
                return this.OutOfRangeValue ?? "???";
            }
        }

        /// <inheritdoc />
        public override long ConvertBack(string value, object parameter, string language)
        {
            return ByteSize.Parse(value, this.UnitPrefix);
        }
    }
}
