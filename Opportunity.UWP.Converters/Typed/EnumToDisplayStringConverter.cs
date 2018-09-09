using System;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert <see cref="Enum"/>s to <see cref="string"/> values by <see cref="EnumDisplayExtension.ToDisplayNameString{T}(T)"/>.
    /// </summary>]
    public sealed class EnumToDisplayStringConverter : ValueConverter<Enum, string>
    {
        /// <inheritdoc />
        public override string Convert(Enum value, object parameter, string language)
        {
            if (value is null)
                return "";
            return EnumDisplayExtension.ToDisplayNameString(value);
        }

        /// <summary>Not implemented.</summary>
        /// <exception cref="NotImplementedException">Not implemented.</exception>
        public override Enum ConvertBack(string value, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
