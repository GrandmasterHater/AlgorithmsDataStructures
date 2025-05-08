using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{

    // Exercise 9, task 2
    public class NativeDictionary<T>
    {
        public int size;
        public string [] slots;
        public T [] values;
        
        private readonly int step = 3;

        public NativeDictionary(int sz)
        {
            size = sz;
            slots = new string[size];
            values = new T[size];
        }

        // Exercise 9, task 3, time complexity O(n), space complexity O(1)
        public int HashFun(string key)
        {
            int hash = key.Sum(c => c);
            
            return Math.Abs(hash % size);
        }

        // Exercise 9, task 3, time complexity O(n), Omega(1), space complexity O(1), where n depend on size
        public bool IsKey(string key)
        {
            int index = FindSlotIndex(key);
            
            return index >= 0;
        }

        // Exercise 9, task 3, time complexity O(n), Omega(1), space complexity O(1), where n depend on size
        public void Put(string key, T value)
        {
            int hash = HashFun(key);
            
            for (int iteration = 0, index = hash; iteration < size; ++iteration, index = GetNextIndex(index))
            {
                if (slots[index] == key || slots[index] == null)
                {
                    slots[index] = key;
                    values[index] = value;
                    break;
                }
            }
        }

        // Exercise 9, task 3, time complexity O(n), Omega(1), space complexity O(1), where n depend on size
        public T Get(string key)
        {
            int index = FindSlotIndex(key);
            
            T result = index >= 0 ? values[index] : default(T);
            
            return result;
        }

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
            return (currentIndex + step) % size;
        }
    } 
}

