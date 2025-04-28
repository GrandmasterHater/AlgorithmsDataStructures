using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class QueueTask4_2Tests
    {
        // iterationsCount > 0 = rotate left
        // iterationsCount < 0 = rotate right
        [TestCase(new int[]{1, 2, 3, 4, 5}, 3, new int[]{4, 5, 1, 2, 3})] 
        [TestCase(new int[]{1, 2, 3, 4, 5}, 6, new int[]{2, 3, 4, 5, 1})]
        [TestCase(new int[]{1, 2, 3, 4, 5}, -1, new int[]{5, 1, 2, 3, 4})]
        [TestCase(new int[]{1, 2, 3, 4, 5}, -7, new int[]{4, 5, 1, 2, 3})]
        public void RotateQueueTest(int[] data, int iterationsCount, int[] expectedData)
        {
            Queue<int> queue = new Queue<int>();

            for (var index = 0; index < data.Length; ++index)
            {
                queue.Enqueue(data[index]);
            }

            queue.Rotate(iterationsCount);
            
            List<int> result = new List<int>(queue.Size());

            while (queue.Size() > 0)
            {
                result.Add(queue.Dequeue());
            }
            
            Assert.That(result, Is.EqualTo(expectedData));
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 5, 4, 3, 2, 1 }, new int[] { 1, 2, 3, 4, 5 })]
        public void ReverseQueueTest(int[] data, int[] expectedData)
        {
            Queue<int> queue = new Queue<int>();

            for (var index = 0; index < data.Length; ++index)
            {
                queue.Enqueue(data[index]);
            }

            queue.Reverse();
            
            List<int> result = new List<int>(queue.Size());

            while (queue.Size() > 0)
            {
                result.Add(queue.Dequeue());
            }
            
            Assert.That(result, Is.EqualTo(expectedData));
        }
        
        #region QueueOnStacks
    
        [TestCase(new [] {5, 7, 2 }, 5)]
        public void Enqueue_AddValues_FirstEnqueuedValueIsFirstDequeued(int[] data, int expectedValue)
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();
            
            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] {8, 7, 2 }, 8)]
        public void Dequeue_QueueWithValues_FirstDequeuedValueExpected(int[] data, int expectedValue)
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();

            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] {8, 7, 2 }, 2)]
        public void Dequeue_QueueWithValues_SizeDecreased(int[] data, int expectedSize)
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();
            
            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Dequeue_EmptyQueue_GetDefaultResultForType()
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();
            
            int expectedValue = default(int);
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Size_EnqueueValue_SizeIncreased()
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();
            
            int newSize = 1;
            
            _queue.Enqueue(5);
            
            Assert.That(_queue.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Size_DequeueValue_SizeDecreased()
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();
            
            
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            
            int expectedSize = 1;

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_DequeueValueWithEmptyQueue_SizeNotChanged()
        {
            QueueOnStacks<int> _queue = new QueueOnStacks<int>();
            
            int expectedSize = 0;

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
    
        #endregion

        #region CircularQueueTests
        
        [Test]
        public void IsFull_FullQueue_ReturnTrue()
        {
            int size = 3;
            CircularQueue<int> _queue = new CircularQueue<int>(size);
            
            foreach (int number in Enumerable.Range(1, size))
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.IsFull(), Is.True);
        }
        
        [Test]
        public void IsFull_NotFullQueue_ReturnFalse()
        {
            int size = 3;
            CircularQueue<int> _queue = new CircularQueue<int>(size);
            
            foreach (int number in Enumerable.Range(1, size))
            {
                _queue.Enqueue(number);
            }
            
            _queue.Dequeue();
            
            Assert.That(_queue.IsFull(), Is.False);
        }

        [TestCase(5, new [] {5, 7, 2 }, 5)]
        public void Circular_Enqueue_AddValues_FirstEnqueuedValueIsFirstDequeued(int size, int[] data, int expectedValue)
        {
            CircularQueue<int> _queue = new CircularQueue<int>(size);
            
            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Circular_Enqueue_FullQueue_InvalidOperationException()
        {
            int size = 3;
            CircularQueue<int> _queue = new CircularQueue<int>(size);
            
            foreach (int number in Enumerable.Range(1, size))
            {
                _queue.Enqueue(number);
            }
            
            Assert.Catch<InvalidOperationException>(() => _queue.Enqueue(4));
        }
        
        [TestCase(5, new [] {8, 7, 2 }, 8)]
        public void Circular_Dequeue_QueueWithValues_FirstDequeuedValueExpected(int size, int[] data, int expectedValue)
        {
            CircularQueue<int> _queue = new CircularQueue<int>(size);

            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(5, new [] {8, 7, 2 }, 2)]
        public void Circular_Dequeue_QueueWithValues_SizeDecreased(int size, int[] data, int expectedSize)
        {
            CircularQueue<int> _queue = new CircularQueue<int>(size);
            
            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Circular_Dequeue_EmptyQueue_GetDefaultResultForType()
        {
            CircularQueue<int> _queue = new CircularQueue<int>(3);
            
            int expectedValue = default(int);
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Circular_Dequeue_WhenFirstIndexLessThanLastIndex_GetExpectedLastValue()
        {
            CircularQueue<int> _queue = new CircularQueue<int>(3);
            int expectedLastValue = 3;
            
            _queue.Enqueue(1); // index 1
            _queue.Dequeue();  // remove at 1
            _queue.Enqueue(expectedLastValue); // index 2
            _queue.Enqueue(4); // index 0
            
            int resultValue = _queue.Dequeue(); // remove at 2
            
            Assert.That(resultValue, Is.EqualTo(expectedLastValue));
        }
        
        [Test]
        public void Circular_Size_EnqueueValue_SizeIncreased()
        {
            CircularQueue<int> _queue = new CircularQueue<int>(5);
            
            int newSize = 1;
            
            _queue.Enqueue(5);
            
            Assert.That(_queue.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Circular_Size_DequeueValue_SizeDecreased()
        {
            CircularQueue<int> _queue = new CircularQueue<int>(5);
            
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            
            int expectedSize = 1;

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Circular_Size_DequeueValueWithEmptyQueue_SizeNotChanged()
        {
            CircularQueue<int> _queue = new CircularQueue<int>(5);
            
            int expectedSize = 0;

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }

        #endregion
    }
}