using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{

    class Deque<T>
    {
        private LinkedList<T> _linkedList = new LinkedList<T>();
        
        public Deque()
        {
            _linkedList = new LinkedList<T>();
        }

        // Exercise 6, task 1, time complexity O(1), space complexity O(1)
        public void AddFront(T item)
        {
            _linkedList.AddFirst(item);
        }

        // Exercise 6, task 1, time complexity O(1), space complexity O(1)
        public void AddTail(T item)
        {
            _linkedList.AddLast(item);
        }

        // Exercise 6, task 1, time complexity O(1), space complexity O(1)
        public T RemoveFront()
        {
            T result = default(T);

            if (_linkedList.First != null)
            {
                result = _linkedList.First.Value;
                _linkedList.RemoveFirst();
            }
            
            return result;
        }

        // Exercise 6, task 1, time complexity O(1), space complexity O(1)
        public T RemoveTail()
        {
            T result = default(T);

            if (_linkedList.Last != null)
            {
                result = _linkedList.Last.Value;
                _linkedList.RemoveLast();
            }
            
            return result;
        }
        
        // Exercise 6, task 1, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _linkedList.Count; // размер очереди
        }
    }

}