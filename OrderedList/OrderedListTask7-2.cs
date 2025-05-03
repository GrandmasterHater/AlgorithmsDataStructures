using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures
{
    public static class OrderedListTask7_2
    {
        // Exercise 7, task 8, time complexity O(n), space complexity O(1)
        public static void Distinct<T>(this OrderedList<T> list)
        {
            bool nodeEquals = false;
            
            for (Node<T> node = list.head; node != null && node.next != null; node = nodeEquals ? node : node.next)
            {
                nodeEquals = list.Compare(node.value, node.next.value) == 0;
                
                if (nodeEquals)
                {
                    list.DeleteNode(node.next);
                }
            }
        }
        
        public static void ReverseList<T>(this OrderedList<T> list)
        {
            Node<T> tempNode;
            
            for (Node<T> node = list.head; node != null; node = node.prev)
            {
                tempNode = node.prev;
                node.prev = node.next;
                node.next = tempNode;
            }

            tempNode = list.head;
            list.head = list.tail;
            list.tail = tempNode;
        }
        
        // Exercise 7, task 9, time complexity O(n), space complexity O(n)
        public static OrderedList<T> Union<T>(this OrderedList<T> firstList, OrderedList<T> secondList, bool ascending)
        {
            bool isFirstAscending = firstList.Compare(firstList.head.value, firstList.head.next.value) < 0;
            bool isSecondAscending = secondList.Compare(secondList.head.value, secondList.head.next.value) < 0;

            if (isFirstAscending != ascending)
            {
                firstList.ReverseList();
            }
            
            if (isSecondAscending != ascending)
            {
                secondList.ReverseList();
            }
            
            OrderedList<T> result = new OrderedList<T>(ascending);
            
            Node<T> currentFirst;
            Node<T> currentSecond;

            for (currentFirst = firstList.head, currentSecond = secondList.head; currentFirst != null && currentSecond != null; )
            {
                int compareResult = firstList.Compare(currentFirst.value, currentSecond.value);
                
                if (ascending && compareResult <= 0 || !ascending && compareResult >= 0)
                {
                    result.Add(currentFirst.value);
                    currentFirst = currentFirst.next;
                }
                else
                {
                    result.Add(currentSecond.value);
                    currentSecond = currentSecond.next;
                }

                if (compareResult == 0)
                {
                    result.Add(currentSecond.value);
                    currentSecond = currentSecond.next;
                }
            }

            Node<T> current = currentFirst != null ? currentFirst : null;
            current = currentSecond != null ? currentSecond : current;

            for (; current != null; current = current.next)
            {
                result.Add(current.value);
            }
            
            return result; 
        }

        // Exercise 7, task 10, time complexity O(n), space complexity O(1)
        public static bool ExistSubOrderedList<T>(this OrderedList<T> list, OrderedList<T> subList)
        {
            list = list ?? throw new ArgumentNullException();
            subList = subList ?? throw new ArgumentNullException();
            
            for (Node<T> node = list.head; node != null; node = node.next)
            {
                int compareResult = list.Compare(node.value, subList.head.value);

                if (compareResult == 0 && IsListEquals(node, subList.head))
                {
                    return true;
                }
            }

            return false;

            bool IsListEquals(Node<T> firstNode, Node<T> secondNode)
            {
                for ( ;firstNode != null && secondNode != null; firstNode = firstNode.next, secondNode = secondNode.next)
                {
                    if (list.Compare(firstNode.value, secondNode.value) != 0)
                    {
                        return false;
                    }
                }

                return secondNode == null;
            }
        }

        // Exercise 7, task 11, time complexity O(n), space complexity O(1)
        public static T GetMostFrequentlyOccuredValue<T>(this OrderedList<T> list)
        {
            list = list ?? throw new ArgumentNullException();
            
            T currentValue = list.head.value;
            int currentCount = 1;
            
            T mostFrequentlyOccuredValue = currentValue;
            int mostFrequentlyOccuredValueCount = currentCount;
            
            for (Node<T> node = list.head.next; node != null; node = node.next)
            {
                int compareResult = list.Compare(node.value, currentValue);

                if (compareResult != 0 && mostFrequentlyOccuredValueCount < currentCount)
                {
                    mostFrequentlyOccuredValueCount = currentCount;
                    mostFrequentlyOccuredValue = currentValue;
                }
                
                if (compareResult != 0)
                {
                    currentValue = node.value;
                    currentCount = 0;
                }
                
                ++currentCount;
            }

            return mostFrequentlyOccuredValue;
        }
    }

    // Exercise 7, task 12
    public class OrderedListWithIndexes<T> where T : IComparable
    {
        private List<T> _buffer; //as DynArray
        private bool _ascending; 

        public OrderedListWithIndexes(bool asc)
        {
            _buffer = new List<T>();
            _ascending = asc;
        }

        // Exercise 7, task 12, time complexity O(1), space complexity O(1)
        public int Compare(T v1, T v2)
        {
            int result = 0;
            
            if(typeof(T) == typeof(String))
            {
                string v1s = v1 as string;
                string v2s = v2 as string;
                v1s = v1s.Trim();
                v2s = v2s.Trim();
                result = string.Compare(v1s, v2s, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                result = v1.CompareTo(v2);
            }
            
            return result;
        }

        // Exercise 7, task 12, time complexity O(n), o(1), space complexity O(n), o(1)
        public void Add(T value)
        {
            int index = GetIndex(value);

            if (index < 0)
            {
                index = ~index;
            }
            
            _buffer.Insert(index, value);
        }

        // Exercise 7, task 12, time complexity O(n), o(1), space complexity O(1)
        public void Delete(T val)
        {
            _buffer.Remove(val);
        }

        // Exercise 7, task 12, time complexity O(1), space complexity O(1)
        public void Clear(bool asc)
        {
            _buffer.Clear();
            _ascending = asc;
        }

        // Exercise 7, task 12, time complexity O(1), space complexity O(1)
        public int Count()
        {
            return _buffer.Count;
        }
        
        // Exercise 7, task 12, time complexity O(1, space complexity O(1)
        public T GetValue(int index)
        {
            return _buffer[index];
        }

        // Exercise 7, task 12, time complexity O(log n), space complexity O(1)
        public int GetIndex(T val)
        {
            return _ascending ? _buffer.BinarySearch(val) : _buffer.BinarySearch(val, new DescendingComparer<T>());
        }

        // Exercise 7, task 12, time complexity O(n), space complexity O(n)
        public List<T> GetAll()
        {
            return _buffer.ToList();
        }
        
        private class DescendingComparer<T> : IComparer<T> where T : IComparable
        {
            public int Compare(T x, T y)
            {
                return y.CompareTo(x);
            }
        }
    }
}