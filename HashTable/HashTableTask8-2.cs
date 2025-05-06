using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsDataStructures
{
    public static class HashTableUtils
    {
        public static readonly int Prime = 1395361;
        public static int[] Primes { get; } = 
        {
            17, 37, 79, 163, 331, 673, 1361, 2729, 5441, 10889,
            21787, 43591, 87187, 174389, 348823, 697669, 1395361,
            2790737, 5581511, 11163067, 22326161, 44652353, 89304751,
            178609517, 357219079, 714438223, 1428876577, 2143314823
        };
        
        public static int GetNearestGreaterPrime(int number)
        {
            foreach (int prime in Primes)
            {
                if (prime >= number)
                    return prime;
            }

            throw new ArgumentException("Number is too big!");
        }
    }
    
    // Exercise 8, task 3 and task 4
    public class DynHashTable
    {
        public int _size;
        public string[] _slots;
        private int _count;
        
        private static float EXTEND_LOAD_FACTOR = 0.7f;

        private float LoadFactor => _count / (float)_size;

        public DynHashTable(int sz)
        {
            _size = sz;
            _slots = new string[_size];
            
            for(int i = 0; i < _size; i++) 
                _slots[i] = null;
        }

        // Exercise 8, task 3, time complexity O(n^2), o(n), Omega(n), space complexity O(1), o(n)
        public int SeekSlot(string value)
        {
            if (LoadFactor > EXTEND_LOAD_FACTOR)
            {
                Extend();
            }
            
            return FindSlotByComparer(_slots, value, arg1 => arg1 == null);;
        }

        // Exercise 8, task 3, time complexity O(n^2), o(n), Omega(n), space complexity O(1), o(n)
        public int Put(string value)
        {
            int index = SeekSlot(value);

            if (index >= 0)
                _slots[index] = value;
            
            ++_count;
            
            return index;
        }

        // Exercise 8, task 3, time complexity O(n), o(1), Omega(1), space complexity O(1), n depend on size
        public int Find(string value)
        {
            return FindSlotByComparer(_slots, value, (arg1, arg2) => arg1 == arg2);
        }

        private int FindSlotByComparer(string[] slots, string value, Func<string, string, bool> comparer)
        {
            int hash1 = DoubleHashFun1(value, slots.Length);
            int hash2 = DoubleHashFun2(value, slots.Length);
            
            for (int iteration = 0, index = hash1 ; iteration < slots.Length; ++iteration, index = GetNextIndex(index, hash1, hash2, slots.Length))
            {
                if (comparer.Invoke(slots[index], value))
                {
                    return index;
                }
            }
            
            return -1;
        }
        
        private int FindSlotByComparer(string[] slots, string value, Predicate<string> comparer)
        {
            int hash1 = DoubleHashFun1(value, slots.Length);
            int hash2 = DoubleHashFun2(value, slots.Length);
            
            for (int iteration = 0, index = hash1 ; iteration < slots.Length; ++iteration, index = GetNextIndex(index, hash1, hash2, slots.Length))
            {
                if (comparer.Invoke(slots[index]))
                {
                    return index;
                }
            }
            
            return -1;
        }
        
        // Exercise 8, task 4
        private int DoubleHashFun1(string value, int size)
        {
            int hash = 0;
            
            foreach (char val in value)
            {
                hash += val;
            }
            
            return Math.Abs(hash % size);
        }
        
        // Exercise 8, task 4
        private int DoubleHashFun2(string value, int size)
        {
            return 1 + DoubleHashFun1(value, size) % (size - 1);
        }
        
        // Exercise 8, task 3, time complexity O(n^2), space complexity O(n)
        private void Extend()
        {
            int newSize = HashTableUtils.GetNearestGreaterPrime(_size * 2);
            string[] newSlots = new string[newSize];
                
            for(int i = 0; i < newSize; ++i) 
                newSlots[i] = null;
                
            for (int i = 0; i < _size; ++i)
            {
                string value = _slots[i];
                
                if (value != null)
                {
                    int newIndex = FindSlotByComparer(newSlots, value, arg1 => arg1 == null);
                    newSlots[newIndex] = value;
                }
            }
                
            _slots = newSlots;
            _size = newSize;
        }
        
        private int GetNextIndex(int index, int hash1, int hash2, int size)
        {
            return (hash1 + index * hash2) % size;
        }
    }
    
    public class SaltedHashTable
    {
        public int size;
        public int step;
        public string [] slots;
        public byte[] _salt = { 10, 9, 4, 93};

        public SaltedHashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            slots = new string[size];
            
            for(int i=0; i<size; i++) 
                slots[i] = null;
        }

        // Exercise 8, task 5, time complexity O(n), space complexity O(1)
        public int HashFun(string value)
        {
            int hash = 0;

            for (var index = 0; index < value.Length; index++)
            {
                hash += index < _salt.Length ? value[index] ^ _salt[index] : value[index];
            }

            return Math.Abs(hash % size);
        }

        // Exercise 8, task 5, time complexity O(n), space complexity O(1), n depend on size
        public int SeekSlot(string value)
        {
            int hash = HashFun(value);
            
            for (int iteration = 0, index = hash ; iteration < size; ++iteration, index = GetNextIndex(index, step))
            {
                if (slots[index] == null)
                {
                    return index;
                }
            }
            
            return -1;
        }

        // Exercise 8, task 5, time complexity O(n), space complexity O(1), n depend on size
        public int Put(string value)
        {
            int index = SeekSlot(value);

            if (index >= 0)
                slots[index] = value;
            
            return index;
        }

        // Exercise 8, task 5, time complexity O(n), o(1), Omega(1), space complexity O(1), n depend on size
        public int Find(string value)
        {
            int hash = HashFun(value);
            
            for (int iteration = 0, index = hash ; iteration < size; ++iteration, index = GetNextIndex(index, step))
            {
                if (slots[index] == value)
                {
                    return index;
                }
            }
            
            return -1;
        }

        private int GetNextIndex(int currentIndex, int step)
        {
            return (currentIndex + step) % size;
        }
    }
}