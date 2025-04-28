using System;

namespace AlgorithmsDataStructures
{
    public static class QueueTask5_2
    {
        // Exercise 5, task 3, time complexity O(n), space complexity O(1)
        public static void Rotate<T>(this Queue<T> queue, int rotateCount)
        {
            int queueSize = queue.Size();

            if (queueSize == 0)
            {
                return;
            }
            
            int iterationsCount = Math.Abs(rotateCount % queueSize);

            if (rotateCount < 0)
            {
                iterationsCount = queueSize - iterationsCount;
            }
            
            for (int i = 0; i < iterationsCount; i++)
            {
                queue.Enqueue(queue.Dequeue());
            }
        }
        
        // Exercise 5, task 5, time complexity O(n), space complexity O(n)
        public static void Reverse<T>(this Queue<T> queue)
        {
            int queueSize = queue.Size();

            if (queueSize == 0)
            {
                return;
            }
            
            Stack<T> stack = new Stack<T>();

            for (int i = 0; i < queueSize; ++i)
            {
                stack.Push(queue.Dequeue());
            }
            
            for (int i = 0; i < queueSize; ++i)
            {
                queue.Enqueue(stack.Pop());
            }
        }
    }
    
    // Exercise 5, task 4
    public class QueueOnStacks<T>
    {
        private Stack<T> _enqueueStack;
        private Stack<T> _dequeueStack;
        
        public QueueOnStacks()
        {
            _enqueueStack = new Stack<T>();
            _dequeueStack = new Stack<T>();
        } 

        // Exercise 5, task 4, time complexity O(1), space complexity O(1)
        public void Enqueue(T item)
        {
            _enqueueStack.Push(item);
        }

        // Exercise 5, task 4, time complexity O(n), space complexity O(1)
        public T Dequeue()
        {
            T result = default(T);

            if (_enqueueStack.Size() < 0)
            {
                return result;
            }
            
            while (_enqueueStack.Size() > 0)
            {
                _dequeueStack.Push(_enqueueStack.Pop());
            }
            
            result = _dequeueStack.Pop();
            
            while (_dequeueStack.Size() > 0)
            {
                _enqueueStack.Push(_dequeueStack.Pop());
            }
            
            return result;
        }

        // Exercise 5, task 4, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _enqueueStack.Size();
        }
    }

    // Exercise 5, task 6
    public class CircularQueue<T>
    {
        private readonly T[] _buffer;
        private int _firstIndex;
        private int _count;
        
        public CircularQueue(int size)
        {
            if (size <= 0)
            {
                throw new AggregateException();
            }
            
            _buffer = new T[size];
            _firstIndex = 0;
            _count = 0;
        }

        // Exercise 5, task 6, time complexity O(1), space complexity O(1)
        public bool IsFull()
        {
            return _buffer.Length == _count;
        }
        
        // Exercise 5, task 6, time complexity O(1), space complexity O(1)
        public void Enqueue(T item)
        {
            if (IsFull())
            {
                throw new InvalidOperationException();
            }
            
            _firstIndex = GetNextIndex(_firstIndex, _buffer);
            _buffer[_firstIndex] = item;
            ++_count;
        }

        // Exercise 5, task 6, time complexity O(1), space complexity O(1)
        public T Dequeue()
        {
            T result = default;
            
            if (_count > 0)
            {
                int tailIndex = _firstIndex - (_count - 1);
                tailIndex = tailIndex < 0 ? tailIndex + _buffer.Length : tailIndex;
                --_count;

                result = _buffer[tailIndex];
                _buffer[tailIndex] = default;
            }
            
            return result;
        }

        // Exercise 5, task 6, time complexity O(1), space complexity O(1)
        public int Size()
        {
            return _count;
        }

        private int GetNextIndex(int currentIndex, T[] buffer)
        {
            return (currentIndex + 1) % buffer.Length;
        }
    }
}