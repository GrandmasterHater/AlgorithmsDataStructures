using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures.PowerSet
{
    public struct Pair
    {
        public int FirstCoord { get; set; }
        public int SecondCoord { get; set; }

        public Pair(int firstCoord, int secondCoord)
        {
            FirstCoord = firstCoord;
            SecondCoord = secondCoord;
        }
    }
    
    
    public static class PowerSetTask10_2
    {
        // Exercise 10, task 4, time complexity O(n^2), space complexity O(n)
        public static PowerSet<Pair> CartesianProduct(this PowerSet<int> set1, PowerSet<int> set2)
        {
            PowerSet<Pair> result = new PowerSet<Pair>();

            foreach (int firstCoord in set1.AllValues)
            {
                foreach (var secondCoord in set2.AllValues)
                {
                    result.Put(new Pair(firstCoord, secondCoord));
                }
            }
            
            return result;
        }
        
        // Exercise 10, task 5, time complexity O(n^totalSetsCount), space complexity O(n)
        public static PowerSet<T> Intersection<T>(this PowerSet<T> set1, PowerSet<T> set2, PowerSet<T> set3, params PowerSet<T>[] sets)
        {
            PowerSet<T>[] allSets = new[] {set1, set2, set3}.Concat(sets).ToArray();
            PowerSet<T> result = new PowerSet<T>();
            
            IEnumerable<T> intersection = set1.AllValues.Where(value => allSets.All(set => set.Get(value)));

            //in order to avoid unnecessary allocations for .ToList().ForEach(...)
            foreach (T value in intersection)
            {
                result.Put(value);
            }
            
            return result;
        }
    }

    // Exercise 10, task 6
    public class Bag<T>
    {
        private Dictionary<T, int> _items = new Dictionary<T, int>();
        
        // Exercise 10, task 6, time complexity O(n), space complexity O(1)
        public void Add(T item)
        {
            if (_items.ContainsKey(item))
                _items[item]++;
            else
                _items[item] = 1;
        }
        
        // Exercise 10, task 6, time complexity O(n), space complexity O(1)
        public void Remove(T item)
        {
            if (!_items.ContainsKey(item))
            {
                return;
            }
            
            --_items[item];
            
            if (_items[item] <= 0)
                _items.Remove(item);
        }

        // Exercise 10, task 6, time complexity O(n), space complexity O(1)
        public KeyValuePair<T, int> Get(T item)
        {
            _items.TryGetValue(item, out int count);
            
            return count > 0 ? new KeyValuePair<T, int>(item, count) : default;
        }

        // Exercise 10, task 6, time complexity O(n), space complexity O(1)
        public IEnumerable<KeyValuePair<T, int>> AllElements() => _items;
    }
}