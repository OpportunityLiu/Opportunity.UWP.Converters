using System;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="Enum"/>s to <see cref="string"/> values by <see cref="EnumExtension.ToFriendlyNameString{T}(T, Func{T, string})"/>.
    /// </summary>]
    public sealed class EnumToStringConverter : ValueConverter<Enum, string>
    {
        /// <summary>
        /// Default value of <see cref="NameProvider"/>, uses <see cref="object.ToString()"/>.
        /// </summary>
        public static Func<Enum, string> DefaultNameProvider { get; } = v => v.ToString();

        /// <summary>
        /// Name provider provides names of defined enum values.
        /// </summary>
        public Func<Enum, string> NameProvider
        {
            get => (Func<Enum, string>)GetValue(NameProviderProperty);
            set => SetValue(NameProviderProperty, value);
        }

        /// <summary>
        /// Indentify <see cref="NameProvider"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NameProviderProperty =
            DependencyProperty.Register(nameof(NameProvider), typeof(Func<Enum, string>), typeof(EnumToStringConverter), new PropertyMetadata(DefaultNameProvider));


        /// <inheritdoc />
        public override string Convert(Enum value, object parameter, string language)
        {
            if (value is null)
                return "";
            return EnumExtension.ToFriendlyNameString(value, NameProvider);
        }

        /// <summary>Not implemented.</summary>
        /// <exception cref="NotImplementedException">Not implemented.</exception>
        public override Enum ConvertBack(string value, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
