using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Delta.CertXplorer.Configuration
{
    [TypeConverter(typeof(SettingsCollectionConverter))]
    public class SettingsCollection<T> : ICollection<T>
    {
        private List<T> innerList;

        internal SettingsCollection(IEnumerable items)
        {
            innerList = items.Cast<T>().ToList();
        }

        public SettingsCollection(IEnumerable<T> items)
        {
            innerList = items.ToList();
        }

        public SettingsCollection()
        {
            innerList = new List<T>();
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            innerList.Add(item);
        }

        public void Clear()
        {
            innerList.Clear();
        }

        public bool Contains(T item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            innerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<T>)innerList).IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return innerList.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return innerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
