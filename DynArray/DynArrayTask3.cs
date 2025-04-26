using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class DynArray<T>
    {
        public T [] array;
        public int count;
        public int capacity;
        
        public const int MIN_CAPACITY = 16;
        public const int CAPACITY_MULTIPLIER = 2;
        public const float CAPACITY_DIVIDER = 1.5f;
        public const float FILLING_PERCENT_FOR_REDUCE = 0.5f;

        public DynArray()
        {
            count = 0;
            MakeArray(16);
        }
        
        // Exercise 3, task 1, time complexity O(n), space complexity O(n)
        public void MakeArray(int new_capacity)
        {
            int newCapacity = new_capacity < MIN_CAPACITY ? MIN_CAPACITY : new_capacity;
            
            Array.Resize(ref array, newCapacity);
            capacity = newCapacity;

            if (count > newCapacity)
            {
                count = newCapacity;
            }
        }

        // Exercise 3, task 1, time complexity O(1), space complexity O(1)
        public T GetItem(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }
                
            return array[index];
        }

        // Exercise 3, task 1, time complexity O(n), o(1), space complexity O(n), o(1)
        public void Append(T itm)
        {
            bool isExtendRequired = IsExtendCapacityRequired(count, capacity);
            
            if (isExtendRequired)
            {
                ExtendCapacity(capacity);
            }
            
            array[count] = itm;
            ++count;
        }

        // Exercise 3, task 2, time complexity: O(n), o(n), Omega(1)   space complexity: O(n), o(n), Omega(1)
        public void Insert(T itm, int index)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException();
            }
            
            bool isExtendRequired = IsExtendCapacityRequired(count, capacity);
            
            if (isExtendRequired)
            {
                ExtendCapacity(capacity);
            }

            if (index != count)
            {
                Array.Copy(array, index, array, index + 1, count - index);
            }

            array[index] = itm;
            ++count;
        }

        // Exercise 3, task 3, time complexity: O(n), o(n), Omega(1)   space complexity: O(n), o(n), Omega(1)
        public void Remove(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index < count - 1)
            {
                int nextIndex = index + 1;
                Array.Copy(array, nextIndex, array, index, count - nextIndex);
            }
            
            --count;

            bool isReduceRequired = IsReduceCapacityRequired(count, capacity);
                
            if (isReduceRequired)
            {
                ReduceCapacity(capacity);
            }
        }
        
        private bool IsExtendCapacityRequired(int currentCount, int currentCapacity) => currentCount == currentCapacity;

        private void ExtendCapacity(int currentCapacity)
        {
            MakeArray(currentCapacity * CAPACITY_MULTIPLIER);
        }

        private bool IsReduceCapacityRequired(int currentCount, int currentCapacity)
        {
            float fillingPercent = (float)currentCount / currentCapacity;
            
            return fillingPercent < FILLING_PERCENT_FOR_REDUCE;
        }
        
        private void ReduceCapacity(int currentCapacity)
        {
            int calculatedCapacity = (int)(currentCapacity / CAPACITY_DIVIDER);
            int newCapacity = calculatedCapacity < MIN_CAPACITY ? MIN_CAPACITY : calculatedCapacity;
            MakeArray(newCapacity);
        }
    }
}