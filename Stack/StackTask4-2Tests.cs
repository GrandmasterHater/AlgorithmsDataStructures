using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgorithmsDataStructures
{
    [TestFixture]
    public class StackTask4_2Tests
    {
        #region Stack2Test
        
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Size_AddValues_ExpectedSize(int expectedSize)
        {
            Stack2<int> _stack = new Stack2<int>();
            
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
            Stack2<int> _stack = new Stack2<int>();
            
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
            Stack2<int> _stack = new Stack2<int>();
            
            int expectedSize = 0;

            _stack.Push(5);
            _stack.Pop();
            
            Assert.That(_stack.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Pop_EmptyStack_NullReturned()
        {
            Stack2<object> _stack = new Stack2<object>();
            
            object expectedValue = null;

            object returnedValue = _stack.Pop();
            
            Assert.That(returnedValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Push_AddNewValue_ValueAdded()
        {
            Stack2<int> _stack = new Stack2<int>();
            
            int expectedValue = 5;

            _stack.Push(expectedValue);
            int returnedValue = _stack.Peek();
            
            Assert.That(returnedValue, Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Push_AddNewValue_SizeIncreased()
        {
            Stack2<int> _stack = new Stack2<int>();
            
            int expectedSize = 1;

            _stack.Push(5);
            
            Assert.That(_stack.Size(), Is.EqualTo(expectedSize));
        }
        
        [Test]
        public void Peek_EmptyList_NullReturned()
        {
            Stack2<object> _stack = new Stack2<object>();
            
            object expectedValue = null;
            
            Assert.That(_stack.Peek(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Peek_ListWithValues_GetExpectedValue()
        {
            Stack2<int> _stack = new Stack2<int>();
            
            int expectedValue = 5;

            _stack.Push(expectedValue);
            
            Assert.That(_stack.Peek(), Is.EqualTo(expectedValue));
        }
        
        [Test]
        public void Peek_ListWithValues_SizeNotChanged()
        {
            Stack2<int> _stack = new Stack2<int>();
            
            int expectedSize = 1;

            _stack.Push(5);
            _stack.Peek();
            
            Assert.That(_stack.Size(), Is.EqualTo(expectedSize));
        }
        
        #endregion
        
        [TestCase("(()((())()))", true)]
        [TestCase("(()()(()", false)]
        [TestCase("())(", false)]
        [TestCase("))((", false)]
        [TestCase("((())", false)]
        [TestCase("(({()}))", true)]
        [TestCase("([{()}])", true)]
        [TestCase("([{()])", false)]
        [TestCase("([{()])}", false)]
        public void IsQueueBalancedTest(string data, bool expectedValue)
        {
            bool isBalanced = StackUtils.IsQueueBalanced(data);
            
            Assert.That(isBalanced, Is.EqualTo(expectedValue));
        }
        
        [TestCase(new [] {1, 2, 3, 4, 5}, 1)]
        [TestCase(new [] {3, 4, 5}, 3)]
        [TestCase(new [] {8, 7, 4, 5}, 4)]
        public void Stack2WithMinValueTest(int[] data, int expectedMinValue)
        {
            Stack2WithMinValue<int> stack = new Stack2WithMinValue<int>((arg1, arg2) => arg1 < arg2 ? arg1 : arg2);
        
            foreach (int value in data)
            {
                stack.Push(value);
            }
        
            Assert.That(stack.GetMinValue(), Is.EqualTo(expectedMinValue));
        }
        
        [TestCase(new [] {1, 2, 3, 4, 5}, 3)]
        [TestCase(new [] {3, 4, 5}, 4)]
        [TestCase(new [] {8, 7, 4, 5}, 6)]
        public void Stack2WithAverageTest(int[] data, int expectedAverageValue)
        {
            Stack2WithAverage stack = new Stack2WithAverage();
        
            foreach (int value in data)
            {
                stack.Push(value);
            }
        
            Assert.That(stack.GetAverageValue(), Is.EqualTo(expectedAverageValue));
        }
        
        [TestCase("1 2 + 3 * =", 9)]
        [TestCase("8 2 + 5 * 9 + =", 59)]
        public void PostfixCalculatorTest(string data, int expectedAverageValue)
        {
            int result = StackUtils.PostfixCalculator(data.Replace(" ", ""));
        
            Assert.That(result, Is.EqualTo(expectedAverageValue));
        }
    }
}