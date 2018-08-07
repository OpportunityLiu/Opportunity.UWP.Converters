using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Opportunity.UWP.Converters.Typed
{
    /// <summary>
    /// Collection of <see cref="Enum"/>s.
    /// </summary>
    public sealed class EnumValueCollection : IList<Enum>, IReadOnlyList<Enum>, IList
    {
        private readonly EnumToBooleanConverter parent;

        /// <summary>
        /// Type of inner values.
        /// </summary>
        public Type EnumType { get; private set; }

        internal readonly List<Enum> Items = new List<Enum>();

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
            get => this.Items[index];
            set
            {
                if (value is null)
                    throw new ArgumentNullException(nameof(value));
                if (value.GetType() != EnumType)
                    throw new ArgumentException($"Wrong type, {EnumType} expected.", nameof(value));
                this.Items[index] = value;
            }
        }

        /// <summary>
        /// Number of values in this <see cref="EnumValueCollection"/>.
        /// </summary>
        public int Count => this.Items.Count;

        bool ICollection<Enum>.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        bool IList.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => ((ICollection)this.Items).SyncRoot;

        object IList.this[int index] { get => this[index]; set => this[index] = (Enum)value; }

        /// <summary>
        /// Add a value into the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public void Add(Enum item) => Insert(Count, item);

        /// <summary>
        /// Remove all values in the <see cref="EnumValueCollection"/>.
        /// </summary>
        public void Clear()
        {
            this.Items.Clear();
            EnumType = null;
        }

        /// <summary>
        /// Check whether a value is in the <see cref="EnumValueCollection"/> or not.
        /// </summary>
        /// <param name="item">The value to check.</param>
        /// <returns>true if item is found in the <see cref="EnumValueCollection"/>; otherwise, false.</returns>
        public bool Contains(Enum item)
        {
            if (item is null)
                return false;
            return this.Items.Contains(item);
        }

        /// <summary>
        /// Find the index of a value in the <see cref="EnumValueCollection"/> or not.
        /// </summary>
        /// <param name="item">The value to find.</param>
        /// <returns>Index of <paramref name="item"/> if item is found in the <see cref="EnumValueCollection"/>; otherwise, -1.</returns>
        public int IndexOf(Enum item)
        {
            if (item is null)
                return -1;
            return this.Items.IndexOf(item);
        }

        /// <summary>
        /// Insert <paramref name="item"/> into the <see cref="EnumValueCollection"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index to insert.</param>
        /// <param name="item">The value to insert.</param>
        public void Insert(int index, Enum item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));
            if (EnumType is null)
                EnumType = item.GetType();
            else if (item.GetType() != EnumType)
                throw new ArgumentException($"Wrong type, {EnumType} expected.", nameof(item));
            this.Items.Insert(index, item);
        }

        /// <summary>
        /// Remove value at given <paramref name="index"/> of the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="index">Index of value to remove.</param>
        public void RemoveAt(int index)
        {
            this.Items.RemoveAt(index);
            if (this.Items.Count == 0)
                EnumType = null;
        }

        /// <summary>
        /// Copy values in the <see cref="EnumValueCollection"/> to an array.
        /// </summary>
        /// <param name="array">Array to copy values to.</param>
        /// <param name="arrayIndex">Index of <paramref name="array"/> where to start copy.</param>
        public void CopyTo(Enum[] array, int arrayIndex)
        {
            this.Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove the first match of <paramref name="item"/> in the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <param name="item">The value to remove.</param>
        /// <returns>true if a value removed; otherwise, false.</returns>
        public bool Remove(Enum item)
        {
            return this.Items.Remove(item);
        }

        /// <summary>
        /// Get the <see cref="IEnumerator{T}"/> to visit values in the <see cref="EnumValueCollection"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Enum> GetEnumerator() => this.Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.Items.GetEnumerator();

        int IList.Add(object value)
        {
            this.Add((Enum)value);
            return this.Count - 1;
        }

        bool IList.Contains(object value) => Contains((Enum)value);

        int IList.IndexOf(object value) => IndexOf((Enum)value);

        void IList.Insert(int index, object value) => Insert(index, (Enum)value);

        void IList.Remove(object value) => Remove((Enum)value);

        void ICollection.CopyTo(Array array, int index) => ((ICollection)this.Items).CopyTo(array, index);
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
            if (this.values.IsNullOrEmpty())
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
                if (this.values is null || this.values.Count == 0)
                    return null;
                return this.values[0];
            }
            else
            {
                return null;
            }
        }
    }
}
