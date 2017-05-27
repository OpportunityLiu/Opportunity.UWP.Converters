using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static Opportunity.Converters.Internal.ConvertHelper;

namespace Opportunity.Converters
{
    /// <summary>
    /// Convert <see cref="object"/>s to <see cref="bool"/> values.
    /// </summary>
    [Windows.UI.Xaml.Markup.ContentProperty(Name = nameof(NextConverter))]
    public sealed class ObjectToBooleanConverter : ChainConverter
    {
        private class ValueCollection : IList
        {
            public ValueCollection(ObjectToBooleanConverter parent)
            {
                this.parent = parent;
            }

            public readonly List<object> Items = new List<object>();
            private readonly ObjectToBooleanConverter parent;

            public object this[int index] { get => Items[index]; set => Items[index] = value; }

            bool IList.IsFixedSize => ((IList)this.Items).IsFixedSize;

            bool IList.IsReadOnly => ((IList)this.Items).IsReadOnly;

            public int Count => this.Items.Count;

            bool ICollection.IsSynchronized => ((IList)this.Items).IsSynchronized;

            object ICollection.SyncRoot => ((IList)this.Items).SyncRoot;

            public int Add(object value)
            {
                var r = ((IList)this.Items).Add(value);
                this.parent.refresh();
                return r;
            }

            public void Clear()
            {
                this.Items.Clear();
                this.parent.refresh();
            }

            public bool Contains(object value)
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

            public void CopyTo(Array array, int index)
            {
                ((IList)this.Items).CopyTo(array, index);
            }

            public IEnumerator GetEnumerator()
            {
                return ((IList)this.Items).GetEnumerator();
            }

            public int IndexOf(object value)
            {
                return ((IList)this.Items).IndexOf(value);
            }

            public void Insert(int index, object value)
            {
                this.Items.Insert(index, value);
                this.parent.refresh();
            }

            public void Remove(object value)
            {
                var c = this.parent.ValueComparer;
                if (c == null)
                {
                    if (this.Items.Remove(value))
                        this.parent.refresh();
                    return;
                }
                var index = this.Items.FindIndex(o => c.Equals(o, value));
                if (index != -1)
                {
                    this.Items.RemoveAt(index);
                    this.parent.refresh();
                }
            }

            public void RemoveAt(int index)
            {
                this.Items.RemoveAt(index);
                this.parent.refresh();
            }
        }

        private static Type merge(Type left, Type right)
        {
            if (left == null && right == null)
                return typeof(object);
            if (left == null)
            {
                if (right == null)
                    return typeof(object);
                else
                    return right;
            }
            if (right == null)
                return left;
            if (left == right)
                return left;
            // UNDONE: left 和 right 的共同基类
            return typeof(object);
        }

        private void refresh()
        {
            var list = default(IEnumerable<object>);
            if (this.valuesForTrue != null)
            {
                if (this.valuesForFalse == null)
                    list = this.valuesForTrue.Cast<object>();
                else
                    list = this.valuesForTrue.Cast<object>().Concat(this.valuesForFalse.Cast<object>());
            }
            if (this.valuesForFalse != null)
            {
                list = this.valuesForFalse.Cast<object>();
            }
            if (list == null)
                list = Enumerable.Empty<object>();
            this.valueType = list.Aggregate(typeof(object), (t, o) => merge(t, o?.GetType()));
        }

        private Type valueType = typeof(object);

        private ValueCollection valuesForTrue;
        /// <summary>
        /// <see cref="object"/>s will be converted to <c>true</c>.
        /// </summary>
        public IList ValuesForTrue => LazyInitializer.EnsureInitialized(ref this.valuesForTrue, () => new ValueCollection(this));

        private ValueCollection valuesForFalse;
        /// <summary>
        /// <see cref="object"/>s will be converted to <c>false</c>.
        /// </summary>
        public IList ValuesForFalse => LazyInitializer.EnsureInitialized(ref this.valuesForFalse, () => new ValueCollection(this));

        /// <summary>
        /// The <see cref="IEqualityComparer"/> used to compare values.
        /// </summary>
        public IEqualityComparer ValueComparer
        {
            get => (IEqualityComparer)GetValue(ValueComparerProperty); set => SetValue(ValueComparerProperty, value);
        }

        /// <summary>
        /// Identifies the <see cref="ValueComparer"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueComparerProperty =
            DependencyProperty.Register("ValueComparer", typeof(IEqualityComparer), typeof(ObjectToBooleanConverter), new PropertyMetadata(null));

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
            DependencyProperty.Register("IfNeither", typeof(bool), typeof(ObjectToBooleanConverter), new PropertyMetadata(false));

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
            DependencyProperty.Register("IfBoth", typeof(bool), typeof(ObjectToBooleanConverter), new PropertyMetadata(false));

        /// <inheritdoc />
        protected override object ConvertImpl(object value, object parameter, string language)
        {
            var comparer = this.ValueComparer ?? EqualityComparer<object>.Default;
            value = ChangeType(value, this.valueType);
            var isTrue = false;
            if(this.valuesForTrue!=null)
            {
                foreach (var item in this.valuesForTrue)
                {
                    if(comparer.Equals(value,item))
                    {
                        isTrue = true;
                        break;
                    }
                }
            }
            var isFalse = false;
            if (this.valuesForFalse != null)
            {
                foreach (var item in this.valuesForFalse)
                {
                    if (comparer.Equals(value, item))
                    {
                        isFalse = true;
                        break;
                    }
                }
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
        protected override object ConvertBackImpl(object value, object parameter, string language)
        {
            var v = ChangeType<bool>(value);
            if (v)
            {
                if (this.valuesForTrue == null || this.valuesForTrue.Count == 0)
                    return null;
                return this.valuesForTrue[0];
            }
            else
            {
                if (this.valuesForFalse == null || this.valuesForFalse.Count == 0)
                    return null;
                return this.valuesForFalse[0];
            }
        }
    }
}
