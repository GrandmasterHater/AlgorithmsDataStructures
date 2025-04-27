using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class Stack<T>
    {
        private LinkedList<T> _linkedList;
        
        public Stack()
        {
            _linkedList = new LinkedList<T>();
        } 

        // Exercise 4, task 1, time complexity O(1) (Depends on LinkedList implementation), space complexity O(1)
        public int Size() 
        {
            return _linkedList.Count;
        }
        
        // Exercise 4, task 1, time complexity O(1), space complexity O(1)
        public T Pop()
        {
            T result = default(T);

            if (_linkedList.Last != null)
            {
                result = _linkedList.Last.Value;
                _linkedList.RemoveLast();
            }
            
            return result;
        }
        
        // Exercise 4, task 1, time complexity O(1), space complexity O(1)
        public void Push(T val)
        {
            _linkedList.AddLast(val);
        }

        // Exercise 4, task 1, time complexity O(1), space complexity O(1)
        public T Peek()
        {
            T result = default(T);

            if (_linkedList.Last != null)
            {
                result = _linkedList.Last.Value;
            }
            
            return result;
        }
    }

}