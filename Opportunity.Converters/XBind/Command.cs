using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Opportunity.Converters.XBind
{
    /// <summary>
    /// Static methods for <see cref="ICommand"/> conversion.
    /// </summary>
    public static class Command
    {
        /// <summary>
        /// Execute a command after specified delay.
        /// </summary>
        /// <param name="value">The command to execute</param>
        /// <param name="delay">Delay time in millisecond</param>
        /// <returns>A command, execute the <paramref name="value"/> after delay of <paramref name="delay"/> ms.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="delay"/> is less than 0.</exception>
        public static ICommand Delay(ICommand value, int delay)
        {
            if (value == null)
                return null;
            if (delay < 0)
                throw new ArgumentException("Must be non-negetive finite number.", nameof(delay));
            return new DelayCommand(value, delay);
        }

        private sealed class DelayCommand : ICommand
        {
            private readonly ICommand command;
            private readonly int delay;

            public DelayCommand(ICommand command, int delay)
            {
                this.command = command;
                this.delay = delay;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add => this.command.CanExecuteChanged += value;
                remove => this.command.CanExecuteChanged -= value;
            }

            bool ICommand.CanExecute(object parameter) => this.command.CanExecute(parameter);

            async void ICommand.Execute(object parameter)
            {
                await Task.Delay(this.delay);
                this.command.Execute(parameter);
            }
        }

        /// <summary>
        /// Limit the frequency of executing a command by specified delay.
        /// </summary>
        /// <param name="value">The command to execute</param>
        /// <param name="delay">Delay time in millisecond</param>
        /// <returns>A command, execute the <paramref name="value"/> after delay of <paramref name="delay"/> ms, if <see cref="ICommand.Execute(object)"/> is not called again in this period.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="delay"/> is less than 0.</exception>
        public static ICommand Throttle(ICommand value, int delay)
        {
            if (value == null)
                return null;
            if (delay < 0)
                throw new ArgumentException("Must be non-negetive finite number.", nameof(delay));
            return new ThrottleCommand(value, delay);
        }

        private sealed class ThrottleCommand : ICommand
        {
            private readonly ICommand command;
            private readonly int delay;
            private int inThrottle = 0;

            public ThrottleCommand(ICommand command, int delay)
            {
                this.command = command;
                this.delay = delay;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add => this.command.CanExecuteChanged += value;
                remove => this.command.CanExecuteChanged -= value;
            }

            bool ICommand.CanExecute(object parameter) => this.command.CanExecute(parameter);

            async void ICommand.Execute(object parameter)
            {
                Interlocked.Increment(ref this.inThrottle);
                await Task.Delay(this.delay);
                if (Interlocked.Decrement(ref this.inThrottle) == 0)
                    this.command.Execute(parameter);
            }
        }
    }
}
