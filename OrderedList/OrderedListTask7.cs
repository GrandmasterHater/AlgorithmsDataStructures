using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Node<T>
    {
        public T value;
        public Node<T> next, prev;

        public Node(T _value)
        {
            value = _value;
            next = null;
            prev = null;
        }
    }

    public class OrderedList<T>
    {
        public Node<T> head, tail;
        private bool _ascending;

        public OrderedList(bool asc)
        {
            head = null;
            tail = null;
            _ascending = asc;
        }

        // Exercise 7, task 2, time complexity O(1), space complexity O(1)
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
                IComparable<T> v1c = v1 as IComparable<T>;
                
                result = v1c.CompareTo(v2);
            }
            
            return result;
        }

        // Exercise 7, task 3, time complexity O(n), space complexity O(1)
        public void Add(T value)
        {
            Node<T> valueNode = new Node<T>(value);
            
            if (head == null)
            {
                InsertNode(null, valueNode);
                return;
            }

            for (Node<T> node = head ; node != null; node = node.next)
            {
                int compareResult = Compare(value, node.value);
                
                if (_ascending && compareResult < 0 || !_ascending && compareResult > 0)
                {
                    InsertNode(node.prev, valueNode);
                    break;
                }

                if (node.next == null)
                {
                    InsertNode(node, valueNode);
                    break;
                }
            }
        }

        // Exercise 7, task 6, time complexity O(n), Omega(1) with preventive search interruption, space complexity O(1)
        public Node<T> Find(T val)
        {
            Node<T> result = null;
            int compareResult;
            
            for (Node<T> node = head; node != null; node = node.next)
            {
                compareResult = Compare(node.value, val);
                
                if (_ascending && compareResult > 0 || !_ascending && compareResult < 0)
                {
                    break;
                }
                
                if (compareResult == 0)
                {
                    result = node;
                    break;
                }
            }
            
            return result;
        }

        // Exercise 7, task 4, time complexity O(n), space complexity O(1)
        public void Delete(T val)
        {
            for (Node<T> node = head; node != null; node = node.next)
            {
                if (Compare(node.value, val) == 0)
                {
                    DeleteNode(node);
                    break;
                }
            }
        }

        public void Clear(bool asc)
        {
            _ascending = asc;
            head = null;
            tail = null;
        }

        public int Count()
        {
            int count = 0;

            for (Node<T> node = head; node != null; node = node.next)
            {
                ++count;
            }
            
            return count; 
        }

        List<Node<T>> GetAll() 
        {
            List<Node<T>> r = new List<Node<T>>();
            Node<T> node = head;
            while(node != null)
            {
                r.Add(node);
                node = node.next;
            }
            return r;
        }
        
        public void InsertNode(Node<T> nodeAfter, Node<T> node)
        {
            if (nodeAfter == null && head != null)
            {
                head.prev = node;
            }
            
            if (nodeAfter == null)
            {
                node.next = head;
                head = node;
            }
            else
            {
                node.prev = nodeAfter;
                node.next = nodeAfter.next;
                nodeAfter.next = node;
            }
            
            if (node.next != null) 
                node.next.prev = node;
            
            if (tail == null || nodeAfter == tail) 
                tail = node;
        }
        
        public void DeleteNode(Node<T> node)
        {
            if (node.next != null) 
                node.next.prev = node.prev;
            else
                tail = node.prev;

            if (node.prev != null)
                node.prev.next = node.next;
            else
                head = node.next;
        }
    }
 
}

