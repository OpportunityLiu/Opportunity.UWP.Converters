﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Convert from <see cref="bool"/> to <see cref="Visibility"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : ValueConverter<bool, Visibility>
    {
        /// <summary>
        /// Convert <c>true</c> to <see cref="Visibility.Visible"/>.
        /// The default value is <c>true</c>.
        /// If set to <c>false</c>, will convert <c>true</c> to <see cref="Visibility.Collapsed"/>.
        /// </summary>
        public bool TrueForVisible
        {
            get => (bool)GetValue(TrueForVisibleProperty);
            set => SetValue(TrueForVisibleProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="TrueForVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TrueForVisibleProperty =
            DependencyProperty.Register("TrueForVisible", typeof(bool), typeof(BooleanToVisibilityConverter), new PropertyMetadata(true));

        /// <inheritdoc />
        public override Visibility Convert(bool value, object parameter, string language)
        {
            if (TrueForVisible)
                value = !value;
            if (value)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        /// <inheritdoc />
        public override bool ConvertBack(Visibility value, object parameter, string language)
        {
            if (TrueForVisible)
                return value == Visibility.Visible;
            else
                return value != Visibility.Visible;
        }
    }
}
