using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{
    public class PowerSet<T>
    {
        private int _size;
        private Slot<T>[] _values;
        private int _count;
        
        public IEnumerable<T> AllValues => _values.Where(slot => slot.HasValue).Select(slot => slot.Value);
        
        public PowerSet()
        {
            _size = 21787;
            _values = new Slot<T>[_size];
            _count = 0;
            
            for(int i = 0; i < _size; i++) 
                _values[i] = new EmptySlot<T>();
        }

        // Exercise 10, task 1, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _count;
        }

        // Exercise 10, task 1, time complexity O(n), Omega(1), space complexity O(1), where n depend on size
        public void Put(T value)
        {
            int index = FindSlotByComparer(value, slot => !slot.HasValue || slot.Value.Equals(value));

            if (!_values[index].HasValue)
            {
                _values[index] = new ValueSlot<T>(value);
                ++_count;
            }
        }

        // Exercise 10, task 1, time complexity O(n), Omega(1), space complexity O(1), where n depend on size
        public bool Get(T value)
        {
            int index = FindSlotByComparer(value, slot => slot.HasValue && slot.Value.Equals(value));
            
            return index != -1;
        }

        // Exercise 10, task 2, time complexity O(n), Omega(1), space complexity O(1), where n depend on size
        public bool Remove(T value)
        {
            int index = FindSlotByComparer(value, slot => slot.HasValue && slot.Value.Equals(value));
            bool result = false;
            
            if (index != -1)
            {
                _values[index] = new EmptySlot<T>();
                --_count;
                result = true;
            }
            
            return result;
        }

        // Exercise 10, task 2, time complexity O(n^2), Omega(n), where n depend on size, space complexity O(n)
        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            
            IEnumerable<Slot<T>> intersection = _values.Where(slot => slot.HasValue && set2.Get(slot.Value));

            //in order to avoid unnecessary allocations for .ToList().ForEach(...)
            foreach (Slot<T> slot in intersection)
            {
                result.Put(slot.Value);
            }
            
            return result;
        }

        // Exercise 10, task 2, time complexity O(n^2), Omega(n), where n depend on size, space complexity O(n)
        public PowerSet<T> Union(PowerSet<T> set2)
        {
            PowerSet<T> result = set2.Difference(this);
            IEnumerable<Slot<T>> values = _values.Where(slot => slot.HasValue);

            //in order to avoid unnecessary allocations for .ToList().ForEach(...)
            foreach (Slot<T> slot in values)
            {
                result.Put(slot.Value);
            }
            
            return result;
        }

        // Exercise 10, task 2, time complexity O(n^2), Omega(n), where n depend on size, space complexity O(n)
        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            PowerSet<T> result = new PowerSet<T>();
            
            IEnumerable<Slot<T>> difference = _values.Where(slot => slot.HasValue && !set2.Get(slot.Value));

            //in order to avoid unnecessary allocations for .ToList().ForEach(...)
            foreach (Slot<T> slot in difference)
            {
                result.Put(slot.Value);
            }
            
            return result;
        }

        // Exercise 10, task 2, time complexity O(n^2), Omega(n), where n depend on size, space complexity O(n)
        public bool IsSubset(PowerSet<T> set2)
        {
            int subsetSize = set2.Size();
            return Size() >= subsetSize && _values.Count(slot => slot.HasValue && set2.Get(slot.Value)) == subsetSize;
        }

        // Exercise 10, task 2, time complexity O(n^2), Omega(n), where n depend on size, space complexity O(n)
        public bool Equals(PowerSet<T> set2)
        {
            return Size() == set2.Size() && _values.Where(slot => slot.HasValue).All(slot => set2.Get(slot.Value));
        }
        
        private int FindSlotByComparer(T value, Predicate<Slot<T>> comparer)
        {
            return value is string stringValue 
                ? FindSlotByComparerForString(stringValue, comparer) 
                : FindSlotByComparerGeneric(value, comparer);
        }
        
        private int FindSlotByComparerGeneric(T value, Predicate<Slot<T>> comparer)
        {
            int hash1 = DoubleHashFun1(value, _values.Length);
            int hash2 = DoubleHashFun2(value, _values.Length);

            for (int iteration = 0, index = hash1 ; iteration < _values.Length; ++iteration, index = GetNextIndex(index, hash1, hash2, _values.Length))
            {
                if (comparer.Invoke(_values[index]))
                {
                    return index;
                }
            }

            return -1;
        }

        private int FindSlotByComparerForString(string value, Predicate<Slot<T>> comparer)
        {
            int hash = StringHashFun(value);
            
            for (int iteration = 0, index = hash; iteration < _size; ++iteration, index = (index + 3) % _size)
            {
                if (comparer.Invoke(_values[index]))
                {
                    return index;
                }
            }
            
            return -1;
        }
        
        private int StringHashFun(string key)
        {
            int hash = key.Sum(c => c);
            
            return Math.Abs(hash % _size);
        }
        
        private int GetNextIndex(int index, int hash1, int hash2, int size)
        {
            return (hash1 + index * hash2) % size;
        }
        
        private int DoubleHashFun1(T value, int size)
        {
            return Math.Abs(value.GetHashCode()) % size;
        }
        
        private int DoubleHashFun2(T value, int size)
        {
            return 1 + (Math.Abs(value.GetHashCode()) % (size - 2));
        }
    }

    public abstract class Slot<T>
    {
        public abstract T Value { get; }

        public abstract bool HasValue { get; }
    }
    
    public class ValueSlot<T> : Slot<T>
    {
        public override T Value { get; }
        
        public ValueSlot(T value)
        {
            Value = value;
        }
        
        public override bool HasValue => true;
    }
    
    public class EmptySlot<T> : Slot<T>
    {
        public override T Value { get; } = default;

        public override bool HasValue => false;
    }
}