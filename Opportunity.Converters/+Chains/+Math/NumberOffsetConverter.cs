using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    /// <summary>
    /// Apply an <see cref="Offset"/> to values.
    /// </summary>
    public abstract class NumberOffsetConverter<T> : ChainConverter<T, T>
    {
        /// <summary>
        /// Apply <see cref="Offset"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract T ApplyOffset(T value);
        /// <summary>
        /// Unapply <see cref="Offset"/> to <paramref name="value"/>.
        /// </summary>
        protected abstract T UnapplyOffset(T value);

        /// <summary>
        /// The offset value used.
        /// </summary>
        public T Offset
        {
            get => (T)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Offset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(T), typeof(NumberOffsetConverter<T>), new PropertyMetadata(default(T)));

        /// <summary>
        /// Apply <see cref="Offset"/>.
        /// </summary>
        /// <param name="value">value canbe converted to <typeparamref name="T"/>.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> + <see cref="Offset"/></returns>
        protected sealed override T ConvertImpl(T value, object parameter, string language)
        {
            return ApplyOffset(value);
        }

        /// <summary>
        /// Unapply <see cref="Offset"/>.
        /// </summary>
        /// <param name="value">value canbe converted to <typeparamref name="T"/>.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> - <see cref="Offset"/></returns>
        protected sealed override T ConvertBackImpl(T value, object parameter, string language)
        {
            return UnapplyOffset(value);
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberOffsetConverter{T}.Offset"/> to values.
    /// </summary>
    public sealed class DoubleOffsetConverter : NumberOffsetConverter<double>
    {
        /// <inhertdoc />
        protected override double ApplyOffset(double value)
        {
            return value + Offset;
        }

        /// <inhertdoc />
        protected override double UnapplyOffset(double value)
        {
            return value - Offset;
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberOffsetConverter{T}.Offset"/> to values.
    /// </summary>
    public abstract class IntegerOffsetConverter<T> : NumberOffsetConverter<T>
    {
        /// <summary>
        /// Check overflow and underflow.
        /// </summary>
        public bool CheckOverflows
        {
            get => (bool)GetValue(CheckOverflowsProperty);
            set => SetValue(CheckOverflowsProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="CheckOverflows"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CheckOverflowsProperty =
            DependencyProperty.Register("CheckOverflows", typeof(bool), typeof(IntegerOffsetConverter<T>), new PropertyMetadata(false));
    }

    /// <summary>
    /// Apply an <see cref="NumberOffsetConverter{T}.Offset"/> to values.
    /// </summary>
    public sealed class Int32OffsetConverter : IntegerOffsetConverter<int>
    {
        /// <inhertdoc />
        protected override int ApplyOffset(int value)
        {
            if (CheckOverflows)
                return checked(value + Offset);
            else
                return unchecked(value + Offset);
        }

        /// <inhertdoc />
        protected override int UnapplyOffset(int value)
        {
            if (CheckOverflows)
                return checked(value - Offset);
            else
                return unchecked(value - Offset);
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberOffsetConverter{T}.Offset"/> to values.
    /// </summary>
    public sealed class UInt32OffsetConverter : IntegerOffsetConverter<uint>
    {
        /// <inhertdoc />
        protected override uint ApplyOffset(uint value)
        {
            if (CheckOverflows)
                return checked(value + Offset);
            else
                return unchecked(value + Offset);
        }

        /// <inhertdoc />
        protected override uint UnapplyOffset(uint value)
        {
            if (CheckOverflows)
                return checked(value - Offset);
            else
                return unchecked(value - Offset);
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberOffsetConverter{T}.Offset"/> to values.
    /// </summary>
    public sealed class Int64OffsetConverter : IntegerOffsetConverter<long>
    {
        /// <inhertdoc />
        protected override long ApplyOffset(long value)
        {
            if (CheckOverflows)
                return checked(value + Offset);
            else
                return unchecked(value + Offset);
        }

        /// <inhertdoc />
        protected override long UnapplyOffset(long value)
        {
            if (CheckOverflows)
                return checked(value - Offset);
            else
                return unchecked(value - Offset);
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberOffsetConverter{T}.Offset"/> to values.
    /// </summary>
    public sealed class UInt64OffsetConverter : IntegerOffsetConverter<ulong>
    {
        /// <inhertdoc />
        protected override ulong ApplyOffset(ulong value)
        {
            if (CheckOverflows)
                return checked(value + Offset);
            else
                return unchecked(value + Offset);
        }

        /// <inhertdoc />
        protected override ulong UnapplyOffset(ulong value)
        {
            if (CheckOverflows)
                return checked(value - Offset);
            else
                return unchecked(value - Offset);
        }
    }
}
