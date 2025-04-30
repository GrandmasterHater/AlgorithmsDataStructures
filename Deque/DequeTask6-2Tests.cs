using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class DequeTask6_2Tests
    {
        [TestCase("Was it a car or a cat I saw?", true)]
        [TestCase("Madam?", true)]
        [TestCase("A man, a plan, a canal Panama", true)]
        [TestCase("The quick brown fox", false)]
        [TestCase("C# is awesome", false)]
        public void IsPalindromeTest(string data, bool expectedValue)
        {
            Assert.That(data.IsPalindrome(), Is.EqualTo(expectedValue));
        }

        [TestCase(new []{ 8, 7, 1, 3, 5}, 1)]
        [TestCase(new []{ 1, 7, 4, 8, 5}, 1)]
        [TestCase(new []{ 10, 7, 4, 3, 5}, 3)]
        public void DequeWithMinTest(int[] data, int expectedMinValue)
        {
            DequeWithMin deque = new DequeWithMin();

            foreach (int number in data)
            {
                if (number % 2 == 0)
                    deque.AddFront(number);
                else
                    deque.AddTail(number);
            }
            
            Assert.That(deque.GetMinValue(), Is.EqualTo(expectedMinValue));
        }

        #region DequeWithDynArrayPrincipleTests

        [TestCase(5)]
        public void AddFront_AddValue_FirstAddToFrontValueIsFirstGetFromFront(int expectedValue)
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            deque.AddFront(expectedValue);
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(5)]
        public void AddFront_AddValue_FirstAddToFrontValueIsFirstGetFromTail(int expectedValue)
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            deque.AddFront(expectedValue);
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void AddFront_WhenDequeIsFull_CapacityExtended()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity).ToList();
            List<int> expectedData = data.ToList();
            int newValue = 50;
            expectedData.Add(newValue);

            foreach (int number in data)
            {
                deque.AddFront(number);
            }
            
            int expectedCapacity = deque.Capacity * DequeWithDynArrayPrinciple<int>.CAPACITY_MULTIPLIER;
            
            List<int> results = new List<int>();
            deque.AddFront(newValue);

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveTail());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [Test]
        public void AddFront_WhenDequeNotFull_CapacityNotChanged()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity - 1).ToList();
            List<int> expectedData = data.ToList();
            int newValue = 50;
            expectedData.Add(newValue);

            foreach (int number in data)
            {
                deque.AddFront(number);
            }
            
            int expectedCapacity = deque.Capacity;
            
            List<int> results = new List<int>();
            deque.AddFront(newValue);

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveTail());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [TestCase(5)]
        public void AddTail_AddValue_FirstAddToTailValueIsFirstGetFromFront(int expectedValue)
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            deque.AddTail(expectedValue);
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(5)]
        public void AddTail_AddValues_FirstAddToTailValueIsFirstGetFromTail(int expectedValue)
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            deque.AddTail(expectedValue);
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void AddTail_WhenDequeIsFull_CapacityExtended()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity).ToList();
            List<int> expectedData = data.ToList();
            int newValue = 50;
            expectedData.Add(newValue);

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            int expectedCapacity = deque.Capacity * DequeWithDynArrayPrinciple<int>.CAPACITY_MULTIPLIER;
            
            List<int> results = new List<int>();
            deque.AddTail(newValue);

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveFront());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [Test]
        public void AddTail_WhenDequeNotFull_CapacityNotChanged()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity - 1).ToList();
            List<int> expectedData = data.ToList();
            int newValue = 50;
            expectedData.Add(newValue);

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            int expectedCapacity = deque.Capacity;
            
            List<int> results = new List<int>();
            deque.AddTail(newValue);

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveFront());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [TestCase(new [] {8, 7, 2 }, 8)]
        public void RemoveFront_DequeWithValues_FirstRemovedFromFrontValueExpected(int[] data, int expectedValue)
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] { 8, 7, 2 }, 2)]
        public void RemoveTail_DequeWithValues_FirstRemovedFromTailValueExpected(int[] data, int expectedValue)
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void RemoveFront_EmptyQueue_GetDefaultResultForType()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            int expectedValue = default(int);
            
            Assert.That(deque.RemoveFront(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void RemoveFront_WhenDequeHalfFilled_CapacityReduced()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity + 1).ToList();
            List<int> expectedData = data.ToList();
            expectedData.Remove(1);
            expectedData.Remove(2);

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            int expectedCapacity = (int)(deque.Capacity / DequeWithDynArrayPrinciple<int>.CAPACITY_DIVIDER);
            
            List<int> results = new List<int>();
            deque.RemoveFront();
            deque.RemoveFront();

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveFront());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [Test]
        public void RemoveFront_WhenDequeFull_CapacityNotChanged()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity).ToList();
            List<int> expectedData = data.ToList();
            expectedData.RemoveAt(0);

            foreach (int number in data)
            {
                deque.AddTail(number);
            }
            
            int expectedCapacity = deque.Capacity;
            
            List<int> results = new List<int>();
            deque.RemoveFront();

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveFront());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [Test]
        public void RemoveTail_WhenDequeHalfFilled_CapacityReduced()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity + 1).ToList();
            List<int> expectedData = data.ToList();
            expectedData.RemoveAt(0);
            expectedData.RemoveAt(0);

            foreach (int number in data)
            {
                deque.AddFront(number);
            }
            
            int expectedCapacity = (int)(deque.Capacity / DequeWithDynArrayPrinciple<int>.CAPACITY_DIVIDER);
            
            List<int> results = new List<int>();
            deque.RemoveTail();
            deque.RemoveTail();

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveTail());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [Test]
        public void RemoveTail_WhenDequeFull_CapacityNotChanged()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();

            List<int> data = Enumerable.Range(1, deque.Capacity).ToList();
            List<int> expectedData = data.ToList();
            expectedData.RemoveAt(0);

            foreach (int number in data)
            {
                deque.AddFront(number);
            }
            
            int expectedCapacity = deque.Capacity;
            
            List<int> results = new List<int>();
            deque.RemoveTail();

            int resultCapacity = deque.Capacity;

            while (deque.Size() > 0)
            {
                results.Add(deque.RemoveTail());
            }
            
            Assert.That(resultCapacity, Is.EqualTo(expectedCapacity));
            Assert.That(results, Is.EqualTo(expectedData));
        }
        
        [Test]
        public void RemoveTail_EmptyQueue_GetDefaultResultForType()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            int expectedValue = default(int);
            
            Assert.That(deque.RemoveTail(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Size_AddValueToFront_SizeIncreased()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            int newSize = 1;
            
            deque.AddFront(5);
            
            Assert.That(deque.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Size_AddValueToTail_SizeIncreased()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            int newSize = 1;
            
            deque.AddTail(5);
            
            Assert.That(deque.Size(), Is.EqualTo(newSize));
        }
        
        [Test]
        public void Size_RemoveValueFromFront_SizeDecreased()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            deque.AddFront(1);
            deque.AddTail(2);
            
            int expectedSize = 1;

            deque.RemoveFront();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_RemoveValueFromTail_SizeDecreased()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            deque.AddFront(1);
            deque.AddTail(2);
            
            int expectedSize = 1;

            deque.RemoveTail();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_RemoveFrontValueWithEmptyQueue_SizeNotChanged()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            int expectedSize = 0;

            deque.RemoveFront();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Size_RemoveTailValueWithEmptyQueue_SizeNotChanged()
        {
            DequeWithDynArrayPrinciple<int> deque = new DequeWithDynArrayPrinciple<int>();
            
            int expectedSize = 0;

            deque.RemoveFront();
            
            Assert.That(deque.Size(), Is.EqualTo(expectedSize));
        }

        #endregion
    }
}