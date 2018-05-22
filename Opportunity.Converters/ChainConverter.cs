using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Opportunity.Converters
{
    /// <summary>
    /// Collection of <see cref="IValueConverter"/>s.
    /// </summary>
    public sealed class ConverterCollection : ObservableCollection<IValueConverter>
    {
        internal ConverterCollection() { }

        /// <inheritdoc/>
        protected override void SetItem(int index, IValueConverter item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            base.SetItem(index, item);
        }

        /// <inheritdoc/>
        protected override void InsertItem(int index, IValueConverter item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            base.InsertItem(index, item);
        }
    }

    /// <summary>
    /// Repersents a chain of <see cref="IValueConverter"/>s,
    /// data will go through the chain and be converted mutiple times.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Converters))]
    public sealed class ChainConverter : ValueConverter
    {
        /// <summary>
        /// Create new instance of <see cref="ChainConverter"/>.
        /// </summary>
        public ChainConverter() { }

        /// <summary>
        /// Create new instance of <see cref="ChainConverter"/>.
        /// </summary>
        /// <param name="converters"><see cref="IValueConverter"/>s used to populate the <see cref="Converters"/>.</param>
        public ChainConverter(params IValueConverter[] converters)
            : this((IEnumerable<IValueConverter>)converters) { }

        /// <summary>
        /// Create new instance of <see cref="ChainConverter"/>.
        /// </summary>
        /// <param name="converters"><see cref="IValueConverter"/>s used to populate the <see cref="Converters"/>.</param>
        public ChainConverter(IEnumerable<IValueConverter> converters)
            : this()
        {
            if (converters is null)
                return;
            foreach (var item in converters)
            {
                Converters.Add(item);
            }
        }

        /// <summary>
        /// Contains <see cref="IValueConverter"/>s in the chain.
        /// </summary> 
        public ConverterCollection Converters { get; } = new ConverterCollection();

        /// <inheritdoc />
        public sealed override object Convert(object value, Type targetType, object parameter, string language)
        {
            foreach (var item in Converters)
            {
                value = item.Convert(value, targetType, parameter, language);
            }
            return value;
        }

        /// <inheritdoc />
        public sealed override object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            for (var i = Converters.Count - 1; i >= 0; i--)
            {
                value = Converters[i].ConvertBack(value, targetType, parameter, language);
            }
            return value;
        }
    }
}
