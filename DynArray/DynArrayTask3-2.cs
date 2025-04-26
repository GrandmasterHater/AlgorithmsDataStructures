using System;

namespace AlgorithmsDataStructures
{
    // Exercise 3, task 6
    public class DynArrayBank<T>
    {
        public T [] array;
        public int count;
        public int capacity;
        
        public const int MIN_CAPACITY = 16;
        public const int CAPACITY_MULTIPLIER = 2;
        public const float CAPACITY_DIVIDER = 1.5f;
        public const float FILLING_PERCENT_FOR_REDUCE = 0.5f;

        public const int AMORTIZED_COST = 3;
        public const int REAL_COST = 1;

        public int balance;
        public int reallocationCost;

        public DynArrayBank()
        {
            count = 0;
            MakeArray(MIN_CAPACITY);
            balance = 0;
        }

        public void MakeArray(int new_capacity)
        {
            int newCapacity = new_capacity < MIN_CAPACITY ? MIN_CAPACITY : new_capacity;
            
            Array.Resize(ref array, newCapacity);
            capacity = newCapacity;

            if (count > newCapacity)
            {
                count = newCapacity;
            }
            
            for (int value = 1, power = 0; value <= newCapacity; value *= 2, ++power)
            {
                reallocationCost = power;
            }

            balance -= reallocationCost;
        }

        public T GetItem(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }
                
            return array[index];
        }

        public void Append(T itm)
        {
            balance += AMORTIZED_COST;
            bool isExtendRequired = IsExtendCapacityRequired(count, capacity);
            
            if (isExtendRequired)
            {
                ExtendCapacity(capacity);
            }
            
            array[count] = itm;
            ++count;
            
            balance -= REAL_COST;
        }

        public void Insert(T itm, int index)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException();
            }

            balance += AMORTIZED_COST;
            
            bool isExtendRequired = IsExtendCapacityRequired(count, capacity);
            
            if (isExtendRequired)
            {
                ExtendCapacity(capacity);
            }

            if (index != count)
            {
                int itemsCount = count - index;
                Array.Copy(array, index, array, index + 1, itemsCount);
                balance -= itemsCount;
            }
            
            array[index] = itm;
            ++count;
            balance -= REAL_COST;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }
            if (index < count - 1)
            {
                int nextIndex = index + 1;
                Array.Copy(array, nextIndex, array, index, count - index);
            }
            
            --count;

            bool isReduceRequired = IsReduceCapacityRequired(count, capacity);
                
            if (isReduceRequired)
            {
                ReduceCapacity(capacity);
            }
        }

        private bool IsExtendCapacityRequired(int currentCount, int currentCapacity) => currentCount == currentCapacity || balance > reallocationCost;

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
    
    // Exercise 3, task 7
    public class MultyDynArray<T> 
    {
        public DynArray<object> array;
        public int count;
        public int capacity;
        public int dimensionsCount;
        
        public const int MIN_CAPACITY = 16;
        public const int CAPACITY_MULTIPLIER = 2;
        public const float CAPACITY_DIVIDER = 1.5f;
        public const float FILLING_PERCENT_FOR_REDUCE = 0.5f;

        public MultyDynArray(params int[] sizes)
        {
            if (sizes.Length == 0)
            {
                throw new InvalidOperationException();
            }
            
            count = 0;
            dimensionsCount = sizes.Length;
            array = new DynArray<object>();
            MakeArray(sizes);
        }

        public void MakeArray(params int[] new_capacity)
        {
            capacity = 0;
            count = 0;
            MakeArrayDimension(0, array, new_capacity);
        }
        
        private void MakeArrayDimension(int currentDimension, DynArray<object> dimension, int[] new_capacity)
        {
            dimension.MakeArray(new_capacity[currentDimension]);
            
            if (currentDimension == dimensionsCount - 1)
            {
                capacity += dimension.capacity;
                count += dimension.count;
                return;
            }
            
            for (int index = 0; index < dimension.capacity; ++index)
            {
                DynArray<object> nextDimension = dimension.array[index] as DynArray<object>;

                if (nextDimension == null)
                {
                    nextDimension = new DynArray<object>();
                    dimension.Insert(new DynArray<object>(), index);
                }
                
                MakeArrayDimension(currentDimension + 1, nextDimension, new_capacity);
            }
        }

        public T GetItem(params int[] index)
        {
            if (index.Length != dimensionsCount)
            {
                throw new InvalidOperationException();
            }
                
            return GetItemRecursive(array, 0, index);
        }

        public void Insert(T itm, params int[] index)
        {
            if (index.Length != dimensionsCount)
            {
                throw new InvalidOperationException();
            }

            InsertItemRecursive(itm, array, 0, index);
        }

        public void Remove(params int[] index)
        {
            if (index.Length != dimensionsCount)
            {
                throw new InvalidOperationException();
            }

            RemoveItemRecursive(array, 0, index);
        }
        
        private T GetItemRecursive(DynArray<object> dimensionArray, int currentDimension, params int[] index)
        {
            int indexValue = index[currentDimension];
            
            return currentDimension == dimensionsCount - 1
                ? (T)dimensionArray.GetItem(indexValue) 
                : GetItemRecursive((DynArray<object>)dimensionArray.GetItem(indexValue), currentDimension + 1, index);
        }
        
        private void InsertItemRecursive(T itm, DynArray<object> dimensionArray, int currentDimension, params int[] index)
        {
            int indexValue = index[currentDimension];
            
            if (currentDimension == dimensionsCount - 1)
            {
                int oldCapacity = dimensionArray.capacity;
                dimensionArray.Insert(itm, indexValue);
                ++count;
                capacity += dimensionArray.capacity - oldCapacity;
                return;
            }
            
            DynArray<object> nextDimensionArray = dimensionArray.GetItem(indexValue) as DynArray<object>;
            
            if (nextDimensionArray == null)
            {
                nextDimensionArray = new DynArray<object>();
                dimensionArray.Insert(nextDimensionArray, indexValue);
            }
            
            InsertItemRecursive(itm, nextDimensionArray, currentDimension + 1, index);
        }
        
        private void RemoveItemRecursive(DynArray<object> dimensionArray, int currentDimension, params int[] index)
        {
            int indexValue = index[currentDimension];
            
            if (currentDimension == dimensionsCount - 1)
            {
                int oldCapacity = dimensionArray.capacity;
                dimensionArray.Remove(indexValue);
                --count;
                capacity += dimensionArray.capacity - oldCapacity;
            }
            else
            {
                RemoveItemRecursive((DynArray<object>)dimensionArray.GetItem(indexValue), currentDimension + 1, index);
            }
        }
    }
}