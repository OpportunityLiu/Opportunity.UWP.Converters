using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Opportunity.Converters.Command
{
    /// <summary>
    /// Limit the frequency of executing a command by specified delay.
    /// </summary>
    public class ThrottleConverter : ValueConverter<ICommand, ICommand>
    {
        /// <summary>
        /// Default delay time in millisecond, if not sepecified by converter parameter.
        /// </summary>
        public int DefaultDelay
        {
            get => (int)GetValue(DefaultDelayProperty);
            set => SetValue(DefaultDelayProperty, value);
        }

        /// <summary>
        /// Indentify <see cref="DefaultDelay"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DefaultDelayProperty =
            DependencyProperty.Register(nameof(DefaultDelay), typeof(int), typeof(DelayConverter), new PropertyMetadata(500, DefaultDelayPropertyChanged));

        private static void DefaultDelayPropertyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = (int)e.OldValue;
            var newValue = (int)e.NewValue;
            if (oldValue == newValue)
                return;
            if (newValue < 0)
                throw new ArgumentException("Must be non-negetive number.", nameof(DefaultDelay));
        }

        /// <summary>
        /// Limit the frequency of executing a command by specified delay.
        /// </summary>
        /// <param name="value">The command to execute</param>
        /// <param name="parameter">Delay time in millisecond</param>
        /// <param name="language">Not used</param>
        /// <returns>A command, execute the <paramref name="value"/> after delay of <paramref name="parameter"/> ms, if <see cref="ICommand.Execute(object)"/> is not called again in this period.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="parameter"/> is less than 0.</exception>
        public override ICommand Convert(ICommand value, object parameter, string language)
        {
            var delay = -1;
            if (parameter == null)
                delay = DefaultDelay;
            else if (!Internal.ConvertHelper.TryChangeType(parameter, out delay))
                delay = DefaultDelay;
            return XBind.Command.Throttle(value, delay);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <exception cref="NotImplementedException">Not implemented.</exception>
        public override ICommand ConvertBack(ICommand value, object parameter, string language) => throw new NotImplementedException();
    }
}
