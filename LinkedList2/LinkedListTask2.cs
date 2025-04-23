using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{

    public class Node
    {
        public int value;
        public Node next, prev;

        public Node(int _value) { 
            value = _value; 
            next = null;
            prev = null;
        }
    }

    public class LinkedList2
    {
        public Node head;
        public Node tail;

        public LinkedList2()
        {
            head = null;
            tail = null;
        }

        public void AddInTail(Node _item)
        {
            if (head == null) {
                head = _item;
                head.next = null;
                head.prev = null;
            } else {
                tail.next = _item;
                _item.prev = tail;
            }
            tail = _item;
        }

        // Exercise 2, task 1, time complexity O(n), space complexity O(1)
        public Node Find(int _value)
        {
            Node node = null;
            
            for (node = head; node != null && node.value != _value; node = node.next) { }
            
            return node;
        }

        // Exercise 2, task 2, time complexity O(n), space complexity O(n)
        public List<Node> FindAll(int _value)
        {
            List<Node> nodes = new List<Node>();

            for (Node node = head; node != null; node = node.next)
            {
                if (node.value == _value) 
                    nodes.Add(node);
            }
            
            return nodes;
        }

        // Exercise 2, task 3, time complexity O(n), space complexity O(1)
        public bool Remove(int _value)
        {
            Node node;
                
            for (node = head; node != null && node.value != _value; node = node.next) { }
            
            return RemoveNode(node); 
        }

        // Exercise 2, task 4, time complexity O(n), space complexity O(1)
        public void RemoveAll(int _value)
        {
            for (Node node = head; node != null; node = node.next)
            {
                if (node.value == _value)
                    RemoveNode(node);
            }
        }

        // Exercise 2, task 7, time complexity O(1), space complexity O(1)
        public void Clear()
        {
            head = null;
            tail = null;
        }

        // Exercise 2, time complexity O(n), space complexity O(1)
        public int Count()
        {
            int count = 0;

            for(Node node = head; node != null; node = node.next)
            {
                ++count;
            }
            
            return count;
        }

        // Exercise 2, task 5, task 6, time complexity O(1), space complexity O(1)
        public void InsertAfter(Node _nodeAfter, Node _nodeToInsert)
        {
            if (_nodeAfter == null)
            {
                _nodeToInsert.next = head;

                if (head != null) 
                    head.prev = _nodeToInsert;
                
                head = _nodeToInsert;
            }
            else
            {
                _nodeToInsert.prev = _nodeAfter;
                _nodeToInsert.next = _nodeAfter.next;
                
                if (_nodeAfter.next != null) 
                    _nodeAfter.next.prev = _nodeToInsert;
                
                _nodeAfter.next = _nodeToInsert;
            }
            
            if (tail == null || _nodeAfter == tail) 
                tail = _nodeToInsert;
        }
        
        private bool RemoveNode(Node node)
        {
            if (node == null) return false;

            if (node.prev != null) node.prev.next = node.next;
            
            if (node.next != null) node.next.prev = node.prev;
            
            if (node == head) head = node.next;

            if (node == tail) tail = node.prev;

            return true;
        }
    }
}