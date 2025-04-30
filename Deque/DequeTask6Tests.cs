using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class DequeTask6Tests
    {
        [TestCase(5)]
        public void AddFront_AddValue_FirstAddToFrontValueIsFirstGetFromFront(int expectedValue)
        {
            Deque<int> deque = new Deque<int>();

            deque.AddFront(expectedValue);
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(5)]
        public void AddFront_AddValue_FirstAddToFrontValueIsFirstGetFromTail(int expectedValue)
        {
            Deque<int> deque = new Deque<int>();
            
            deque.AddFront(expectedValue);
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(5)]
        public void AddTail_AddValue_FirstAddToTailValueIsFirstGetFromFront(int expectedValue)
        {
            Deque<int> deque = new Deque<int>();

            deque.AddTail(expectedValue);
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(5)]
        public void AddTail_AddValues_FirstAddToTailValueIsFirstGetFromTail(int expectedValue)
        {
            Deque<int> deque = new Deque<int>();
            
            deque.AddTail(expectedValue);
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] {8, 7, 2 }, 8)]
        public void RemoveFront_DequeWithValues_FirstRemovedFromFrontValueExpected(int[] data, int expectedValue)
        {
            Deque<int> deque = new Deque<int>();

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] { 8, 7, 2 }, 2)]
        public void RemoveTail_DequeWithValues_FirstRemovedFromTailValueExpected(int[] data, int expectedValue)
        {
            Deque<int> deque = new Deque<int>();

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void RemoveFront_EmptyQueue_GetDefaultResultForType()
        {
            Deque<int> deque = new Deque<int>();
            
            int expectedValue = default(int);
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void RemoveTail_EmptyQueue_GetDefaultResultForType()
        {
            Deque<int> deque = new Deque<int>();
            
            int expectedValue = default(int);
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Size_AddValueToFront_SizeIncreased()
        {
            Deque<int> deque = new Deque<int>();
            
            int newSize = 1;
            
            deque.AddFront(5);
            
            Assert.That(deque.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Size_AddValueToTail_SizeIncreased()
        {
            Deque<int> deque = new Deque<int>();
            
            int newSize = 1;
            
            deque.AddTail(5);
            
            Assert.That(deque.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Size_RemoveValueFromFront_SizeDecreased()
        {
            Deque<int> deque = new Deque<int>();
            
            deque.AddFront(1);
            deque.AddTail(2);
            
            int expectedSize = 1;

            deque.RemoveFront();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_RemoveValueFromTail_SizeDecreased()
        {
            Deque<int> deque = new Deque<int>();
            
            deque.AddFront(1);
            deque.AddTail(2);
            
            int expectedSize = 1;

            deque.RemoveTail();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_RemoveFrontValueWithEmptyQueue_SizeNotChanged()
        {
            Deque<int> deque = new Deque<int>();
            
            int expectedSize = 0;

            deque.RemoveFront();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_RemoveTailValueWithEmptyQueue_SizeNotChanged()
        {
            Deque<int> deque = new Deque<int>();
            
            int expectedSize = 0;

            deque.RemoveFront();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
    }
}