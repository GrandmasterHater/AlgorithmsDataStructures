using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class StackTask4Tests
    {
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Size_AddValues_ExpectedSize(int expectedSize)
        {
            Stack<int> _stack = new Stack<int>();
            
            for (int i = 0; i < expectedSize; i++)
            {
                _stack.Push(i);
            }
            
            int size = _stack.Size();
            
            Assert.That(size, Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Pop_GetValuesFromStack_ValueReturnedWithExpectedOrder()
        {
            Stack<int> _stack = new Stack<int>();
            
            List<int> expectedValues = Enumerable.Range(0, 5).ToList();

            foreach (int value in expectedValues)
            {
                _stack.Push(value);
            }
            
            expectedValues.Reverse();
            
            List<int> returnedValues = new List<int>();

            while (_stack.Size() > 0)
            {
                returnedValues.Add(_stack.Pop());
            }
            
            Assert.That(returnedValues, Is.EqualTo(expectedValues));
            Assert.That(returnedValues, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Pop_GetValuesFromStack_SizeReduced()
        {
            Stack<int> _stack = new Stack<int>();
            
            int expectedSize = 0;

            _stack.Push(5);
            _stack.Pop();
            
            Assert.That(_stack.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Pop_EmptyStack_NullReturned()
        {
            Stack<object> _stack = new Stack<object>();
            
            object expectedValue = null;

            object returnedValue = _stack.Pop();
            
            Assert.That(returnedValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Push_AddNewValue_ValueAdded()
        {
            Stack<int> _stack = new Stack<int>();
            
            int expectedValue = 5;

            _stack.Push(expectedValue);
            int returnedValue = _stack.Peek();
            
            Assert.That(returnedValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Push_AddNewValue_SizeIncreased()
        {
            Stack<int> _stack = new Stack<int>();
            
            int expectedSize = 1;

            _stack.Push(5);
            
            Assert.That(_stack.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Peek_EmptyList_NullReturned()
        {
            Stack<object> _stack = new Stack<object>();
            
            object expectedValue = null;
            
            Assert.That(_stack.Peek(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Peek_ListWithValues_GetExpectedValue()
        {
            Stack<int> _stack = new Stack<int>();
            
            int expectedValue = 5;

            _stack.Push(expectedValue);
            
            Assert.That(_stack.Peek(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Peek_ListWithValues_SizeNotChanged()
        {
            Stack<int> _stack = new Stack<int>();
            
            int expectedSize = 1;

            _stack.Push(5);
            _stack.Peek();
            
            Assert.That(_stack.Size(), Is.EqualTo(expectedSize));
        }
    }
}