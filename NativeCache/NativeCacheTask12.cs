using System;
using System.Linq;

namespace AlgorithmsDataStructures
{
    class NativeCache<T>
    {
        public int size;
        public string [] slots;
        public T [] values;
        public int [] hits;
        
        private int _count;

        public NativeCache(int size)
        {
            this.size = size;
            _count = 0;
            slots = new string[size];
            values = new T[size];
            hits = new int[size];
        }
        
        // Exercise 12, time complexity O(n), space complexity O(1), where n key length
        public int HashFun(string key)
        {
            int hash = key.Sum(c => c);
            
            return Math.Abs(hash % size);
        }

        // Exercise 12, time complexity O(n), Omega(1), space complexity O(1), where n items count
        public bool IsKey(string key)
        {
            int index = FindSlotIndex(key);
            
            return index >= 0;
        }

        // Exercise 12, time complexity O(n), Omega(1), space complexity O(1), where n items count
        public void Put(string key, T value)
        {
            int hash = HashFun(key);
            
            for (int iteration = 0, index = hash; iteration < size; ++iteration, index = GetNextIndex(index))
            {
                if (slots[index] == key || slots[index] == null)
                {
                    AddValue(index, key, value);
                    return;
                }
            }
            
            DisplaceItem();
            Put(key, value);
        }
        
        // Exercise 12, time complexity O(n), Omega(1), space complexity O(1), where n items count
        public T Get(string key)
        {
            int index = FindSlotIndex(key);
            
            return index >= 0 ? values[index] : default(T);
        }
        
        // Exercise 12, time complexity O(n), Omega(1), space complexity O(1), where n items count
        public int HitsCount(string key)
        {
            int index = FindSlotIndex(key);
            
            return index >= 0 ? hits[index] : -1;
        }

        private void DisplaceItem()
        {
            int minValueIndex = 0;
            int minValue = int.MaxValue;
            
            for (int index = 0; index < hits.Length; ++index)
            {
                if (hits[index] < minValue)
                {
                    minValueIndex = index;
                    minValue = hits[index];
                }
            }
            
            slots[minValueIndex] = null;
            hits[minValueIndex] = 0;
            values[minValueIndex] = default;
            --_count;
        }

        private void AddValue(int index, string key, T value)
        {
            bool isSlotEmpty = slots[index] == null;
            
            if (isSlotEmpty)
            {
                slots[index] = key;
                values[index] = value;
                ++_count;
            }

            if (!isSlotEmpty && !values[index].Equals(value))
            {
                values[index] = value;
                hits[index] = 0;
            }
            
            IncreaseValueHits(index);
        }

        private void IncreaseValueHits(int index) => ++hits[index];

        private int FindSlotIndex(string key)
        {
            int hash = HashFun(key);

            for (int iteration = 0, index = hash; iteration < size; ++iteration, index = GetNextIndex(index))
            {
                if (slots[index] == key)
                {
                    return index;
                }
            }

            return -1;
        }
        
        private int GetNextIndex(int currentIndex)
        {
            return (currentIndex + 3) % size;
        }
    }
}

