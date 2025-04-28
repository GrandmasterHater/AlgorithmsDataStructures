using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class QueueTask5Tests
    {
        [TestCase(new [] {5, 7, 2 }, 5)]
        public void Enqueue_AddValues_FirstEnqueuedValueIsFirstDequeued(int[] data, int expectedValue)
        {
            Queue<int> _queue = new Queue<int>();
            
            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] {8, 7, 2 }, 8)]
        public void Dequeue_QueueWithValues_FirstDequeuedValueExpected(int[] data, int expectedValue)
        {
            Queue<int> _queue = new Queue<int>();

            foreach (int number in data)
            {
                _queue.Enqueue(number);
            }
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] {8, 7, 2 }, 2)]
        public void Dequeue_QueueWithValues_SizeDecreased(int[] data, int expectedSize)
        {
            Queue<int> _queue = new Queue<int>();
            
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
            Queue<int> _queue = new Queue<int>();
            
            int expectedValue = default(int);
            
            Assert.That(_queue.Dequeue(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Size_EnqueueValue_SizeIncreased()
        {
            Queue<int> _queue = new Queue<int>();
            
            int newSize = 1;
            
            _queue.Enqueue(5);
            
            Assert.That(_queue.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Size_DequeueValue_SizeDecreased()
        {
            Queue<int> _queue = new Queue<int>();
            
            
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            
            int expectedSize = 1;

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_DequeueValueWithEmptyQueue_SizeNotChanged()
        {
            Queue<int> _queue = new Queue<int>();
            
            int expectedSize = 0;

            _queue.Dequeue();
            
            Assert.That(_queue.Size(), Is.EqualTo(expectedSize));
        }
    }
}