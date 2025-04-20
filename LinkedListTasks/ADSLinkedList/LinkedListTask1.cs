using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Node
    {
        public int value;
        public Node next;
        public Node(int _value) { value = _value; }
    }

    public class LinkedList
    {
        public Node head;
        public Node tail;

        public LinkedList()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(Node _item)
        {
            if (head == null) head = _item;
            else              tail.next = _item;
            tail = _item;
        }

        public Node Find(int _value)
        {
            Node node = head;
            while (node != null)
            {
                if (node.value == _value) return node;
                node = node.next;
            }
            return null;
        }

        // Exercise 1, task 4, time complexity O(n), space complexity O(n)
        public List<Node> FindAll(int _value)
        {
            List<Node> nodes = new List<Node>();
            Node node = head;
            
            while (node != null)
            {
                if (node.value == _value)
                {
                    nodes.Add(node);
                }
                
                node = node.next;
            }
            
            return nodes;
        }

        // Exercise 1, task 1, time complexity O(n), space complexity O(1)
        public bool Remove(int _value)
        {
            Node node = head;
            Node prevNode = null;
            
            while (node != null && node.value != _value)
            {
                prevNode = node;
                node = node.next;
            }
            
            return RemoveNode(node, prevNode);
        }

        // Exercise 1, task 2, time complexity O(n), space complexity O(1)
        public void RemoveAll(int _value)
        {
            Node node = head;
            Node prevNode = null;
            
            while (node != null)
            {
                if (node.value == _value)
                {
                    RemoveNode(node, prevNode);
                }
                else
                {
                    prevNode = node;
                }
                
                node = node.next;
            }
        }

        // Exercise 1, task 3, time complexity O(1), space complexity O(1)
        public void Clear()
        {
            head = null;
            tail = null;
        }

        // Exercise 1, task 5, time complexity O(n), space complexity O(1)
        public int Count()
        {
            Node node = head;
            int count = 0;

            while (node != null)
            {
                ++count;
                node = node.next;
            }
            
            return count;
        }

        // Exercise 1, task 6, time complexity O(n), space complexity O(1)
        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            if (_nodeAfter == null)
            {
                _nodeToInsert.next = head;
                head = _nodeToInsert;
            }
            else
            {
                Node node = head;

                while (node.value != _nodeAfter.value)
                {
                    node = node.next;
                }

                _nodeToInsert.next = node.next;
                node.next = _nodeToInsert;
            }
            
            if (tail == null || _nodeAfter?.value == tail.value) tail = _nodeToInsert;
        }

        private bool RemoveNode(Node _node, Node _prevNode)
        {
            if (_node == null) return false;
            
            if (_node == head) head = _node.next;

            if (_node == tail) tail = _prevNode;
                
            if (_prevNode != null) _prevNode.next = _node.next;

            return true;
        }
    }
}

