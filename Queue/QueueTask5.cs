using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Queue<T>
    {
        private LinkedList<T> _linkedList;
        
        public Queue()
        {
            _linkedList = new LinkedList<T>();
        } 

        // Exercise 5, task 2, time complexity O(1), space complexity O(1)
        public void Enqueue(T item)
        {
            _linkedList.AddFirst(item);
        }

        // Exercise 5, task 2, time complexity O(1), space complexity O(1)
        public T Dequeue()
        {
            T result = default(T);

            if (_linkedList.Last != null)
            {
                result = _linkedList.Last.Value;
                _linkedList.RemoveLast();
            }
            
            return result;
        }

        // Exercise 5, task 2, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _linkedList.Count;
        }

    }
}

