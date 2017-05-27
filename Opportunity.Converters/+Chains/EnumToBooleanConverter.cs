using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters
{
    /// <summary>
    /// Collection of <see cref="Enum"/>s.
    /// </summary>
    public sealed class EnumValueCollection : IList<IConvertible>, IReadOnlyList<IConvertible>, IList
    {
        private readonly EnumToBooleanConverter parent;

        internal readonly List<ulong> Items = new List<ulong>();

        internal EnumValueCollection(EnumToBooleanConverter parent)
        {
            this.parent = parent;
        }

        internal static ulong ToStorage(IConvertible value)
        {
            switch (value.GetTypeCode())
            {
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.SByte:
            default:
                return unchecked((ulong)Convert.ToInt64(value));
            case TypeCode.Byte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
                return Convert.ToUInt64(value);
            }
        }

        /// <summary>
        /// Get or set value at <paramref name="index"/>
        /// </summary>
        /// <param name="index">Index of value.</param>
        /// <returns>The value at <paramref name="index"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="Count"/> -1.</exception>
        public IConvertible this[int index] { get => Items[index]; set => Items[index] = ToStorage(value); }

        /// <summary>
        /// Number of values in this <see cref="EnumValueCollection"/>.
        /// </summary>
        public int Count => this.Items.Count;

        bool ICollection<IConvertible>.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        bool IList.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => ((ICollection)this.Items).SyncRoot;

        object IList.this[int index] { get => Items[index]; set => this[index] = (IConvertible)value; }

        /// <summary>
        /// Add a value into the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public void Add(IConvertible item)
        {
            this.Items.Add(ToStorage(item));
        }

        /// <summary>
        /// Remove all values in the <see cref="EnumValueCollection"/>.
        /// </summary>
        public void Clear()
        {
            this.Items.Clear();
        }

        /// <summary>
        /// Check whether a value is in the <see cref="EnumValueCollection"/> or not.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found in the <see cref="EnumValueCollection"/>; otherwise, false.</returns>
        public bool Contains(IConvertible value)
        {
            var val = ToStorage(value);
            return this.Items.Contains(val);
        }

        /// <summary>
        /// Find the index of a value in the <see cref="EnumValueCollection"/> or not.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>Index of <paramref name="value"/> if item is found in the <see cref="EnumValueCollection"/>; otherwise, -1.</returns>
        public int IndexOf(IConvertible value)
        {
            return this.Items.IndexOf(ToStorage(value));
        }

        /// <summary>
        /// Insert <paramref name="value"/> into the <see cref="EnumValueCollection"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index to insert.</param>
        /// <param name="value">The value to insert.</param>
        public void Insert(int index, IConvertible value)
        {
            this.Items.Insert(index, ToStorage(value));
        }

        /// <summary>
        /// Remove value at given <paramref name="index"/> of the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="index">Index of value to remove.</param>
        public void RemoveAt(int index)
        {
            this.Items.RemoveAt(index);
        }

        /// <summary>
        /// Copy values in the <see cref="EnumValueCollection"/> to an array.
        /// </summary>
        /// <param name="array">Array to copy values to.</param>
        /// <param name="arrayIndex">Index of <paramref name="array"/> where to start copy.</param>
        public void CopyTo(IConvertible[] array, int arrayIndex)
        {
            ((ICollection)this.Items).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove the first match of <paramref name="item"/> in the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="item">The value to remove.</param>
        /// <returns>true if a value removed; otherwise, false.</returns>
        public bool Remove(IConvertible item)
        {
            var val = ToStorage(item);
            return this.Items.Remove(val);
        }

        /// <summary>
        /// Get the <see cref="IEnumerator{T}"/> to visit values in the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IConvertible> GetEnumerator() => this.Items.Cast<IConvertible>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.Items.GetEnumerator();

        int IList.Add(object value)
        {
            this.Add((IConvertible)value);
            return this.Count - 1;
        }

        bool IList.Contains(object value) => Contains((IConvertible)value);

        int IList.IndexOf(object value) => IndexOf((IConvertible)value);

        void IList.Insert(int index, object value) => Insert(index, (IConvertible)value);

        void IList.Remove(object value) => Remove((IConvertible)value);

        void ICollection.CopyTo(Array array, int index) => ((ICollection)this.Items).CopyTo(array, index);
    }

    /// <summary>
    /// Convert <see cref="Enum"/>s to <see cref="bool"/> values.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Values))]
    public sealed class EnumToBooleanConverter : ChainConverter
    {
        private EnumValueCollection values;
        /// <summary>
        /// <see cref="object"/>s will be converted to <see cref="InRange"/>.
        /// </summary>
        public EnumValueCollection Values => LazyInitializer.EnsureInitialized(ref this.values, () => new EnumValueCollection(this));

        /// <summary>
        /// Returns when <c>value</c> is in <see cref="Values"/>.
        /// </summary>
        public bool InRange
        {
            get => (bool)GetValue(IfNeitherProperty);
            set => SetValue(IfNeitherProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="InRange"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IfNeitherProperty =
            DependencyProperty.Register("InRange", typeof(bool), typeof(EnumToBooleanConverter), new PropertyMetadata(true));

        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            if (!(value is IConvertible val))
                return !InRange;
            if (this.values == null)
                return !InRange;
            var storage = EnumValueCollection.ToStorage(val);
            if (this.values.Items.Contains(storage))
                return InRange;
            return !InRange;
        }

        /// <inheritdoc />
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            var v = Internal.ConvertHelper.ChangeType<bool>(value);
            if (v == InRange)
            {
                if (this.values == null || this.values.Count == 0)
                    return null;
                return this.values[0];
            }
            else
            {
                if (this.values == null || this.values.Count == 0)
                    return 0;
                if (!this.values.Items.Contains(ulong.MaxValue))
                    return ulong.MaxValue;
                else if (!this.values.Items.Contains(ulong.MinValue))
                    return ulong.MinValue;
                return null;
            }
        }
    }
}
