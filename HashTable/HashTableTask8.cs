using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsDataStructures
{

    public class HashTable
    {
        public int size;
        public int step;
        public string [] slots; 

        public HashTable(int sz, int stp)
        {
            size = sz;
            step = stp;
            slots = new string[size];
            for(int i=0; i<size; i++) slots[i] = null;
        }

        // Exercise 8, time complexity O(n), space complexity O(1)
        public int HashFun(string value)
        {
            int hash = 0;
            
            foreach (char val in value)
            {
                hash += val;
            }
            
            return Math.Abs(hash % size);
        }

        // Exercise 8, time complexity O(n), space complexity O(1), n depend on size
        public int SeekSlot(string value)
        {
            int hash = HashFun(value);
            
            for (int iteration = 0, index = hash ; iteration < size; ++iteration, index = GetNextIndex(index, step, size))
            {
                if (slots[index] == null)
                {
                    return index;
                }
            }
            
            return -1;
        }

        // Exercise 8, time complexity O(n), space complexity O(1), n depend on size
        public int Put(string value)
        {
            int index = SeekSlot(value);

            if (index >= 0)
                slots[index] = value;
            
            return index;
        }

        // Exercise 8, time complexity O(n), o(1), Omega(1), space complexity O(1), n depend on size
        public int Find(string value)
        {
            int hash = HashFun(value);
            
            for (int iteration = 0, index = hash ; iteration < size; ++iteration, index = GetNextIndex(index, step, size))
            {
                if (slots[index] == value)
                {
                    return index;
                }
            }
            
            return -1;
        }

        private int GetNextIndex(int currentIndex, int step, int bufferSize)
        {
            return (currentIndex + step) % size;
        }
    }
 
}