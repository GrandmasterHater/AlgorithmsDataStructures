using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public static class DequeTask6_2
    {
        // Exercise 6, task 4, time complexity O(n), space complexity O(n)
        public static bool IsPalindrome(this string data)
        {
            data = data ?? throw new ArgumentNullException();
            
            Deque<char> deque = new Deque<char>();
            
            foreach (char c in data.ToLower())
            {
                if (char.IsLetterOrDigit(c))
                {
                    deque.AddTail(c);
                }
            }
            
            bool isSymbolsEquals = false;
            
            while (deque.Size() > 1)
            {
                isSymbolsEquals = deque.RemoveFront() == deque.RemoveTail();
                
                if (!isSymbolsEquals)
                {
                    break;
                }
            }
            
            return isSymbolsEquals;
        }
    }
    
    public class DequeWithMin
    {
        private LinkedList<int> _linkedList = new LinkedList<int>();
        private LinkedList<int> _minValuesList = new LinkedList<int>();
        
        public DequeWithMin()
        {
            _linkedList = new LinkedList<int>();
            _minValuesList = new LinkedList<int>();
        }
        
        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        public int GetMinValue()
        {
            int minValue = default(int);
            
            if (_linkedList.Count > 0)
            {
                minValue = _minValuesList.First.Value < _minValuesList.Last.Value ? _minValuesList.First.Value : _minValuesList.Last.Value;
            }
            
            return minValue;
        }

        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        public void AddFront(int item)
        {
            _linkedList.AddFirst(item);
            
            var minValue = GetMinValue(item);

            _minValuesList.AddFirst(minValue);
        }

        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        private int GetMinValue(int item)
        {
            int minFirstValue = _minValuesList.First != null && _minValuesList.First.Value < item ? _minValuesList.First.Value : item;
            int minLastValue = _minValuesList.Last != null && _minValuesList.Last.Value < item ? _minValuesList.Last.Value : item;
            return minFirstValue < minLastValue ? minFirstValue : minLastValue;
        }

        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        public void AddTail(int item)
        {
            _linkedList.AddLast(item);
            
            var minValue = GetMinValue(item);
            
            _minValuesList.AddLast(minValue);
        }

        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        public int RemoveFront()
        {
            int result = default(int);

            if (_linkedList.First != null)
            {
                result = _linkedList.First.Value;
                _linkedList.RemoveFirst();
                _minValuesList.RemoveFirst();
            }
            
            return result;
        }

        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        public int RemoveTail()
        {
            int result = default(int);

            if (_linkedList.Last != null)
            {
                result = _linkedList.Last.Value;
                _linkedList.RemoveLast();
                _minValuesList.RemoveLast();
            }
            
            return result;
        }
        
        // Exercise 6, task 5, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _linkedList.Count; // размер очереди
        }
    }

    public class DequeWithDynArrayPrinciple<T>
    {
        public const int MIN_CAPACITY = 16;
        public const int CAPACITY_MULTIPLIER = 2;
        public const float CAPACITY_DIVIDER = 1.5f;
        public const float FILLING_PERCENT_FOR_REDUCE = 0.5f;

        private T[] _buffer;
        private int _firstIndex;
        private int _count;
        
        public int Capacity => _buffer.Length;
        
        public DequeWithDynArrayPrinciple()
        {
            _buffer = new T[MIN_CAPACITY];

            _firstIndex = 0;
            _count = 0;
        }
        
        // Exercise 6, task 6, time complexity O(n), o(1), space complexity O(n), o(1)
        public void AddFront(T item)
        {
            bool isExtendRequired = IsExtendCapacityRequired();

            if (isExtendRequired)
            {
                ExtendCapacity();
            }
            
            _firstIndex = GetIncrementedIndex(_buffer, _firstIndex);

            _buffer[_firstIndex] = item;
            ++_count;
        }

        // Exercise 6, task 6, time complexity O(n), o(1), space complexity O(n), o(1)
        public void AddTail(T item)
        {
            bool isExtendRequired = IsExtendCapacityRequired();

            if (isExtendRequired)
            {
                ExtendCapacity();
            }

            int lastIndex = GetLastIndex();
            lastIndex = GetDecrementedIndex(_buffer, lastIndex);
            _buffer[lastIndex] = item;
            ++_count;
        }

        // Exercise 6, task 6, time complexity O(n), o(1), space complexity O(n), o(1)
        public T RemoveFront()
        {
            T result = default(T);
            
            if (_count == 0)
            {
                return result;
            }

            result = _buffer[_firstIndex];
            _buffer[_firstIndex] = default(T);
            _firstIndex = GetDecrementedIndex(_buffer, _firstIndex);
            --_count;
            
            bool isReduceCapacityRequired = IsReduceCapacityRequired();

            if (isReduceCapacityRequired)
            {
                ReduceCapacity();
            }

            return result;
        }

        // Exercise 6, task 6, time complexity O(n), o(1), space complexity O(n), o(1)
        public T RemoveTail()
        {
            T result = default(T);
            
            if (_count == 0)
            {
                return result;
            }

            int lastIndex = GetLastIndex();
            result = _buffer[lastIndex];
            _buffer[lastIndex] = default(T);
            --_count;
            
            bool isReduceCapacityRequired = IsReduceCapacityRequired();

            if (isReduceCapacityRequired)
            {
                ReduceCapacity();
            }

            return result;
        }
        
        // Exercise 6, task 6, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _count;
        }
        
        private int GetIncrementedIndex(T[] buffer, int currentIndex)
        {
            return (currentIndex + 1) % buffer.Length;
        }
        
        private int GetDecrementedIndex(T[] buffer, int currentIndex)
        {
            int nextTailIndex = currentIndex - 1;
            
            return nextTailIndex < 0 ? nextTailIndex + buffer.Length : nextTailIndex;
        }

        private int GetLastIndex()
        {
            int tailIndex = _firstIndex - (_count - 1);
            return tailIndex < 0 ? tailIndex + _buffer.Length : tailIndex;
        }

        private bool IsExtendCapacityRequired() => _buffer.Length == _count;
        
        private void ExtendCapacity()
        {
            MakeArray(_buffer.Length * CAPACITY_MULTIPLIER);
        }

        private bool IsReduceCapacityRequired()
        {
            float fillingPercent = (float)_count / _buffer.Length;
            
            return fillingPercent < FILLING_PERCENT_FOR_REDUCE;
        }
        
        private void ReduceCapacity()
        {
            int calculatedCapacity = (int)(_buffer.Length / CAPACITY_DIVIDER);
            MakeArray(calculatedCapacity);
        }
        
        private void MakeArray(int newCapacity)
        {
            newCapacity = newCapacity < MIN_CAPACITY ? MIN_CAPACITY : newCapacity;
            
            int oldBufferLength = _buffer.Length;
            bool isBufferExtending = newCapacity > oldBufferLength;
            int lastIndex = GetLastIndex();
            
            if (!isBufferExtending && _firstIndex < lastIndex)
            {
                int shiftLength = oldBufferLength - lastIndex;
                int newLastIndex = newCapacity - shiftLength;
                Array.Copy(_buffer, lastIndex, _buffer, newLastIndex, shiftLength);
            }

            if (!isBufferExtending && _firstIndex >= lastIndex && lastIndex > 0)
            {
                int newLastIndex = 0;
                int shiftLength = _firstIndex - lastIndex + 1;
                Array.Copy(_buffer, lastIndex, _buffer, newLastIndex, shiftLength);
                _firstIndex = shiftLength - 1;
            }
            
            Array.Resize(ref _buffer, newCapacity);
            
            if (isBufferExtending && _firstIndex < lastIndex)
            {
                int shiftLength = oldBufferLength - lastIndex;
                int newLastIndex = newCapacity - shiftLength;
                Array.Copy(_buffer, lastIndex, _buffer, newLastIndex, shiftLength);
            }
        }
    }
}