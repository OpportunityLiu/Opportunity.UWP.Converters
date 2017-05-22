﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    public sealed class NumberToBooleanConverter : ChainConverter
    {
        /// <summary>
        /// Start value of number range.
        /// </summary>
        public double RangeStart
        {
            get => (double)GetValue(RangeStartProperty); set => SetValue(RangeStartProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="RangeStart"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RangeStartProperty =
            DependencyProperty.Register("RangeStart", typeof(double), typeof(NumberToBooleanConverter), new PropertyMetadata(0));

        /// <summary>
        /// End value of number range.
        /// </summary>
        public double RangeEnd
        {
            get => (double)GetValue(RangeEndProperty); set => SetValue(RangeEndProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="RangeEnd"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RangeEndProperty =
            DependencyProperty.Register("RangeEnd", typeof(double), typeof(NumberToBooleanConverter), new PropertyMetadata(0));

        /// <summary>
        /// The number range includes <see cref="RangeStart"/>.
        /// Default value is <c>true</c>.
        /// </summary>
        public bool IncludeStart
        {
            get => (bool)GetValue(IncludeStartProperty); set => SetValue(IncludeStartProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IncludeStart"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IncludeStartProperty =
            DependencyProperty.Register("IncludeStart", typeof(bool), typeof(NumberToBooleanConverter), new PropertyMetadata(true));

        /// <summary>
        /// The number range includes <see cref="RangeEnd"/>. 
        /// Default value is <c>false</c>.
        /// </summary>
        public bool IncludeEnd
        {
            get => (bool)GetValue(IncludeEndProperty); set => SetValue(IncludeEndProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IncludeEnd"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IncludeEndProperty =
            DependencyProperty.Register("IncludeEnd", typeof(bool), typeof(NumberToBooleanConverter), new PropertyMetadata(false));

        /// <summary>
        /// Values in number range will be convert to this.
        /// </summary>
        public bool InRangeResult
        {
            get => (bool)GetValue(InRangeResultProperty); set => SetValue(InRangeResultProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="InRangeResult"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InRangeResultProperty =
            DependencyProperty.Register("InRangeResult", typeof(bool), typeof(NumberToBooleanConverter), new PropertyMetadata(true));

        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            var val = Internal.ConvertHelper.ChangeType<bool>(value);
            if (val == this.InRangeResult)
                return (RangeStart + RangeEnd) / 2;
            else
                return RangeStart - 1;
        }

        protected override object ConvertImpl(object value, object parameter, string language)
        {
            var val = Internal.ConvertHelper.ChangeType<double>(value);
            if (val < RangeStart)
                return !InRangeResult;
            if (val > RangeEnd)
                return !InRangeResult;
            if (val == RangeStart)
            {
                if (IncludeStart)
                    return InRangeResult;
                else
                    return !InRangeResult;
            }
            if (val == RangeEnd)
            {
                if (IncludeEnd)
                    return InRangeResult;
                else
                    return !InRangeResult;
            }
            return InRangeResult;
        }
    }
}
