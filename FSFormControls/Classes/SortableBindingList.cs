using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

#if NET35_OR_GREATER || NETCOREAPP
    using System.Linq;
    using System.Xml.Linq;
#endif

namespace FSFormControls
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection;
        private PropertyDescriptor _sortProperty;

        public SortableBindingList()
            : base()
        {
        }

        public SortableBindingList(List<T> list)
            : base(list)
        {
        }
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (PropertyComparer.CanSort(prop.PropertyType))
            {
                ((List<T>)Items).Sort(new PropertyComparer(prop, direction));
                _sortDirection = direction;
                _sortProperty = prop;
                _isSorted = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }
        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }

        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return _sortDirection; }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return _sortProperty; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        internal class PropertyComparer : Comparer<T>
        {
            private readonly IComparer _comparer;
            private readonly ListSortDirection _direction;
            private readonly PropertyDescriptor _prop;
            private readonly bool _useToString;

            public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
            {
                if (!prop.ComponentType.IsAssignableFrom(typeof(T)))
                {
                    throw new MissingMemberException(typeof(T).Name, prop.Name);
                }

                Debug.Assert(CanSort(prop.PropertyType), "Cannot use PropertyComparer unless it can be compared by IComparable or ToString");

                _prop = prop;
                _direction = direction;

                if (CanSortWithIComparable(prop.PropertyType))
                {
#if NET45_OR_GREATER || NETCOREAPP
                    var property = typeof(Comparer<>).MakeGenericType(new[] { prop.PropertyType }).GetTypeInfo().GetDeclaredProperty("Default");
#else
                    var property = typeof(Comparer<>).MakeGenericType(new[] { prop.PropertyType }).GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
#endif
                    _comparer = (IComparer)property.GetValue(null, null);
                    _useToString = false;
                }
                else
                {
                    Debug.Assert(
                        CanSortWithToString(prop.PropertyType),
                        "Cannot use PropertyComparer unless it can be compared by IComparable or ToString");

                    _comparer = StringComparer.CurrentCultureIgnoreCase;
                    _useToString = true;
                }
            }

            public override int Compare(T left, T right)
            {
                var leftValue = _prop.GetValue(left);
                var rightValue = _prop.GetValue(right);

                if (_useToString)
                {
                    leftValue = leftValue != null ? leftValue.ToString() : null;
                    rightValue = rightValue != null ? rightValue.ToString() : null;
                }

                return _direction == ListSortDirection.Ascending
                           ? _comparer.Compare(leftValue, rightValue)
                           : _comparer.Compare(rightValue, leftValue);
            }

            public static bool CanSort(Type type)
            {
                return CanSortWithToString(type) || CanSortWithIComparable(type);
            }

            private static bool CanSortWithIComparable(Type type)
            {
                return type.GetInterface("IComparable") != null ||
                       (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
            }

#if NET35_OR_GREATER || NETCOREAPP
            private static bool CanSortWithToString(Type type)
            {
                return type.Equals(typeof(XNode)) || type.IsSubclassOf(typeof(XNode));
            }
#else
            private static bool CanSortWithToString(Type type)
            {
                return type.Equals(typeof(string)) ||
                       type.Equals(typeof(int)) ||
                       type.Equals(typeof(double)) ||
                       type.Equals(typeof(float)) ||
                       type.Equals(typeof(decimal)) ||
                       type.Equals(typeof(short)) ||
                       type.Equals(typeof(long)) ||
                       type.Equals(typeof(byte)) ||
                       type.Equals(typeof(uint)) ||
                       type.Equals(typeof(ulong)) ||
                       type.Equals(typeof(ushort));
            }
#endif
        }
    }

    public static class EnumerableExtensions
    {
#if NET35_OR_GREATER || NETCOREAPP
        public static BindingList<T> ToSortableBindingList<T>(IEnumerable<T> source)
        {
            return new SortableBindingList<T>(source.ToList());
        }
#else
        public static SortableBindingList<T> ToSortableBindingList<T>(IEnumerable<T> source)
        {
            SortableBindingList<T> bindingList = new SortableBindingList<T>();

            if (source != null)
            {
                foreach (T item in source)
                    bindingList.Add(item);
            }

            return bindingList;
        }
#endif
    }
}