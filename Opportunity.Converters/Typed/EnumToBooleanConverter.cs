using Opportunity.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Collection of <see cref="Enum"/>s.
    /// </summary>
    public sealed class EnumValueCollection : IList<Enum>, IReadOnlyList<Enum>, IList
    {
        private readonly EnumToBooleanConverter parent;

        internal readonly List<Enum> Keys = new List<Enum>();

        internal readonly List<ulong> Values = new List<ulong>();

        internal EnumValueCollection(EnumToBooleanConverter parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Get or set value at <paramref name="index"/>
        /// </summary>
        /// <param name="index">Index of value.</param>
        /// <returns>The value at <paramref name="index"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="Count"/> -1.</exception>
        public Enum this[int index]
        {
            get => Keys[index];
            set
            {
                this.Values[index] = value.ToUInt64();
                this.Keys[index] = value;
            }
        }

        /// <summary>
        /// Number of values in this <see cref="EnumValueCollection"/>.
        /// </summary>
        public int Count => this.Keys.Count;

        bool ICollection<Enum>.IsReadOnly => false;
        bool IList.IsReadOnly => false;
        bool IList.IsFixedSize => false;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => ((ICollection)this.Keys).SyncRoot;

        object IList.this[int index] { get => this[index]; set => this[index] = (Enum)value; }

        /// <summary>
        /// Add a value into the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public void Add(Enum item)
        {
            this.Values.Add(item.ToUInt64());
            this.Keys.Add(item);
        }

        /// <summary>
        /// Remove all values in the <see cref="EnumValueCollection"/>.
        /// </summary>
        public void Clear()
        {
            this.Keys.Clear();
            this.Values.Clear();
        }

        /// <summary>
        /// Check whether a value is in the <see cref="EnumValueCollection"/> or not.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found in the <see cref="EnumValueCollection"/>; otherwise, false.</returns>
        public bool Contains(Enum value)
        {
            if (value == null)
                return false;
            return this.Values.Contains(value.ToUInt64());
        }

        /// <summary>
        /// Find the index of a value in the <see cref="EnumValueCollection"/> or not.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>Index of <paramref name="value"/> if item is found in the <see cref="EnumValueCollection"/>; otherwise, -1.</returns>
        public int IndexOf(Enum value)
        {
            if (value == null)
                return -1;
            return this.Values.IndexOf(value.ToUInt64());
        }

        /// <summary>
        /// Insert <paramref name="value"/> into the <see cref="EnumValueCollection"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index to insert.</param>
        /// <param name="value">The value to insert.</param>
        public void Insert(int index, Enum value)
        {
            this.Values.Insert(index, value.ToUInt64());
            this.Keys.Insert(index, value);
        }

        /// <summary>
        /// Remove value at given <paramref name="index"/> of the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="index">Index of value to remove.</param>
        public void RemoveAt(int index)
        {
            this.Keys.RemoveAt(index);
            this.Values.RemoveAt(index);
        }

        /// <summary>
        /// Copy values in the <see cref="EnumValueCollection"/> to an array.
        /// </summary>
        /// <param name="array">Array to copy values to.</param>
        /// <param name="arrayIndex">Index of <paramref name="array"/> where to start copy.</param>
        public void CopyTo(Enum[] array, int arrayIndex) => this.Keys.CopyTo(array, arrayIndex);

        /// <summary>
        /// Remove the first match of <paramref name="item"/> in the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="item">The value to remove.</param>
        /// <returns>true if a value removed; otherwise, false.</returns>
        public bool Remove(Enum item)
        {
            if (item == null)
                return false;
            var val = item.ToUInt64();
            var idx = this.Values.IndexOf(val);
            if (idx < 0)
                return false;
            this.Keys.RemoveAt(idx);
            this.Values.RemoveAt(idx);
            return true;
        }

        /// <summary>
        /// Get the <see cref="IEnumerator{T}"/> to visit values in the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Enum> GetEnumerator() => this.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.Keys.GetEnumerator();

        int IList.Add(object value)
        {
            Add((Enum)value);
            return this.Count - 1;
        }

        bool IList.Contains(object value) => Contains((Enum)value);

        int IList.IndexOf(object value) => IndexOf((Enum)value);

        void IList.Insert(int index, object value) => Insert(index, (Enum)value);

        void IList.Remove(object value) => Remove((Enum)value);

        void ICollection.CopyTo(Array array, int index) => ((ICollection)this.Keys).CopyTo(array, index);
    }

    /// <summary>
    /// Convert <see cref="Enum"/>s to <see cref="bool"/> values.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(Values))]
    public sealed class EnumToBooleanConverter : ValueConverter<Enum, bool>
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
        public override bool Convert(Enum value, object parameter, string language)
        {
            if (this.values == null)
                return !InRange;
            if (this.values.Contains(value))
                return InRange;
            return !InRange;
        }

        /// <inheritdoc />
        public override Enum ConvertBack(bool value, object parameter, string language)
        {
            if (value == InRange)
            {
                if (this.values == null || this.values.Count == 0)
                    return null;
                return this.values[0];
            }
            else
            {
                if (this.values == null || this.values.Count == 0)
                    return null;
                var enumType = this.values[0].GetType();
                var enumValues = Enum.GetValues(enumType);
                foreach (var item in enumValues.Cast<Enum>())
                {
                    if (!this.values.Contains(item))
                        return item;
                }
                return null;
            }
        }
    }
}
