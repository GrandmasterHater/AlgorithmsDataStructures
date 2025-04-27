using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AlgorithmsDataStructures
{
    // Exercise 4, task 2
    public class Stack2<T>
    {
        private LinkedList<T> _linkedList;
        
        public Stack2()
        {
            _linkedList = new LinkedList<T>();
        } 

        // Exercise 4, task 2, time complexity O(1), space complexity O(1)
        public int Size() 
        {
            return _linkedList.Count;
        }

        // Exercise 4, task 2, time complexity O(1), space complexity O(1)
        public T Pop()
        {
            T result = default(T);

            if (_linkedList.Count > 0)
            {
                result = _linkedList.First.Value;
                _linkedList.RemoveFirst();
            }
            
            return result;
        }
	  
        // Exercise 4, task 2, time complexity O(1), space complexity O(1)
        public void Push(T val)
        {
            _linkedList.AddFirst(val);
        }

        // Exercise 4, task 2, time complexity O(1), space complexity O(1)
        public T Peek()
        {
            T result = _linkedList.Count > 0 ? _linkedList.First.Value : default(T);
            
            return result;
        }
    }

    public class StackUtils
    {
        // Exercise 4, task 4, task 5, time complexity O(n), space complexity O(n)
        public static bool IsQueueBalanced(string data)
        {
            Stack<char> stack = new Stack<char>();
            bool isBroken = false;
            
            foreach (char symbol in data)
            {
                char foundSymbol = symbol;
                bool isPopSymbol = symbol == ')' || symbol == ']' || symbol == '}';

                if (isPopSymbol) 
                    foundSymbol = stack.Pop();
                else
                    stack.Push(symbol);

                if (isPopSymbol && !IsSymbolsMatch(foundSymbol, symbol))
                {
                    isBroken = true;
                    break;
                }
            }
            
            return !isBroken && stack.Size() == 0;
            
            bool IsSymbolsMatch(char firstSymbol, char secondSymbol)
            {
                return firstSymbol == '(' && secondSymbol == ')' ||
                       firstSymbol == '[' && secondSymbol == ']' ||
                       firstSymbol == '{' && secondSymbol == '}';
            }
        }

        // Exercise 4, task 8, time complexity O(n), space complexity O(n)
        public static int PostfixCalculator(string inputData)
        {
            Stack<char> inputStack = new Stack<char>();

            for (int i = inputData.Length - 1 ; i >= 0; --i)
            {
                inputStack.Push(inputData[i]);
            }
            
            Stack<int> numbers = new Stack<int>();
            
            for (char symbol = inputStack.Pop(); inputStack.Size() > 0; symbol = inputStack.Pop())
            {
                int number = 0;
                bool isOperationSymbol = IsOperationSymbol(symbol);

                number = isOperationSymbol ? ExecuteOperation(symbol, numbers) : symbol - '0';
                
                numbers.Push(number);

                if (symbol == '=')
                {
                    break;
                }
            }

            return numbers.Pop();

            bool IsOperationSymbol(char symbol)
            {
                return symbol == '+' || symbol == '*' || symbol == '=';
            }

            int ExecuteOperation(char operationSymbol, Stack<int> argsStack)
            {
                switch (operationSymbol)
                {
                    case '+':
                        return argsStack.Pop() + argsStack.Pop();
                    case '*':
                        return argsStack.Pop() * argsStack.Pop();
                    case '=':
                        return argsStack.Pop();
                    default:
                        throw new ArgumentException();
                }
            }
        }
    }
    
    // Exercise 4, task 6
    public class Stack2WithMinValue<T> 
    {
        private LinkedList<T> _stack;
        private LinkedList<T> _minValues;
        private Func<T, T, T> _minValueComparer;
        
        public Stack2WithMinValue(Func<T, T, T> minValueComparer)
        {
            _stack = new LinkedList<T>();
            _minValues = new LinkedList<T>();
            _minValueComparer = minValueComparer;
        } 

        public int Size() 
        {
            return _stack.Count;
        }

        public T Pop()
        {
            T result = default(T);

            if (_stack.Count > 0)
            {
                result = _stack.First.Value;
                _stack.RemoveFirst();
                _minValues.RemoveFirst();
            }
            
            return result;
        }
	  
        public void Push(T val)
        {
            _stack.AddFirst(val);

            if (_minValues.First == null)
            {
                _minValues.AddFirst(val);
            }
            else
            {
                T minValue = _minValueComparer.Invoke(_minValues.First.Value, val);
                _minValues.AddFirst(minValue);
            }
        }

        public T Peek()
        {
            T result = _stack.Count > 0 ? _stack.First.Value : default(T);
            
            return result;
        }

        // Exercise 4, task 6, time complexity O(1), space complexity O(1)
        public T GetMinValue()
        {
            return _minValues.First != null ? _minValues.First.Value : default(T);
        }
    }
    
    // Exercise 4, task 7
    public class Stack2WithAverage 
    {
        private LinkedList<int> _linkedList;
        private LinkedList<int> _sumList;
        private int _itemsCount;
        
        public Stack2WithAverage()
        {
            _linkedList = new LinkedList<int>();
            _sumList = new LinkedList<int>();
            _itemsCount = 0;
        } 

        public int Size() 
        {
            return _linkedList.Count;
        }

        public int Pop()
        {
            int result = default;

            if (_linkedList.Count > 0)
            {
                result = _linkedList.First.Value;
                _linkedList.RemoveFirst();
                _sumList.RemoveFirst();
                --_itemsCount;
            }
            
            return result;
        }
	  
        public void Push(int val)
        {
            _linkedList.AddFirst(val);

            if (_sumList.First == null)
            {
                _sumList.AddFirst(val);
            }
            else
            {
                int sum = _sumList.First.Value + val;
                _sumList.AddFirst(sum);
            }
            
            ++_itemsCount;
        }

        public int Peek()
        {
            int result = _linkedList.Count > 0 ? _linkedList.First.Value : default;
            
            return result;
        }

        // Exercise 4, task 7, time complexity O(1), space complexity O(1)
        public int GetAverageValue()
        {
            return _sumList.First?.Value / _itemsCount ?? default;
        }
    }
}