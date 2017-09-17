using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters.Typed
{
    /// <summary>
    /// Collection of values.
    /// </summary>
    public sealed class ValueCollection<T> : IList<T>, IReadOnlyList<T>, IList
    {
        internal ValueCollection(ValuesToBooleanConverter<T> parent)
        {
            this.parent = parent;
        }

        internal readonly List<T> Items = new List<T>();
        private readonly ValuesToBooleanConverter<T> parent;

        /// <summary>
        /// Get or set value at <paramref name="index"/>
        /// </summary>
        /// <param name="index">Index of value.</param>
        /// <returns>The value at <paramref name="index"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 or greater than <see cref="Count"/> -1.</exception>
        public T this[int index] { get => Items[index]; set => Items[index] = value; }

        /// <summary>
        /// Number of values in this <see cref="ValueCollection{T}"/>.
        /// </summary>
        public int Count => this.Items.Count;

        bool ICollection<T>.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        bool IList.IsReadOnly => false;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => ((ICollection)this.Items).SyncRoot;

        object IList.this[int index] { get => Items[index]; set => Items[index] = ChangeType<T>(value); }

        /// <summary>
        /// Add a value into the <see cref="ValueCollection{T}"/>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public void Add(T item)
        {
            this.Items.Add(item);
        }

        /// <summary>
        /// Remove all values in the <see cref="ValueCollection{T}"/>.
        /// </summary>
        public void Clear()
        {
            this.Items.Clear();
        }

        /// <summary>
        /// Check whether a value is in the <see cref="ValueCollection{T}"/> or not.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found in the <see cref="ValueCollection{T}"/>; otherwise, false.</returns>
        public bool Contains(T value)
        {
            var comparer = this.parent.ValueComparer;
            if (comparer == null)
                return this.Items.Contains(value);
            foreach (var item in this.Items)
            {
                if (comparer.Equals(value, item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find the index of a value in the <see cref="ValueCollection{T}"/> or not.
        /// </summary>
        /// <param name="value">The value to find.</param>
        /// <returns>Index of <paramref name="value"/> if item is found in the <see cref="ValueCollection{T}"/>; otherwise, -1.</returns>
        public int IndexOf(T value)
        {
            return this.Items.IndexOf(value);
        }

        /// <summary>
        /// Insert <paramref name="value"/> into the <see cref="ValueCollection{T}"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index to insert.</param>
        /// <param name="value">The value to insert.</param>
        public void Insert(int index, T value)
        {
            this.Items.Insert(index, value);
        }

        /// <summary>
        /// Remove value at given <paramref name="index"/> of the <see cref="ValueCollection{T}"/>.
        /// </summary>
        /// <param name="index">Index of value to remove.</param>
        public void RemoveAt(int index)
        {
            this.Items.RemoveAt(index);
        }

        /// <summary>
        /// Copy values in the <see cref="ValueCollection{T}"/> to an array.
        /// </summary>
        /// <param name="array">Array to copy values to.</param>
        /// <param name="arrayIndex">Index of <paramref name="array"/> where to start copy.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove the first match of <paramref name="item"/> in the <see cref="ValueCollection{T}"/>.
        /// </summary>
        /// <param name="item">The value to remove.</param>
        /// <returns>true if a value removed; otherwise, false.</returns>
        public bool Remove(T item)
        {
            var c = this.parent.ValueComparer;
            if (c == null)
            {
                if (this.Items.Remove(item))
                {
                    return true;
                }
                return false;
            }
            var index = this.Items.FindIndex(o => c.Equals(o, item));
            if (index != -1)
            {
                this.Items.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the <see cref="IEnumerator{T}"/> to visit values in the <see cref="ValueCollection{T}"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => this.Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.Items.GetEnumerator();

        int IList.Add(object value)
        {
            this.Add(ChangeType<T>(value));
            return this.Count - 1;
        }

        bool IList.Contains(object value) => Contains(ChangeType<T>(value));

        int IList.IndexOf(object value) => IndexOf(ChangeType<T>(value));

        void IList.Insert(int index, object value) => Insert(index, ChangeType<T>(value));

        void IList.Remove(object value) => Remove(ChangeType<T>(value));

        void ICollection.CopyTo(Array array, int index) => ((ICollection)this.Items).CopyTo(array, index);
    }

    /// <summary>
    /// Convert <see cref="object"/>s to <see cref="bool"/> values.
    /// </summary>
    public class ValuesToBooleanConverter<T> : ValueConverter<T, bool>
    {
        private ValueCollection<T> valuesForTrue;
        /// <summary>
        /// <see cref="object"/>s will be converted to <c>true</c>.
        /// </summary>
        public ValueCollection<T> ValuesForTrue => LazyInitializer.EnsureInitialized(ref this.valuesForTrue, () => new ValueCollection<T>(this));

        private ValueCollection<T> valuesForFalse;
        /// <summary>
        /// <see cref="object"/>s will be converted to <c>false</c>.
        /// </summary>
        public ValueCollection<T> ValuesForFalse => LazyInitializer.EnsureInitialized(ref this.valuesForFalse, () => new ValueCollection<T>(this));

        /// <summary>
        /// The <see cref="IEqualityComparer{T}"/> used to compare values.
        /// </summary>
        public IEqualityComparer<T> ValueComparer
        {
            get => (IEqualityComparer<T>)GetValue(ValueComparerProperty); set => SetValue(ValueComparerProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ValueComparer"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueComparerProperty =
            DependencyProperty.Register("ValueComparer", typeof(IEqualityComparer<T>), typeof(ValuesToBooleanConverter<T>), new PropertyMetadata(null));

        /// <summary>
        /// Returns when <c>value</c> is in neither <see cref="ValuesForTrue"/> nor <see cref="ValuesForFalse"/>.
        /// </summary>
        public bool IfNeither
        {
            get => (bool)GetValue(IfNeitherProperty);
            set => SetValue(IfNeitherProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IfNeither"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IfNeitherProperty =
            DependencyProperty.Register("IfNeither", typeof(bool), typeof(ValuesToBooleanConverter<T>), new PropertyMetadata(false));

        /// <summary>
        /// Returns when <c>value</c> is in both <see cref="ValuesForTrue"/> and <see cref="ValuesForFalse"/>.
        /// </summary>
        public bool IfBoth
        {
            get => (bool)GetValue(IfBothProperty);
            set => SetValue(IfBothProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="IfBoth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IfBothProperty =
            DependencyProperty.Register("IfBoth", typeof(bool), typeof(ValuesToBooleanConverter<T>), new PropertyMetadata(false));

        /// <inheritdoc />
        public override bool Convert(T value, object parameter, string language)
        {
            var isTrue = false;
            if (this.valuesForTrue != null)
            {
                isTrue = this.valuesForTrue.Contains(value);
            }
            var isFalse = false;
            if (this.valuesForFalse != null)
            {
                isFalse = this.valuesForFalse.Contains(value);
            }
            if (isTrue && isFalse)
                return this.IfBoth;
            if (isTrue)
                return true;
            if (isFalse)
                return false;
            return this.IfNeither;
        }

        /// <inheritdoc />
        public override T ConvertBack(bool value, object parameter, string language)
        {
            if (value)
            {
                if (this.valuesForTrue == null || this.valuesForTrue.Count == 0)
                    return default(T);
                return this.valuesForTrue[0];
            }
            else
            {
                if (this.valuesForFalse == null || this.valuesForFalse.Count == 0)
                    return default(T);
                return this.valuesForFalse[0];
            }
        }
    }
}
