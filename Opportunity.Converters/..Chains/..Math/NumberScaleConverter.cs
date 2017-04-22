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
    /// Apply an <see cref="Scale"/> to values.
    /// </summary>
    public abstract class NumberScaleConverter<T> : ChainConverter
    {
        /// <summary>
        /// Apply <see cref="Scale"/> to <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract T ApplyScale(T value);
        /// <summary>
        /// Unapply <see cref="Scale"/> to <paramref name="value"/>.
        /// </summary>
        protected abstract T UnapplyScale(T value);

        /// <summary>
        /// The Scale value used.
        /// </summary>
        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="Scale"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(NumberScaleConverter<T>), new PropertyMetadata(1d));

        /// <summary>
        /// Apply <see cref="Scale"/>.
        /// </summary>
        /// <param name="value">value canbe converted to <typeparamref name="T"/>.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> + <see cref="Scale"/></returns>
        protected sealed override object ConvertImpl(object value,  object parameter, string language)
        {
            return ApplyScale(ChangeType<T>(value));
        }

        /// <summary>
        /// Unapply <see cref="Scale"/>.
        /// </summary>
        /// <param name="value">value canbe converted to <typeparamref name="T"/>.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="language">Not used.</param>
        /// <returns><paramref name="value"/> - <see cref="Scale"/></returns>
        protected sealed override object ConvertBackImpl(object value,  object parameter, string language)
        {
            return UnapplyScale(ChangeType<T>(value));
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberScaleConverter{T}.Scale"/> to values.
    /// </summary>
    public sealed class DoubleScaleConverter : NumberScaleConverter<double>
    {
        /// <inhertdoc />
        protected override double ApplyScale(double value)
        {
            return value * Scale;
        }

        /// <inhertdoc />
        protected override double UnapplyScale(double value)
        {
            return value / Scale;
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberScaleConverter{T}.Scale"/> to values.
    /// </summary>
    public abstract class IntegerScaleConverter<T> : NumberScaleConverter<T>
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
            DependencyProperty.Register("CheckOverflows", typeof(bool), typeof(IntegerScaleConverter<T>), new PropertyMetadata(false));
    }

    /// <summary>
    /// Apply an <see cref="NumberScaleConverter{T}.Scale"/> to values.
    /// </summary>
    public sealed class Int32ScaleConverter : IntegerScaleConverter<int>
    {
        /// <inhertdoc />
        protected override int ApplyScale(int value)
        {
            if (CheckOverflows)
                return checked((int)(value * Scale));
            else
                return unchecked((int)(value * Scale));
        }

        /// <inhertdoc />
        protected override int UnapplyScale(int value)
        {
            if (CheckOverflows)
                return checked((int)(value / Scale));
            else
                return unchecked((int)(value / Scale));
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberScaleConverter{T}.Scale"/> to values.
    /// </summary>
    public sealed class UInt32ScaleConverter : IntegerScaleConverter<uint>
    {
        /// <inhertdoc />
        protected override uint ApplyScale(uint value)
        {
            if (CheckOverflows)
                return checked((uint)(value * Scale));
            else
                return unchecked((uint)(value * Scale));
        }

        /// <inhertdoc />
        protected override uint UnapplyScale(uint value)
        {
            if (CheckOverflows)
                return checked((uint)(value / Scale));
            else
                return unchecked((uint)(value / Scale));
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberScaleConverter{T}.Scale"/> to values.
    /// </summary>
    public sealed class Int64ScaleConverter : IntegerScaleConverter<long>
    {
        /// <inhertdoc />
        protected override long ApplyScale(long value)
        {
            if (CheckOverflows)
                return checked((long)(value * Scale));
            else
                return unchecked((long)(value * Scale));
        }

        /// <inhertdoc />
        protected override long UnapplyScale(long value)
        {
            if (CheckOverflows)
                return checked((long)(value / Scale));
            else
                return unchecked((long)(value / Scale));
        }
    }

    /// <summary>
    /// Apply an <see cref="NumberScaleConverter{T}.Scale"/> to values.
    /// </summary>
    public sealed class UInt64ScaleConverter : IntegerScaleConverter<ulong>
    {
        /// <inhertdoc />
        protected override ulong ApplyScale(ulong value)
        {
            if (CheckOverflows)
                return checked((ulong)(value * Scale));
            else
                return unchecked((ulong)(value * Scale));
        }

        /// <inhertdoc />
        protected override ulong UnapplyScale(ulong value)
        {
            if (CheckOverflows)
                return checked((ulong)(value / Scale));
            else
                return unchecked((ulong)(value / Scale));
        }
    }
}
