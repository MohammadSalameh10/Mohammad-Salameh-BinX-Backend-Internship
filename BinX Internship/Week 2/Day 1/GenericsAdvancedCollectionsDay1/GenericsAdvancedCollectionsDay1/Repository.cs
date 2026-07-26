using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsAdvancedCollectionsDay1
{
    internal class Repository<T> where T : class
    {
        // The repository stores domain objects, so T must be a reference type.
        private readonly List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public IReadOnlyList<T> GetAll()
        {
            return _items.AsReadOnly();
        }

        public T? Find(Predicate<T> predicate)
        {
            return _items.Find(predicate);
        }
    }
}
