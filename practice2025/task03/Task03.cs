﻿using System.Collections;
namespace task03
{
    public class CustomCollection<T> : IEnumerable<T>
    {
        private readonly List<T> _items = new();

        public void Add(T item) => _items.Add(item);
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> GetReverseEnumerator()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                yield return _items[_items.Count - i - 1];
            }
        }

        public static IEnumerable<int> GenerateSequence(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return start + i;
            }
        }

        public IEnumerable<T> FilterAndSort(Func<T, bool> predicate, Func<T, IComparable> keySelector) => _items.Where(predicate).OrderBy(keySelector);

    }
}
