using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Delta.ImageRenameTool.ComponentModel
{
    // Adapted from https://www.codeproject.com/Articles/31418/Implementing-a-Sortable-BindingList-Very-Very-Quic
    internal class SortableBindingList<T> : BindingList<T>
    {
        private static Dictionary<string, Func<List<T>, IEnumerable<T>>> cachedOrderByExpressions = new Dictionary<string, Func<List<T>, IEnumerable<T>>>();

        private List<T> originalList;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;
        private Action<SortableBindingList<T>, List<T>> populateBaseList = (a, b) => a.ResetItems(b);

        public SortableBindingList() => originalList = new List<T>();

        public SortableBindingList(IEnumerable<T> enumerable)
        {
            originalList = enumerable.ToList();
            populateBaseList(this, originalList);
        }

        public SortableBindingList(List<T> list)
        {
            originalList = list;
            populateBaseList(this, originalList);
        }

        protected override bool SupportsSortingCore => true;

        protected override ListSortDirection SortDirectionCore => sortDirection;

        protected override PropertyDescriptor SortPropertyCore => sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            // Look for an appropriate sort method in the cache. If not found, call CreateOrderByMethod to create one. 
            // Apply it to the original list.
            // Notify any bound controls that the sort has been applied.


            sortProperty = prop;

            var orderByMethodName = direction == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;

            if (!cachedOrderByExpressions.ContainsKey(cacheKey))
                CreateOrderByMethod(prop, orderByMethodName, cacheKey);

            ResetItems(cachedOrderByExpressions[cacheKey](originalList).ToList());
            ResetBindings();
            sortDirection = sortDirection == ListSortDirection.Ascending ?
                ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        protected override void RemoveSortCore() => ResetItems(originalList);

        protected override void OnListChanged(ListChangedEventArgs e) => originalList = Items.ToList();

        private void CreateOrderByMethod(PropertyDescriptor prop, string orderByMethodName, string cacheKey)
        {
            // Create a generic method implementation for IEnumerable<T>. Cache it.

            var sourceParameter = Expression.Parameter(typeof(List<T>), "source");
            var lambdaParameter = Expression.Parameter(typeof(T), "lambdaParameter");
            var accesedMember = typeof(T).GetProperty(prop.Name);
            var propertySelectorLambda = Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter, accesedMember), lambdaParameter);
            var orderByMethod = typeof(Enumerable).GetMethods()
                .Where(a => a.Name == orderByMethodName && a.GetParameters().Length == 2)
                .Single()
                .MakeGenericMethod(typeof(T), prop.PropertyType);

            var orderByExpression = Expression.Lambda<Func<List<T>, IEnumerable<T>>>(
                Expression.Call(orderByMethod, new Expression[] { sourceParameter, propertySelectorLambda }), 
                sourceParameter);

            cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
        }

        private void ResetItems(List<T> items)
        {
            base.ClearItems();
            for (var i = 0; i < items.Count; i++)
                base.InsertItem(i, items[i]);
        }
    }
}
